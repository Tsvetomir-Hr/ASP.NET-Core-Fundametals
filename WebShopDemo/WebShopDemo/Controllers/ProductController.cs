using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopDemo.Core.Constants;
using WebShopDemo.Core.Contracts;
using WebShopDemo.Core.Models;

namespace WebShopDemo.Controllers
{
    /// <summary>
    /// Web shop products
    /// </summary>
    /// 
     // used to autorize only to logged in users to see products page 
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        /// <summary>
        /// List all products
        /// </summary>
        /// <returns></returns>
        /// 
        // now index will be allowed to not logged in user but they can't modify the items in the collection.
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = await this._productService.GetAll();
            ViewData["Title"] = "Products";
            return View(products);
        }
        [HttpGet]
        [Authorize(Roles  = $"{RoleConstants.Manager}, {RoleConstants.Supervisor}")]
        public IActionResult Add()
        {
            var model = new ProductDto();
            ViewData["Title"] = "Add new product";

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Manager}, {RoleConstants.Supervisor}")]
        public async Task<IActionResult> Add(ProductDto model)
        {
            ViewData["Title"] = "Add new product";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _productService.Add(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles =RoleConstants.Supervisor)]

        public async Task<IActionResult> Delete([FromForm] string id)
        {
            Guid idGuid = Guid.Parse(id);
            await _productService.Delete(idGuid);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = RoleConstants.Manager)]
        public async Task<IActionResult> Buy([FromForm] string id)
        {
            Guid guidId = Guid.Parse(id);

            await this._productService.Buy(guidId);

            return RedirectToAction(nameof(Index));
        }

    }
}

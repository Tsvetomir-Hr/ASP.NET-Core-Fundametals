using Microsoft.AspNetCore.Mvc;
using WebShopDemo.Core.Contracts;
using WebShopDemo.Core.Models;

namespace WebShopDemo.Controllers
{
    /// <summary>
    /// Web shop products
    /// </summary>
    public class ProductController : Controller
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
        public async Task<IActionResult> Index()
        {
            var products = await this._productService.GetAll();
            ViewData["Title"] = "Products";
            return View(products);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var model = new ProductDto();
            ViewData["Title"] = "Add new product";

            return View(model);
        }
        [HttpPost]
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
    }
}

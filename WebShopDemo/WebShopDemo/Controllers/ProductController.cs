using Microsoft.AspNetCore.Mvc;
using WebShopDemo.Core.Contracts;

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
        public async  Task<IActionResult> Index()
        {
            var products = await this._productService.GetAll();
            ViewData["Title"] = "Products";
            return View(products);
        }
        public async Task<IActionResult> Add()
        {
            //TODO:
            throw new NotImplementedException();
        }
    }
}

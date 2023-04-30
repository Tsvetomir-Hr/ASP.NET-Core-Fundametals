using Microsoft.AspNetCore.Mvc;
using MVC_Intro_Demo.Models;

namespace MVC_Intro_Demo.Controllers
{
    public class ProductsController : Controller
    {
     
        private IEnumerable<ProductViewModel> products = 
            new List<ProductViewModel>()
            {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 20.50M
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 10.49M
                },
                new ProductViewModel()
                {
                    Id = 3,
                    Name = "Meat",
                    Price = 30.50M
                },
                new ProductViewModel()
                {
                    Id = 4,
                    Name = "Milk",
                    Price = 2.5M
                }
            };

        public IActionResult All()
        {
            return View(this.products);
        }
    }
}

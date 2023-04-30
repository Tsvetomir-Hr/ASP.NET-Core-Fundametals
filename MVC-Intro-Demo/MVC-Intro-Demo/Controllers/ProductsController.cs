using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVC_Intro_Demo.Models;
using System.Text;
using System.Text.Json;

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

        [ActionName("My-Products")]
        public IActionResult All()
        {
            return View(this.products);
        }
        public IActionResult ById(int id)
        {
            var product = this.products.FirstOrDefault(p=>p.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }
        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return Json(this.products, options);
        }
        public IActionResult AllAsText()
        {
            var text = string.Empty;
            foreach (var p in this.products)
            {
                text += $"Product {p.Id}: {p.Name} - {p.Price}";
                text += "\r\n";
            }
            return Content(text);
        } 
        public IActionResult AllAsTextFile()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var p in this.products)
            {
                stringBuilder.AppendLine($"Product {p.Id}: {p.Name} - {p.Price:F2}");
            }
            Response.Headers.Add(HeaderNames.ContentDisposition, $"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(stringBuilder.ToString().TrimEnd()),"text/plain");
        }

    }
}

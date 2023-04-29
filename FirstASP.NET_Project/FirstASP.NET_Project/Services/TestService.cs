using FirstASP.NET_Project.Contracts;
using FirstASP.NET_Project.Models;

namespace FirstASP.NET_Project.Services
{
    public class TestService : ITestService
    {
        public string GetProduct(TestModel model)
        {
            return model.Product;
        }
    }
}

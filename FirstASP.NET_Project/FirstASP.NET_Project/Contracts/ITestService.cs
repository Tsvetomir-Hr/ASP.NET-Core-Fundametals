using FirstASP.NET_Project.Models;

namespace FirstASP.NET_Project.Contracts
{
    public interface ITestService
    {
        string GetProduct(TestModel model);
    }
}

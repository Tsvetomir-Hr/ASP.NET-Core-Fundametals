using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using WebShopDemo.Core.Contracts;
using System.Linq;

namespace WebShopDemo.Grpc.Services
{
    public class ProductGrpcService : Product.ProductBase
    {
        private readonly IProductService productService;
        public ProductGrpcService(IProductService _productService)
        {
            this.productService = _productService;
        }
        public override Task<ProductList> GetAll(Empty request, ServerCallContext context)
        {
           ProductList result = new ProductList();
            var products = this.productService.GetAll();

            result.Items.AddRange(products.Select(p => new ProductItem()
            {

            }));
        }
    }
}

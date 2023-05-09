using WebShopDemo.Core.Models;

namespace WebShopDemo.Core.Contracts
{
    /// <summary>
    /// Manipulates products data
    /// </summary>
    public interface IProductService
    {

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of products</returns>
        Task<IEnumerable<ProductDto>> GetAll();

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productDto">Product model</param>
        /// <returns></returns>
        Task Add(ProductDto productDto);

        /// <summary>
        /// Deleting product from the list of products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(Guid id);

        /// <summary>
        /// Buying product of the list of products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Buy(Guid id);
    }
}

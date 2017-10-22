namespace refactor_me.appservices.ServiceInterfaces
{
    using refactor_me.core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IProductService
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>IEnumerable&lt;Product&gt;.</returns>
        IEnumerable<Product> GetAllProducts();
        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product.</returns>
        Product GetProductById(Guid id);
        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void CreateProduct(Product product);
        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void UpdateProduct(Product product);
        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteProduct(Guid id);
    }
}

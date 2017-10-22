namespace refactor_me.appservices.Services
{
    using refactor_me.appservices.ServiceInterfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using refactor_me.core.Models;
    using refactor_me.core.RepositoryInterfaces;

    /// <summary>
    /// Class ProductService.
    /// </summary>
    /// <seealso cref="refactor_me.appservices.ServiceInterfaces.IProductService" />
    public class ProductService : IProductService
    {
        /// <summary>
        /// The product repository
        /// </summary>
        private IProductRepository _productRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void CreateProduct(Product product)
        {
            _productRepository.Create(product);
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteProduct(Guid id)
        {
            _productRepository.Delete(id);
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>IEnumerable&lt;Product&gt;.</returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product.</returns>
        public Product GetProductById(Guid id)
        {
            return _productRepository.GetById(id);
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }
    }
}

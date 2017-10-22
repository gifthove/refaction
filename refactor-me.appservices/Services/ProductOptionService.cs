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
    /// Class ProductOptionService.
    /// </summary>
    /// <seealso cref="refactor_me.appservices.ServiceInterfaces.IProductOptionService" />
    public class ProductOptionService : IProductOptionService
    {
        /// <summary>
        /// The product option repository
        /// </summary>
        private IProductOptionRepository _productOptionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOptionService"/> class.
        /// </summary>
        /// <param name="productOptionRepository">The product option repository.</param>
        public ProductOptionService(IProductOptionRepository productOptionRepository)
        {
            _productOptionRepository = productOptionRepository;
        }

        /// <summary>
        /// Creates the product option.
        /// </summary>
        /// <param name="productOption">The product option.</param>
        public void CreateProductOption(ProductOption productOption)
        {
            _productOptionRepository.Create(productOption);
        }

        /// <summary>
        /// Deletes the product option.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteProductOption(Guid id)
        {
            _productOptionRepository.Delete(id);
        }

        /// <summary>
        /// Gets all product options.
        /// </summary>
        /// <returns>IEnumerable&lt;ProductOption&gt;.</returns>
        public IEnumerable<ProductOption> GetAllProductOptions()
        {
            return _productOptionRepository.GetAll();
        }

        /// <summary>
        /// Gets the product option by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProductOption.</returns>
        public ProductOption GetProductOptionById(Guid id)
        {
            return _productOptionRepository.GetById(id);
        }

        /// <summary>
        /// Updates the product option.
        /// </summary>
        /// <param name="productOption">The product option.</param>
        public void UpdateProductOption(ProductOption productOption)
        {
            _productOptionRepository.Update(productOption);
        }
    }
}

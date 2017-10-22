namespace refactor_me.appservices.ServiceInterfaces
{
    using refactor_me.core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IProductOptionService
    /// </summary>
    public interface IProductOptionService
    {
        /// <summary>
        /// Gets all product options.
        /// </summary>
        /// <returns>IEnumerable&lt;ProductOption&gt;.</returns>
        IEnumerable<ProductOption> GetAllProductOptions();
        /// <summary>
        /// Gets the product option by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProductOption.</returns>
        ProductOption GetProductOptionById(Guid id);
        /// <summary>
        /// Creates the product option.
        /// </summary>
        /// <param name="model">The model.</param>
        void CreateProductOption(ProductOption model);
        /// <summary>
        /// Updates the product option.
        /// </summary>
        /// <param name="model">The model.</param>
        void UpdateProductOption(ProductOption model);
        /// <summary>
        /// Deletes the product option.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteProductOption(Guid id);
    }
}

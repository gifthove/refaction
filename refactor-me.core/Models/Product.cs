namespace refactor_me.core.Models
{
    using System;

    /// <summary>
    /// Class Product.
    /// </summary>
    public class Product
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the delivery price.
        /// </summary>
        /// <value>The delivery price.</value>
        public decimal DeliveryPrice { get; set; }
    }
}
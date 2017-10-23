using refactor_me.core.Models;
using refactor_me.data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Tests.MockDataStore
{
    /// <summary>
    /// Class FakeDatabaseEntitiesContext.
    /// </summary>
    /// <seealso cref="refactor_me.data.IDatabaseEntities" />
    public class FakeDatabaseEntitiesContext : IDatabaseEntities
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDatabaseEntitiesContext"/> class.
        /// </summary>
        public FakeDatabaseEntitiesContext()
        {
            this.Products = new FakeProductSet();
            this.ProductOptions = new FakeProductOptionSet();
        }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        public IDbSet<data.Product> Products { get;  set; }

        /// <summary>
        /// Gets or sets the product options.
        /// </summary>
        /// <value>The product options.</value>
        public IDbSet<data.ProductOption> ProductOptions { get;  set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int SaveChanges()
        {
            return 0;
        }
    }

}


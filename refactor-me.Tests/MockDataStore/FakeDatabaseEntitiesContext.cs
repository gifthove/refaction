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
    public class FakeDatabaseEntitiesContext : IDatabaseEntities
    {
        public FakeDatabaseEntitiesContext()
        {
            this.Products = new FakeProductSet();
            this.ProductOptions = new FakeProductOptionSet();
        }

        public IDbSet<data.Product> Products { get;  set; }

        public IDbSet<data.ProductOption> ProductOptions { get;  set; }

        public int SaveChanges()
        {
            return 0;
        }
    }

}


using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace refactor_me.data
{
    public interface IDatabaseEntities
    {
        IDbSet<ProductOption> ProductOptions { get; set; }
        IDbSet<Product> Products { get; set; }

        int SaveChanges();
    }
}
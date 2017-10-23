namespace refactor_me.data.Repositories
{
    using AutoMapper;
    using Infrastructure.Logging;
    using refactor_me.core.RepositoryInterfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models = refactor_me.core.Models;

    /// <summary>
    /// Class ProductRepository.
    /// </summary>
    /// <seealso cref="refactor_me.core.RepositoryInterfaces.IProductRepository" />
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The database context
        /// </summary>
        private readonly IDatabaseEntities _dbContext;

        /// <summary>
        /// The logging
        /// </summary>
        private readonly ILoggingService _logging;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="dbContext">The database context.</param>
        public ProductRepository(IMapper mapper, IDatabaseEntities dbContext, ILoggingService logging)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logging = logging;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Create(Models.Product model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                // Log to technology specific error store
                _logging.Error(ex);
                // Bubble up exception.
                throw;
            };
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Guid id)
        {
            try
            {
                Product product = _dbContext.Products.AsNoTracking().FirstOrDefault(c => c.Id == id);
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log to technology specific error store
                _logging.Error(ex);
                // Bubble up exception.
                throw;
            };
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;Models.Product&gt;.</returns>
        public IEnumerable<Models.Product> GetAll()
        {   
            try
            {
                var entities = _dbContext.Products.AsNoTracking();
                if (entities == null)
                {
                }
                return _mapper.Map<List<Models.Product>>(entities);
            }
            catch (Exception ex)
            {
                // Log to technology specific error store
                _logging.Error(ex);
                // Bubble up exception.
                throw;
            };
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Models.Product.</returns>
        /// <exception cref="refactor_me.data.Repositories.EntityNotFoundException"></exception>
        public Models.Product GetById(Guid id)
        {
            try
            {
                var entity = _dbContext.Products.AsNoTracking().FirstOrDefault(c => c.Id == id);
                return _mapper.Map<Models.Product>(entity);
            }
            catch (Exception ex)
            {
                // Log to technology specific error store
                _logging.Error(ex);
                // Bubble up exception.
                throw;
            };
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Models.Product model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                var original = _dbContext.Products.Find(product.Id);
                original.Name = product.Name;
                original.Price = product.Price;
                original.DeliveryPrice = product.DeliveryPrice;
                original.Description = product.Description;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log to technology specific error store
                _logging.Error(ex);
                // Bubble up exception.
                throw;
            };
        }
    }
}

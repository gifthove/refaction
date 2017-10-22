namespace refactor_me.data.Repositories
{
    using refactor_me.core.RepositoryInterfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using System.Data.Entity;
    using refactor_me.Infrastructure;
    using data;
    using Models = refactor_me.core.Models;
    using Infrastructure.Logging;


    /// <summary>
    /// Class ProductOptionRepository.
    /// </summary>
    /// <seealso cref="refactor_me.core.RepositoryInterfaces.IProductOptionRepository" />
    public class ProductOptionRepository : IProductOptionRepository
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;
        /// <summary>
        /// The database context
        /// </summary>
        private readonly IDatabaseEntities _dbContext;
        /// <summary>
        /// The logging
        /// </summary>
        private readonly ILoggingService _logging;


        /// <summary>
        /// Initializes a new instance of the <see cref="ProductOptionRepository"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logging">The logging.</param>
        public ProductOptionRepository(IMapper mapper, IDatabaseEntities dbContext, ILoggingService logging)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Create(Models.ProductOption model)
        {
            try
            {
                var productOption = _mapper.Map<ProductOption>(model);
                _dbContext.ProductOptions.Add(productOption);
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
                ProductOption productOption = _dbContext.ProductOptions.AsNoTracking().FirstOrDefault(c => c.Id == id);
                _dbContext.ProductOptions.Remove(productOption);
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
        /// <returns>IEnumerable&lt;Models.ProductOption&gt;.</returns>
        public IEnumerable<Models.ProductOption> GetAll()
        {
            try
            {
                var entities = _dbContext.ProductOptions.AsNoTracking();
                return _mapper.Map<List<Models.ProductOption>>(entities);
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
        /// <returns>Models.ProductOption.</returns>
        /// <exception cref="refactor_me.data.Repositories.EntityNotFoundException"></exception>
        public Models.ProductOption GetById(Guid id)
        {
            try
            {
                var entity = _dbContext.ProductOptions.AsNoTracking().FirstOrDefault(c => c.Id == id);
                return _mapper.Map<Models.ProductOption>(entity);
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
        public void Update(Models.ProductOption model)
        {
            try
            {
                using (var context = new DatabaseEntities())
                {
                    var productOption = _mapper.Map<ProductOption>(model);

                    var original = _dbContext.ProductOptions.Find(productOption.Id);
                    original.Name = productOption.Name;
                    original.ProductId = productOption.ProductId;
                    original.Description = productOption.Description;
                    _dbContext.SaveChanges();
                }
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

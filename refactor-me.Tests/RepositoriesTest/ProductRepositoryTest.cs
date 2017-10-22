namespace refactor_me.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using refactor_me.data;
    using refactor_me.data.Repositories;
    using AutoMapper;
    using refactor_me.data.Mapper;
    using refactor_me.Tests.MockDataStore;
    using System.Linq;
    using Infrastructure.Logging;

    /// <summary>
    /// Class ProductRepositoryTest.
    /// </summary>
    [TestClass]
    public class ProductRepositoryTest
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private  IMapper _mapper;

        /// <summary>
        /// The mock context
        /// </summary>
        private IDatabaseEntities _mockContext;

        /// <summary>
        /// The logging
        /// </summary>
        private  ILoggingService _logging;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //Arrange for Tests
            var mapperConfig = new MapperConfiguration(cnfig =>
            {
                cnfig.AddProfile(new RepositoryMapper());
            });

            _mapper = mapperConfig.CreateMapper();

            _mockContext = new FakeDatabaseEntitiesContext
            {
                Products =
                {
                    new data.Product { Id= new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"), Name="Samsung Galaxy S7", Description="Newest mobile product from Samsung.", Price=1024.99M, DeliveryPrice=16.99M},
                    new data.Product { Id= new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), Name="Apple iPhone 6S", Description="Newest mobile product from Apple.", Price=1299.99M,  DeliveryPrice=15.99M}
                }
            };

            _logging = new LoggingService();

        }

        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
           
        }

        /// <summary>
        /// Creates the product saves a product via context test.
        /// </summary>
        [TestMethod]
        public void Create_saves_a_Product_via_context()
        {
            //Arrange 
            var repository = new ProductRepository(_mapper, _mockContext, _logging);
            var product = new core.Models.Product { Name = "TestMockProduct", Price = 100.00M, Description = "Mock Text Product", DeliveryPrice = 10.00M, Id = new Guid() };

            //Act 
            repository.Create(product);
            var count = _mockContext.Products.Local.Count;

            //Assert
            Assert.AreEqual(count, 3);        
         
        }

        /// <summary>
        /// Gets the by identifier gets a product via context test.
        /// </summary>
        [TestMethod]
        public void GetById_gets_a_Product_via_context()
        {
            //Arrange
            var repository = new ProductRepository(_mapper, _mockContext, _logging);

            //Act 
            var product = repository.GetById(new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));
          
            //Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(product.Name, "Samsung Galaxy S7");

        }

        /// <summary>
        /// Gets the by identifier gets a product via context test.
        /// </summary>
        [TestMethod]
        public void GetByAll_gets_all_Products_via_context()
        {
            //Arrange
            var repository = new ProductRepository(_mapper, _mockContext, _logging);


            //Act 
            var products = repository.GetAll().ToList();

            //Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products.Count,2);

        }

        /// <summary>
        /// Updates the update specified product via context test.
        /// </summary>
        [TestMethod]
        public void Update_update_specified_Product_via_context()
        {
            //Arrange
            var repository = new ProductRepository(_mapper, _mockContext, _logging);
            var productToUpdate = new core.Models.Product { Id = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), Name = "Apple iPhone 6S Test Mock", Description = "Newest mobile product from Apple.", Price = 1299.99M, DeliveryPrice =20M };

            //Act 
            repository.Update(productToUpdate);
            var updatedProduct = repository.GetById(new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"));

            //Assert
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(updatedProduct.Name, "Apple iPhone 6S Test Mock");
            Assert.AreEqual(updatedProduct.Name, productToUpdate.Name);
            Assert.AreEqual(updatedProduct.DeliveryPrice, productToUpdate.DeliveryPrice);
        }

        /// <summary>
        /// Deletes the deletes a product via context delete.
        /// </summary>
        [TestMethod]
        public void Delete_deletes_a_Product_via_context()
        {
            //Arrange
            var repository = new ProductRepository(_mapper, _mockContext, _logging);

            //Act
            repository.Delete(new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));
            var products = repository.GetAll();

            //Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products.Count(),1);
        }
    }
}

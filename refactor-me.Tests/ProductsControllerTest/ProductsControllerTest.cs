using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.core.Models;
using refactor_me.data;
using Moq;
using System.Data.Entity;
using refactor_me.data.Repositories;
using AutoMapper;
using refactor_me.data.Mapper;
using refactor_me.Tests.MockDataStore;
using System.Collections.Generic;
using System.Linq;
using refactor_me.appservices.Services;
using refactor_me.Controllers;
using refactor_me.Infrastructure.Logging;

namespace refactor_me.Tests
{
    /// <summary>
    /// Class ProductOptionRepositoryTest.
    /// </summary>
    [TestClass]
    public class ProductsControllerTest
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;
        /// <summary>
        /// The mock context
        /// </summary>
        private IDatabaseEntities _mockContext;

        /// <summary>
        /// The controller
        /// </summary>
        private ProductsController _Controller;

        /// <summary>
        /// The logging
        /// </summary>
        private ILoggingService _logging;

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
                },
                ProductOptions =
                {
                    new data.ProductOption { Id= new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9"),ProductId=new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),Name="White",Description="White Samsung Galaxy S7"},
                    new data.ProductOption { Id= new Guid("a21d5777-a655-4020-b431-624bb331e9a2"),ProductId=new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),Name="Black",   Description="Black Samsung Galaxy S7" },
                    new data.ProductOption { Id= new Guid("5c2996ab-54ad-4999-92d2-89245682d534"),ProductId=new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),Name="Rose Gold",Description="Gold Apple iPhone 6S" }
                }
            };
            _logging = new LoggingService();
            var productOptionRepository = new ProductOptionRepository(_mapper, _mockContext,_logging);
            var productRepository = new ProductRepository(_mapper, _mockContext, _logging);

            var productOptionService = new ProductOptionService(productOptionRepository);
            var productService = new ProductService(productRepository);
            _Controller = new ProductsController(productService, productOptionService);
        }

        /// <summary>
        /// Gets the gets all products via service.
        /// </summary>
        [TestMethod]
        public void Get_gets_all_Products_via_service()
        {
            //Act 
            var products= _Controller.Get();

            //Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products.Items.Count, 2);

        }

        /// <summary>
        /// Searches the by name gets all products that match a name via service.
        /// </summary>
        [TestMethod]
        public void SearchByName_gets_all_Products_that_match_a_Name_via_service()
        {
            //Act 
            var products = _Controller.SearchByName("Samsung Galaxy S7");

            //Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products.Items.Count, 1);

        }

        /// <summary>
        /// Searches the by name gets a product by name via context.
        /// </summary>
        [TestMethod]
        public void GetProduct_gets_a_Product_by_id_via_service()
        {
            //Act 
            var product = _Controller.GetProduct(new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            //Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(product.Name, "Samsung Galaxy S7");
            Assert.AreEqual(product.Description, "Newest mobile product from Samsung.");

        }

        /// <summary>
        /// Creates the productOption saves a productOption via context test.
        /// </summary>
        [TestMethod]
        public void Create_saves_a_Product_via_service()
        {
            //Arrange 
            var product = new core.Models.Product { Name = "TestMockProduct", Price = 100.00M, Description = "Mock Text Product", DeliveryPrice = 10.00M, Id = new Guid() };

            //Act 
            _Controller.Create(product);
            var products = _Controller.Get();

            //Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products.Items.Count, 3);

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.data;
using refactor_me.data.Repositories;
using AutoMapper;
using refactor_me.data.Mapper;
using refactor_me.Tests.MockDataStore;
using System.Linq;
using refactor_me.Infrastructure.Logging;
using refactor_me.appservices.ServiceInterfaces;
using refactor_me.appservices.Services;

namespace refactor_me.Tests
{
    /// <summary>
    /// Class ProductOptionRepositoryTest.
    /// </summary>
    [TestClass]
    public class ProductOptionServiceTest
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// The mock repository
        /// </summary>
        private IDatabaseEntities _mockContext;

        /// <summary>
        /// The logging
        /// </summary>
        private ILoggingService _logging;

        /// <summary>
        /// The product service
        /// </summary>
        private IProductOptionService _productOptionService;

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
            var productOptionRepository = new ProductOptionRepository(_mapper, _mockContext, _logging);
            var productRepository = new ProductRepository(_mapper, _mockContext, _logging);
            _productOptionService = new ProductOptionService(productOptionRepository);
        }

        /// <summary>
        /// Creates the product option saves a product option via repository.
        /// </summary>
        [TestMethod]
        public void CreateProductOption_saves_a_ProductOption_via_repository()
        {
            //Arrange 
            var productOption = new core.Models.ProductOption { Id = new Guid(), ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), Name = "Rose Gold", Description = "Gold Apple iPhone 6S" };

            //Act 
            _productOptionService.CreateProductOption(productOption);
            var count = _mockContext.ProductOptions.Local.Count;

            //Assert
            Assert.AreEqual(count, 4);

        }

        /// <summary>
        /// Gets the product option by identifier gets a product option via repository.
        /// </summary>
        [TestMethod]
        public void GetProductOptionById_gets_a_ProductOption_via_repository()
        {
           
            //Act 
            var productOption = _productOptionService.GetProductOptionById(new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));

            //Assert
            Assert.IsNotNull(productOption);
            Assert.AreEqual(productOption.Name, "White");

        }

        /// <summary>
        /// Gets all product options gets all product options via repository.
        /// </summary>
        [TestMethod]
        public void GetAllProductOptions_gets_all_ProductOptions_via_repository()
        {
            //Act 
            var productOptions = _productOptionService.GetAllProductOptions().ToList();

            //Assert
            Assert.IsNotNull(productOptions);
            Assert.AreEqual(productOptions.Count, 3);

        }

        /// <summary>
        /// Updates the product option update specified product option via repository.
        /// </summary>
        [TestMethod]
        public void UpdateProductOption_update_specified_ProductOption_via_repository()
        {
            //Arrange
            var productOptionToUpdate = new core.Models.ProductOption { Id = new Guid("5c2996ab-54ad-4999-92d2-89245682d534"), ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), Name = "Rose Gold Test Mock", Description = "Gold Apple iPhone 6S" };

            //Act 
            _productOptionService.UpdateProductOption(productOptionToUpdate);
            var updatedProductOption = _productOptionService.GetProductOptionById(new Guid("5c2996ab-54ad-4999-92d2-89245682d534"));

            //Assert
            Assert.IsNotNull(updatedProductOption);
            Assert.AreEqual(updatedProductOption.Name, "Rose Gold Test Mock");
            Assert.AreEqual(updatedProductOption.Name, productOptionToUpdate.Name);
        }

        /// <summary>
        /// Deletes the product option deletes a product option via repository.
        /// </summary>
        [TestMethod]
        public void DeleteProductOption_deletes_a_ProductOption_via_repository()
        {

            //Act
            _productOptionService.DeleteProductOption(new Guid("a21d5777-a655-4020-b431-624bb331e9a2"));
            var productOptions = _productOptionService.GetAllProductOptions();

            //Assert
            Assert.IsNotNull(productOptions);
            Assert.AreEqual(productOptions.Count(), 2);
        }
    }
}

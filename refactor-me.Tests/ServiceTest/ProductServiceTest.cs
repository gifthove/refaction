namespace refactor_me.Tests.ServiceTest
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
    using appservices.ServiceInterfaces;
    using appservices.Services;

    /// <summary>
    /// Class ProductRepositoryTest.
    /// </summary>
    [TestClass]
    public class ProductServiceTest
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
        /// The logging
        /// </summary>
        private ILoggingService _logging;

        private IProductService _productService;

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
            _productService = new ProductService(productRepository);
        }

        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
           
        }

        /// <summary>
        /// Creates the product saves a product via repository.
        /// </summary>
        [TestMethod]
        public void CreateProduct_saves_a_Product_via_repository()
        {
            //Arrange 
            var product = new core.Models.Product { Name = "TestMockProduct", Price = 100.00M, Description = "Mock Text Product", DeliveryPrice = 10.00M, Id = new Guid() };

            //Act 
            _productService.CreateProduct(product);
            var count = _mockContext.Products.Local.Count;

            //Assert
            Assert.AreEqual(count, 3);        
         
        }

        /// <summary>
        /// Gets the product by identifier gets a product via repository.
        /// </summary>
        [TestMethod]
        public void GetProductById_gets_a_Product_via_repository()
        {
            //Arrange
            //Act 
            var product = _productService.GetProductById(new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));
          
            //Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(product.Name, "Samsung Galaxy S7");

        }

        /// <summary>
        /// Gets all products gets all products via repository.
        /// </summary>
        [TestMethod]
        public void GetAllProducts_gets_all_Products_via_repository()
        {
            //Arrange
            //Act 
            var products = _productService.GetAllProducts().ToList();

            //Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products.Count,2);

        }

        /// <summary>
        /// Updates the product update specified product via repository.
        /// </summary>
        [TestMethod]
        public void UpdateProduct_update_specified_Product_via_repository()
        {
            //Arrange
            var productToUpdate = new core.Models.Product { Id = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), Name = "Apple iPhone 6S Test Mock", Description = "Newest mobile product from Apple.", Price = 1299.99M, DeliveryPrice =20M };

            //Act 
            _productService.UpdateProduct(productToUpdate);
            var updatedProduct = _productService.GetProductById(new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"));

            //Assert
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(updatedProduct.Name, "Apple iPhone 6S Test Mock");
            Assert.AreEqual(updatedProduct.Name, productToUpdate.Name);
            Assert.AreEqual(updatedProduct.DeliveryPrice, productToUpdate.DeliveryPrice);
        }

        /// <summary>
        /// Deletes the product deletes a product via repository.
        /// </summary>
        [TestMethod]
        public void DeleteProduct_deletes_a_Product_via_repository()
        {
            //Arrange

            //Act
            _productService.DeleteProduct(new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));
            var products = _productService.GetAllProducts();

            //Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(products.Count(),1);
        }
    }
}

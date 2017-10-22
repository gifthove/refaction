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
using refactor_me.Infrastructure.Logging;

namespace refactor_me.Tests
{
    /// <summary>
    /// Class ProductOptionRepositoryTest.
    /// </summary>
    [TestClass]
    public class ProductOptionRepositoryTest
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
                ProductOptions =
                {
                    new data.ProductOption { Id= new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9"),ProductId=new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),Name="White",Description="White Samsung Galaxy S7"},
                    new data.ProductOption { Id= new Guid("a21d5777-a655-4020-b431-624bb331e9a2"),ProductId=new Guid("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"),Name="Black",   Description="Black Samsung Galaxy S7" },
                    new data.ProductOption { Id= new Guid("5c2996ab-54ad-4999-92d2-89245682d534"),ProductId=new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"),Name="Rose Gold",Description="Gold Apple iPhone 6S" }
                }
            };

            _logging = new LoggingService();

        }

        /// <summary>
        /// Creates the productOption saves a productOption via context test.
        /// </summary>
        [TestMethod]
        public void Create_saves_a_ProductOption_via_context()
        {
            //Arrange 
            var repository = new ProductOptionRepository(_mapper, _mockContext,_logging);
            var productOption = new core.Models.ProductOption { Id = new Guid(), ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), Name = "Rose Gold", Description = "Gold Apple iPhone 6S" };

            //Act 
            repository.Create(productOption);
            var count = _mockContext.ProductOptions.Local.Count;

            //Assert
            Assert.AreEqual(count, 4);        
         
        }

        /// <summary>
        /// Gets the by identifier gets a productOption via context test.
        /// </summary>
        [TestMethod]
        public void GetById_gets_a_ProductOption_via_context()
        {
            //Arrange
            var repository = new ProductOptionRepository(_mapper, _mockContext,_logging);

            //Act 
            var productOption = repository.GetById(new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));
          
            //Assert
            Assert.IsNotNull(productOption);
            Assert.AreEqual(productOption.Name, "White");

        }

        /// <summary>
        /// Gets the by identifier gets a productOption via context test.
        /// </summary>
        [TestMethod]
        public void GetByAll_gets_all_ProductOptions_via_context()
        {
            //Arrange
            var repository = new ProductOptionRepository(_mapper, _mockContext,_logging);


            //Act 
            var productOptions = repository.GetAll().ToList();

            //Assert
            Assert.IsNotNull(productOptions);
            Assert.AreEqual(productOptions.Count,3);

        }

        /// <summary>
        /// Updates the update specified productOption via context test.
        /// </summary>
        [TestMethod]
        public void Update_update_specified_ProductOption_via_context()
        {
            //Arrange
            var repository = new ProductOptionRepository(_mapper, _mockContext,_logging);
            var productOptionToUpdate = new core.Models.ProductOption {  Id = new Guid("5c2996ab-54ad-4999-92d2-89245682d534"), ProductId = new Guid("de1287c0-4b15-4a7b-9d8a-dd21b3cafec3"), Name = "Rose Gold Test Mock", Description = "Gold Apple iPhone 6S" };

            //Act 
            repository.Update(productOptionToUpdate);
            var updatedProductOption = repository.GetById(new Guid("5c2996ab-54ad-4999-92d2-89245682d534"));

            //Assert
            Assert.IsNotNull(updatedProductOption);
            Assert.AreEqual(updatedProductOption.Name, "Rose Gold Test Mock");
            Assert.AreEqual(updatedProductOption.Name, productOptionToUpdate.Name);
        }

        /// <summary>
        /// Deletes the deletes a productOption via context delete.
        /// </summary>
        [TestMethod]
        public void Delete_deletes_a_ProductOption_via_context()
        {
            //Arrange
            var repository = new ProductOptionRepository(_mapper, _mockContext,_logging);

            //Act
            repository.Delete(new Guid("a21d5777-a655-4020-b431-624bb331e9a2"));
            var productOptions = repository.GetAll();

            //Assert
            Assert.IsNotNull(productOptions);
            Assert.AreEqual(productOptions.Count(),2);
        }
    }
}

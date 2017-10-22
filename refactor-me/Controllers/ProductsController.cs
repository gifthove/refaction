// ***********************************************************************
// Assembly         : refactor-me
// Author           :
// Created          : 
//
// Last Modified By : Gift.Hove
// Last Modified On : 10-22-2017
// ***********************************************************************
// <copyright file="ProductsController.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace refactor_me.Controllers
{
    using Infrastructure.Logging;
    using refactor_me.appservices.ServiceInterfaces;
    using refactor_me.core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    /// <summary>
    /// Class ProductsController.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {

        /// <summary>
        /// The product service
        /// </summary>
        private readonly IProductService _productService;

        /// <summary>
        /// The product option service
        /// </summary>
        private readonly IProductOptionService _productOptionService;
        /// <summary>
        /// The logging service
        /// </summary>
        private readonly ILoggingService _loggingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        /// <param name="productOptionService">The product option service.</param>
        public ProductsController(
            IProductService productService,
            IProductOptionService productOptionService,
            ILoggingService loggingService)
        {
            _productService = productService;
            _productOptionService = productOptionService;
            _loggingService = loggingService;

        }


        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>products</returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [Route]
        [HttpGet]
        public Data<List<Product>> Get()
        {
            try
            {
                Data<List<Product>> results = new Data<List<Product>>();
                if (results == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                results.Items = _productService.GetAllProducts().ToList();
                return results;
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// Searches by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>products</returns>
        [Route]
        [HttpGet]
        public Data<List<Product>> SearchByName(string name)
        {
            try
            {
                Data<List<Product>> results = new Data<List<Product>>();
                if (results == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                results.Items = _productService.GetAllProducts().Where(p => p.Name == name).ToList();
                return results;
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                throw;
            }
        }


        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Product.</returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                return product;
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            _productService.CreateProduct(product);
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="product">The product.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            try
            {

                var original = _productService.GetProductById(id);

                if (original != null)
                {
                    original.Name = product.Name;
                    original.Description = product.Description;
                    original.Price = product.Price;
                    original.DeliveryPrice = product.DeliveryPrice;
                    _productService.UpdateProduct(product);
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            _productService.DeleteProduct(id);
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>The list of product options.</returns>
        [Route("{productId}/options")]
        [HttpGet]
        public Data<List<ProductOption>> GetOptions(Guid productId)
        {
            Data<List<ProductOption>> results = new Data<List<ProductOption>>();
             results.Items = _productOptionService.GetAllProductOptions().Where(p => p.ProductId == productId).ToList();
            return results;
        }

        /// <summary>
        /// Gets the option.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="id">The option identifier.</param>
        /// <returns>ProductOption.</returns>
        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {

            return _productOptionService.GetAllProductOptions().Where(p => p.Id == id && p.ProductId == productId).FirstOrDefault();
        }

        /// <summary>
        /// Creates the option.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="option">The option.</param>
        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            _productOptionService.CreateProductOption(option);
        }

        /// <summary>
        /// Updates the option.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="option">The option.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            try
            {
                var original = _productOptionService.GetProductOptionById(id);

                if (original != null)
                {
                    original.Name = option.Name;
                    original.Description = option.Description;
                    original.ProductId = option.ProductId;
                    _productOptionService.UpdateProductOption(original);
                }
                else
                {
                    //
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes the option.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            _productOptionService.DeleteProductOption(id); ;
        }
    }
}

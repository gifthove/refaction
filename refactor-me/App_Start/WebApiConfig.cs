namespace refactor_me
{
    using AutoMapper;
    using refactor_me.appservices.ServiceInterfaces;
    using refactor_me.appservices.Services;
    using refactor_me.core.RepositoryInterfaces;
    using refactor_me.data.Mapper;
    using refactor_me.data.Repositories;
    using Infrastructure.Logging;
    using refactor_me.Resolver;
    using System.Web.Http;
    using UnityNLogExtension.NLog;
    using Microsoft.Practices.Unity;
    using data;
    using System.Data.Entity;

    /// <summary>
    /// Class WebApiConfig.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            var mapperConfig = new MapperConfiguration(cnfig =>
            {
                cnfig.AddProfile(new RepositoryMapper());
            });

            // UNITY DI
            var container = new UnityContainer();
            container.RegisterInstance<IMapper>(mapperConfig.CreateMapper());
            //container.AddNewExtension<NLogExtension<LoggingService>>();
            //container.AddNewExtension<NLogExtension<LoggingService>>();
            container.RegisterType<ILoggingService, LoggingService>();

            container.RegisterType<IDatabaseEntities, DatabaseEntities>(new PerThreadLifetimeManager());

            container.RegisterType<DbContext, DatabaseEntities>(new PerThreadLifetimeManager());
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductOptionRepository, ProductOptionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductService, ProductService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductOptionService, ProductOptionService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

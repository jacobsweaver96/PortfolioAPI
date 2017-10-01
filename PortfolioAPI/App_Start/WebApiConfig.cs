using PortfolioAPI.DataServices;
using PortfolioAPI.Interfaces;
using PortfolioAPI.Models;
using PortfolioAPI.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using log4net;
using PortfolioAPI.DataServices.DataServiceInterfaces;

namespace PortfolioAPI
{
    /// <summary>
    /// Configures the API
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the configuration
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            ILog log = LogManager.GetLogger(ConfigurationManager.AppSettings.Get("LoggerName"));

            // Fail fast
            if (log == null)
            {
                throw new ConfigurationErrorsException();
            }

            DependencyResolver.SetService<ILog>(log);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Database connection
            var pfConnString = ConfigurationManager.ConnectionStrings["PortfolioDBConnectionString"].ConnectionString;
            var credentials = CredentialManager.GetCredentials("PortfolioDBConnectionString");
            pfConnString = $"{pfConnString};User Id={credentials.UserId};Password={credentials.Password}";
            var dbCtx = new PortfolioDBDataContext(pfConnString);
            PortfolioDataAccessService portfolioService = new PortfolioDataAccessService(dbCtx);
            DependencyResolver.SetService<IDataAccessService>(portfolioService);

            PortfolioAuthorizationService authorizationService = new PortfolioAuthorizationService(dbCtx);
            DependencyResolver.SetService<IAuthorizationService>(authorizationService);
        }
    }
}

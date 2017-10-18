using PortfolioAPI.DataServices;
using PortfolioAPI.Interfaces;
using PortfolioAPI.Models;
using System.Configuration;
using System.Web.Http;
using log4net;
using PortfolioAPI.DataServices.DataServiceInterfaces;
using SandyUtils.Utils;
using RestEasy.Services;

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
            var pfConnString = ConfigurationManager.ConnectionStrings["PortfolioDBEntities"].ConnectionString;
            var credentialsFilePath = $"{ConfigurationManager.AppSettings["CredentialsFileName"]}";
            var credentials = CredentialManager.GetCredentials("PortfolioDBEntities", credentialsFilePath);
            pfConnString = pfConnString.Replace("{userid}", credentials.UserId)
                                        .Replace("{password}", credentials.Password);
            PortfolioDataAccessService portfolioService = new PortfolioDataAccessService(pfConnString);
            DependencyResolver.SetService<IDataAccessService>(portfolioService);

            PortfolioAuthorizationService authorizationService = new PortfolioAuthorizationService(pfConnString);
            DependencyResolver.SetService<IAuthorizationService>(authorizationService);
        }
    }
}

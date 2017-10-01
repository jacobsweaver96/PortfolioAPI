using PortfolioAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Configuration;
using System.Runtime.CompilerServices;
using PortfolioAPI.Util;
using PortfolioAPI.Controllers.Interfaces;
using System.Reflection;
using PortfolioAPI.ControllerAttributes;
using ApiModels.Models.Response;
using ApiModels.Models;
using PortfolioAPI.Interfaces;
using log4net;
using PortfolioAPI.DataServices.DataServiceInterfaces;
using System.Net;
using PortfolioAPI.DataServices.DataAccessors.Class;

namespace PortfolioAPI.Controllers
{
    /// <summary>
    /// Abstract controller for handling many of the REST requirements
    /// </summary>
    public abstract class RestController : ApiController, IRESTful
    {
        /// <summary>
        /// Gets the endpoints set in this controller
        /// </summary>
        /// <returns></returns>
        public abstract ApiResponse Paths();

        /// <summary>
        /// Endpoints that are related to the endpoint of the current http context 
        /// </summary>
        protected abstract List<RestController> RelatedEndpoints { get; }

        /// <summary>
        /// The service used for data access
        /// </summary>
        protected IDataAccessService DataAccessService
        {
            get
            {
                return DependencyResolver.Resolve<IDataAccessService>();
            }
        }

        /// <summary>
        /// The service used for authorization
        /// </summary>
        protected IAuthorizationService AuthorizationService
        {
            get
            {
                return DependencyResolver.Resolve<IAuthorizationService>();
            }
        }

        /// <summary>
        /// The logger
        /// </summary>
        protected ILog Log
        {
            get
            {
                return DependencyResolver.Resolve<ILog>();
            }
        }

        /// <summary>
        /// Creates a response consisting solely of routing information
        /// </summary>
        /// <param name="method">The calling method</param>
        /// <returns>An API formatted response</returns>
        protected ApiResponse CreateRestResponse([CallerMemberName] string method = null)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                response.EndPointItems = GetRelatedRoutes();
                response.ResponseCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception while getting related routing information for method {method}", ex);
                response.ResponseCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        /// <summary>
        /// Create a response without a return type
        /// </summary>
        /// <param name="dataExec">The data interaction function</param>
        /// <param name="method">The calling method</param>
        /// <returns>An API formatted response</returns>
        protected ApiResponse CreateRestResponse(Func<DataResponse> dataExec, [CallerMemberName] string method = null)
        {
            return CreateRestResponse<object, object>(dataExec);
        }

        /// <summary>
        /// Creates a response that returns data to the client
        /// </summary>
        /// <typeparam name="T">The type of data returned to the client</typeparam>
        /// <typeparam name="U">The type of serializable data returned to the client</typeparam>
        /// <param name="dataExec">Function for getting data as a database model</param>
        /// <param name="transExec">Function for tranforming the database model to a serializable model</param>
        /// <param name="method">The calling method</param>
        /// <returns>An API formatted response</returns>
        protected ApiResponse CreateRestResponse<T,U>(Func<DataResponse> dataExec, Func<T,U> transExec = null, [CallerMemberName] string method = null)
        {
            ApiResponse response = new ApiResponse();
            DataResponse ret;

            MethodInfo mInfo = GetType().GetMethod(method);

            List<PermissionLevel> requiredPermissions = new List<PermissionLevel>();

            foreach (var v in mInfo.CustomAttributes)
            {
                if (v.AttributeType == typeof(RequiresReadAccessAttribute))
                {
                    requiredPermissions.Add(PermissionLevel.READ);
                    continue;
                }
                if (v.AttributeType == typeof(RequiresWriteAccessAttribute))
                {
                    requiredPermissions.Add(PermissionLevel.WRITE);
                    continue;
                }
                if (v.AttributeType == typeof(RequiresAdminAccessAttribute))
                {
                    requiredPermissions.Add(PermissionLevel.ADMIN);
                    continue;
                }
            }

            try
            {
                var authHeader = ActionContext.Request.Headers.Authorization?.Parameter;

                // Only allow https
                if (ActionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
                {
                    response.ResponseCode = HttpStatusCode.Forbidden;

                    if (ActionContext.Request.RequestUri.Scheme == Uri.UriSchemeHttp &&
                        !String.IsNullOrWhiteSpace(authHeader) && authHeader.Length == 32)
                    {
                        Log.Warn($"Client key beginning with {authHeader.Substring(0, 10)}... was receieved over an insecure connection");
                    }
                }
                else if (AuthorizationService.Authorize(authHeader, requiredPermissions))
                {
                    ret = dataExec();
                    switch (ret.Status)
                    {
                        case DataStatusCode.SUCCESS:
                            response.ResponseCode = HttpStatusCode.OK;
                            break;
                        case DataStatusCode.INVALID:
                            response.ResponseCode = HttpStatusCode.BadRequest;
                            break;
                        case DataStatusCode.ERROR:
                            response.ResponseCode = HttpStatusCode.InternalServerError;
                            break;
                        default:
                            response.ResponseCode = HttpStatusCode.NotImplemented;
                            break;
                    }

                    if (ret is DataResponse<T>)
                    {
                        var typedRet = (DataResponse<T>)ret;
                        response.Content = typedRet.HasValue ? (object)transExec(typedRet.Value) : null;
                    }
                }
                else
                {
                    response.ResponseCode = HttpStatusCode.Unauthorized;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Exception occurred while creating an api response; Calling method name: {method}", ex);
                response.ResponseCode = HttpStatusCode.InternalServerError;
            }

            response.EndPointItems = GetRelatedRoutes();

            return response;
        }

        private List<EndPointItem> GetRelatedRoutes()
        {
            List<EndPointItem> endpointObjs = new List<EndPointItem>();
            MethodInfo[] infos = GetType().GetMethods();
            Uri requestUri = ActionContext.Request.RequestUri;

            foreach (var v in infos.Where(w => w.IsPublic))
            {
                RouteAttribute route = v.GetCustomAttribute<RouteAttribute>();
                RestInfoAttribute restInfo = v.GetCustomAttribute<RestInfoAttribute>();
                RoutePrefixAttribute routePrefix = v.DeclaringType.GetCustomAttribute<RoutePrefixAttribute>();

                bool isRouteAbsolute = (route?.Template.FirstOrDefault() == '~');
                string fullRoute = $"{requestUri.Host}:{requestUri.Port}";

                if (routePrefix == null)
                {
                    Log.Warn($"The controller {v.DeclaringType.ToString()} doesn't have a route prefix attribute");
                }
                else if (!isRouteAbsolute && routePrefix.Prefix.Length > 0)
                {
                    string _prefix;
                    if (routePrefix.Prefix[0] == '~')
                    {
                        _prefix = new string(routePrefix.Prefix.Skip(1).ToArray());
                    }
                    else
                    {
                        _prefix = routePrefix.Prefix;
                    }

                    fullRoute += $"/{_prefix}";
                }

                if (route == null)
                {
                    Log.Warn($"The method {v.Name} of the controller {v.DeclaringType.ToString()} doesn't have a route attribute");
                }
                else if (route.Template.Length > 0)
                {
                    string _routeStr;
                    if (route.Template[0] == '~')
                    {
                        _routeStr = new string(route.Template.Skip(1).ToArray());
                    }
                    else
                    {
                        _routeStr = $"/{route.Template}";
                    }

                    fullRoute += $"{_routeStr}";
                }

                if (restInfo != null)
                {
                    endpointObjs.Add(new EndPointItem(restInfo.RestHttpMethod, fullRoute, restInfo.RestDescription, restInfo.RequiresModel ? restInfo.RestModel : null));
                }
            }

            foreach (var endpoint in RelatedEndpoints)
            {
                try
                {
                    endpoint.ActionContext = ActionContext;
                    endpointObjs.AddRange(endpoint.GetRelatedRoutes());
                }
                catch (Exception ex)
                {
                    Log.Error($"There was an exception while getting the endpoint information for the controller {endpoint.GetType().DeclaringType.ToString()}", ex);
                }
            }

            return endpointObjs;
        }
    }
}
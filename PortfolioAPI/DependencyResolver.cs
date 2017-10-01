using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using log4net;

namespace PortfolioAPI
{
    public static class DependencyResolver
    {
        private static Dictionary<Type, object> _resolveDict = new Dictionary<Type, object>();
        private static ILog logger => LogManager.GetLogger("root");

        /// <summary>
        /// Resolves the service defined by the provided interface
        /// </summary>
        /// <typeparam name="T">The interface of the service being resolved</typeparam>
        /// <returns>An instance of the requested service</returns>
        public static T Resolve<T>()
        {
            Type serviceType = typeof(T);

            if (!_resolveDict.Keys.Contains(serviceType))
            {
                throw new Exception();
            }

            return (T)_resolveDict[serviceType];
        }

        /// <summary>
        /// Sets the instance of the given service type
        /// </summary>
        /// <typeparam name="T">The service interface</typeparam>
        /// <param name="service">The instance of the service</param>
        public static void SetService<T>(T service)
        {
            Type serviceType = typeof(T);

            if (!serviceType.IsInterface)
            {
                throw new Exception();
            }

            _resolveDict[serviceType] = service;
        }
    }
}
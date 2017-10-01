﻿using log4net;
using PortfolioAPI.DataServices.DataAccessors;
using PortfolioAPI.DataServices.DataServiceInterfaces;
using PortfolioAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioAPI.DataServices
{
    /// <summary>
    /// Service for authorizing clients
    /// </summary>
    public class PortfolioAuthorizationService : IAuthorizationService
    {
        private PortfolioDBDataContext DbContext { get; set; }
        private ILog logger
        {
            get { return DependencyResolver.Resolve<ILog>(); }
        }

        private readonly ClientDataAccessor ClientAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">Injects the data context</param>
        public PortfolioAuthorizationService(PortfolioDBDataContext ctx)
        {
            ClientAccessor = new ClientDataAccessor(ctx);
        }

        /// <summary>
        /// Determine whether or not a client is authorized to perform an action
        /// </summary>
        /// <param name="clientKey">The client's identifying key</param>
        /// <param name="requiredPermissionLevel">The required permissions to perform the action</param>
        /// <returns>Authorization determinator</returns>
        public bool Authorize(string clientKey, PermissionLevel requiredPermissionLevel)
        {
            var response = ClientAccessor.GetClient(clientKey);

            if (response.Status != DataAccessors.Class.DataStatusCode.SUCCESS || !response.HasValue)
            {
                return false;
            }

            var client = response.Value;

            if (client.ApiPermission == null)
            {
                logger.Warn($"Client with key {client.ClientKey.Substring(0, 10)}... has an invalid permission set");
                return false;
            }

            switch (requiredPermissionLevel)
            {
                case PermissionLevel.NONE:
                    return true;
                case PermissionLevel.READ:
                    return client.ApiPermission.CanRead.GetValueOrDefault();
                case PermissionLevel.WRITE:
                    return client.ApiPermission.CanWrite.GetValueOrDefault();
                case PermissionLevel.ADMIN:
                    return client.ApiPermission.CanAlterPermissions.GetValueOrDefault();
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determine whether or not a client is authorized to perform an action
        /// </summary>
        /// <param name="clientKey">The client's identifying key</param>
        /// <param name="requiredPermissionLevels">The required permissions to perform the action</param>
        /// <returns>Authorization determinator</returns>
        public bool Authorize(string clientKey, List<PermissionLevel> requiredPermissionLevels)
        {
            var response = ClientAccessor.GetClient(clientKey);

            if (response.Status != DataAccessors.Class.DataStatusCode.SUCCESS || !response.HasValue)
            {
                return false;
            }

            var client = response.Value;

            if (client.ApiPermission == null)
            {
                logger.Warn($"Client with key {client.ClientKey.Substring(0, 10)}... has an invalid permission set");
                return false;
            }

            foreach (var v in requiredPermissionLevels)
            {
                switch (v)
                {
                    case PermissionLevel.NONE:
                        break;
                    case PermissionLevel.READ:
                        if (!client.ApiPermission.CanRead.GetValueOrDefault())
                        {
                            return false;
                        }
                        break;
                    case PermissionLevel.WRITE:
                        if (!client.ApiPermission.CanWrite.GetValueOrDefault())
                        {
                            return false;
                        }
                        break;
                    case PermissionLevel.ADMIN:
                        if (!client.ApiPermission.CanAlterPermissions.GetValueOrDefault())
                        {
                            return false;
                        }
                        break;
                    default:
                        return false;
                }
            }

            return true;
        }
    }
}
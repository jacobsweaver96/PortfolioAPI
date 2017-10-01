using PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioAPI.Models;
using System.Data.SqlClient;
using log4net;
using PortfolioAPI.DataServices.DataAccessors.Class;

namespace PortfolioAPI.DataServices.DataAccessors
{
    /// <summary>
    /// Data accessor for interacting with clients
    /// </summary>
    public class ClientDataAccessor : IClientDataAccessor
    {
        private PortfolioDBDataContext DbContext { get; set; }
        private ILog logger
        {
            get { return DependencyResolver.Resolve<ILog>(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">Injects the data context</param>
        public ClientDataAccessor(PortfolioDBDataContext ctx)
        {
            DbContext = ctx;
        }

        /// <summary>
        /// Gets a client
        /// </summary>
        /// <param name="clientKey">The clients unique key</param>
        /// <returns></returns>
        public DataResponse<Client> GetClient(string clientKey)
        {
            DataResponse<Client> response;
            Client client = null;

            if (String.IsNullOrWhiteSpace(clientKey) || clientKey.Length != 32)
            {
                return new DataResponse<Client>(DataStatusCode.INVALID, "Invalid client key");
            }

            try
            {
                client = DbContext.Clients.SingleOrDefault(v => v.ClientKey == clientKey);
                response = new DataResponse<Client>(client, DataStatusCode.SUCCESS);
            }
            catch (SqlException ex)
            {
                logger.Error($"There was an sql exception while getting the client with the given key: {clientKey.Substring(0, 10)}...", ex);
                response = new DataResponse<Client>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error($"There was an exception while getting the client with the given key: {clientKey.Substring(0, 10)}...", ex);
                response = new DataResponse<Client>(DataStatusCode.ERROR);
            }

            return response;
        }
    }
}
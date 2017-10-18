using PortfolioAPI.Models;
using SandyModels.Models;
using System.Threading.Tasks;

namespace PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors
{
    /// <summary>
    /// Interface for interacting with clients
    /// </summary>
    public interface IClientDataAccessor
    {
        /// <summary>
        /// Gets a client for authorization
        /// </summary>
        /// <param name="clientKey">The key that identifies the client</param>
        /// <returns>The client</returns>
        Task<DataResponse<Client>> GetClient(string clientKey);
    }
}

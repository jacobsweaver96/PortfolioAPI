using PortfolioAPI.Interfaces;
using PortfolioAPI.DataServices.DataAccessors;
using PortfolioAPI.Models;
using PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors;

namespace PortfolioAPI.DataServices
{
    /// <summary>
    /// Service for accessing data
    /// </summary>
    public sealed class PortfolioDataAccessService : IDataAccessService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DbCxt">The database context</param>
        public PortfolioDataAccessService(string DbCxt)
        {
            UserDataAccessor = new UserDataAccessor(DbCxt);
            GithubUserDataAccessor = new GithubUserDataAccessor(DbCxt);
        }

        /// <summary>
        /// The data access service's user data accessor
        /// </summary>
        public IUserDataAccessor UserDataAccessor { get; set; }

        /// <summary>
        /// The data access service's github user data accessor
        /// </summary>
        public IGithubUserDataAccessor GithubUserDataAccessor { get; set; }
    }
}
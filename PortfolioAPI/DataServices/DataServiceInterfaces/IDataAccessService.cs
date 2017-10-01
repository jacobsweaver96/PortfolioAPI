using PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors;

namespace PortfolioAPI.Interfaces
{
    /// <summary>
    /// Data access service interface
    /// </summary>
    public interface IDataAccessService
    {
        /// <summary>
        /// The user data accessor
        /// </summary>
        IUserDataAccessor UserDataAccessor { get; set; }
        /// <summary>
        /// The github user data accessor
        /// </summary>
        IGithubUserDataAccessor GithubUserDataAccessor { get; set; } 
    }
}

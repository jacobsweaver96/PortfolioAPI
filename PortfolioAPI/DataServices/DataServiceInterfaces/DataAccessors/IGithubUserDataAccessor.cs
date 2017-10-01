using PortfolioAPI.DataServices.DataAccessors.Class;
using PortfolioAPI.Models;

namespace PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors
{
    /// <summary>
    /// Interface for interacting with github users
    /// </summary>
    public interface IGithubUserDataAccessor
    {
        /// <summary>
        /// Get a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <returns>The github user</returns>
        DataResponse<GithubUser> GetGithubUser(int githubUserId);
        /// <summary>
        /// Get a github user by email address
        /// </summary>
        /// <param name="email">The github user's email address</param>
        /// <returns>The github user</returns>
        DataResponse<GithubUser> GetGithubUser(string email);
        /// <summary>
        /// Add a github user
        /// </summary>
        /// <param name="githubUser">The github user to add</param>
        /// <returns>Success determinator</returns>
        DataResponse AddGithubUser(GithubUser githubUser);
        /// <summary>
        /// Update a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <param name="githubUser">The github user to update</param>
        /// <returns>Success determinator</returns>
        DataResponse UpdateGithubUser(int githubUserId, GithubUser githubUser);
        /// <summary>
        /// Deletes a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <returns>Success determinator</returns>
        DataResponse DeleteGithubUser(int githubUserId);
    }
}

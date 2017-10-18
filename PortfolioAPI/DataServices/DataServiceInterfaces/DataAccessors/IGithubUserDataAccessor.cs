using SandyModels.Models;
using System.Threading.Tasks;

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
        Task<DataResponse<Models.GithubUser>> GetGithubUser(int githubUserId);
        /// <summary>
        /// Get a github user by email address
        /// </summary>
        /// <param name="email">The github user's email address</param>
        /// <returns>The github user</returns>
        Task<DataResponse<Models.GithubUser>> GetGithubUser(string email);
        /// <summary>
        /// Add a github user
        /// </summary>
        /// <param name="githubUser">The github user to add</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> AddGithubUser(Models.GithubUser githubUser);
        /// <summary>
        /// Update a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <param name="githubUser">The github user to update</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> UpdateGithubUser(int githubUserId, Models.GithubUser githubUser);
        /// <summary>
        /// Deletes a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> DeleteGithubUser(int githubUserId);
    }
}

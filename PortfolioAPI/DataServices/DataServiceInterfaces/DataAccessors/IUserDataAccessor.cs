using PortfolioAPI.Models;
using SandyModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors
{
    /// <summary>
    /// Interface for interacting with users
    /// </summary>
    public interface IUserDataAccessor
    {
        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <returns>The user</returns>
        Task<DataResponse<Models.User>> GetUser(int UserId);
        /// <summary>
        /// Gets a user by email address
        /// </summary>
        /// <param name="email">The user's email address / username</param>
        /// <returns>The user</returns>
        Task<DataResponse<Models.User>> GetUser(string email);
        /// <summary>
        /// Adds a user
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> AddUser(Models.User user);
        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> DeleteUser(int UserId);
        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="user">The user to update</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> UpdateUser(int UserId, Models.User user);
        /// <summary>
        /// Adds a github account to a user
        /// </summary>
        /// <param name="UserId">The user id of the user who owns the github account</param>
        /// <param name="githubUser">The github user</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> AddGithubAccount(int UserId, Models.GithubUser githubUser);
        /// <summary>
        /// Removes all github accounts from a user
        /// </summary>
        /// <param name="UserId">The user id of the user from whom the github accounts are being removed</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> RemoveGithubAccounts(int UserId);
        /// <summary>
        /// Removes a single github account from a user
        /// </summary>
        /// <param name="UserId">The user id of the user from whom the github account is being removed</param>
        /// <param name="GithubUsername">The username / email associated with the github account that is to be removed</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> RemoveGithubAccount(int UserId, string GithubUsername);
        /// <summary>
        /// Adds a role to a user
        /// </summary>
        /// <param name="UserId">The user's user id</param>
        /// <param name="RoleId">The role id</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> AddRoleToUser(int UserId, int RoleId);
        /// <summary>
        /// Removes a role from a user
        /// </summary>
        /// <param name="UserId">The user's user id</param>
        /// <param name="RoleId">The role id</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> RemoveRoleFromUser(int UserId, int RoleId);
        /// <summary>
        /// Adds a new role
        /// </summary>
        /// <param name="role">The role to be added</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> AddRole(Models.Role role);
        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="roleId">The role id</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> DeleteRole(int roleId);
        /// <summary>
        /// Gets all active roles
        /// </summary>
        /// <returns>List of roles</returns>
        Task<DataResponse<List<Models.Role>>> GetRoles();
        /// <summary>
        /// Gets a role
        /// </summary>
        /// <param name="roleId">The role id</param>
        /// <returns>The role</returns>
        Task<DataResponse<Models.Role>> GetRole(int roleId);
        /// <summary>
        /// Updates a role
        /// </summary>
        /// <param name="roleId">The role id of the role to updated</param>
        /// <param name="role">The updated role</param>
        /// <returns>Success determinator</returns>
        Task<DataResponse> UpdateRole(int roleId, Models.Role role);
    }
}

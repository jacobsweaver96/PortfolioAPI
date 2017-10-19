using System;
using System.Linq;
using PortfolioAPI.Models;
using log4net;
using System.Data.SqlClient;
using PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors;
using System.Collections.Generic;
using SandyUtils.Utils;
using SandyModels.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PortfolioAPI.DataServices.DataAccessors
{
    /// <summary>
    /// Data accessor for interacting with users
    /// </summary>
    public class UserDataAccessor : IUserDataAccessor
    {
        private string DbContextStr { get; set; }
        private ILog logger
        {
            get { return DependencyResolver.Resolve<ILog>(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctxStr">Injects the data context</param>
        public UserDataAccessor(string ctxStr)
        {
            DbContextStr = ctxStr;
        }

        /// <summary>
        /// Add a github user to a user account
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="githubUser">The github account</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> AddGithubAccount(int UserId, Models.GithubUser githubUser)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var user = await DbContext.Users.SingleOrDefaultAsync(v => v.UserId == UserId);

                    if (user == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                        return;
                    }

                    githubUser.UserId = UserId;
                    DbContext.GithubUsers.Add(githubUser);
                    await DbContext.SaveChangesAsync();

                    response = new DataResponse(DataStatusCode.SUCCESS);
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on adding a github account", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on adding github account", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Adds a user
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> AddUser(Models.User user)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    DbContext.Users.Add(user);
                    await DbContext.SaveChangesAsync();

                    response = new DataResponse(DataStatusCode.SUCCESS);
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on adding a user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch(Exception ex)
            {
                logger.Error("Exception on adding user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> DeleteUser(int UserId)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var user = await DbContext.Users.SingleOrDefaultAsync(v => v.UserId == UserId);

                    if (user == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                    }
                    else
                    {
                        user.IsDeleted = true;
                        await DbContext.SaveChangesAsync();
                        response = new DataResponse(DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on deleting a user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on deleting user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <returns>The user</returns>
        public async Task<DataResponse<Models.User>> GetUser(int UserId)
        {
            DataResponse<Models.User> response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var user = await DbContext.Users.SingleOrDefaultAsync(v => v.UserId == UserId);

                    if (user == null)
                    {
                        response = new DataResponse<Models.User>(DataStatusCode.INVALID, "Invalid user id");
                    }
                    else
                    {
                        response = new DataResponse<Models.User>(user, DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on geting a user", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting user", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Gets a user by email address
        /// </summary>
        /// <param name="email">The user's email address / username</param>
        /// <returns>The user</returns>
        public async Task<DataResponse<Models.User>> GetUser(string email)
        {
            DataResponse<Models.User> response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var user = await DbContext.Users.SingleOrDefaultAsync(v => v.Email == email);

                    if (user == null)
                    {
                        response = new DataResponse<Models.User>(DataStatusCode.INVALID, "Invalid user id");
                    }
                    else
                    {
                        response = new DataResponse<Models.User>(user, DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on getting a user by email", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting user by email", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Removes all github accounts from a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> RemoveGithubAccounts(int UserId)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var user = await DbContext.Users.SingleOrDefaultAsync(v => v.UserId == UserId);

                    if (user == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                    }
                    else
                    {
                        var githubAccounts = user.GithubUsers;

                        foreach (var v in githubAccounts)
                        {
                            v.IsDeleted = true;
                        }

                        await DbContext.SaveChangesAsync();

                        response = new DataResponse(DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on removing github accounts from a user", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on removing github accounts from user", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Removes a github account from a given user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="GithubUsername">The github account's associated username</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> RemoveGithubAccount(int UserId, string GithubUsername)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var githubUser = DbContext.GithubUsers.SingleOrDefault(v => v.UserId == UserId && v.Username == GithubUsername && !v.IsDeleted);

                    if (githubUser == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid userId or username");
                    }
                    else
                    {
                        githubUser.IsDeleted = true;
                        await DbContext.SaveChangesAsync();
                        response = new DataResponse(DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on removing a github account from a user", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on removing github account from user", ex);
                response = new DataResponse<Models.User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="user">The user</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> UpdateUser(int UserId, Models.User user)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var dbUser = await DbContext.Users.SingleOrDefaultAsync(v => v.UserId == UserId);

                    if (dbUser == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                    }
                    else
                    {
                        dbUser.FirstName = user.FirstName ?? dbUser.FirstName;
                        dbUser.MiddleName = user.MiddleName ?? dbUser.MiddleName;
                        dbUser.LastName = user.LastName ?? dbUser.LastName;
                        dbUser.Biography = user.Biography ?? dbUser.Biography;
                        dbUser.ProfilePictureUri = user.ProfilePictureUri ?? dbUser.ProfilePictureUri;
                        dbUser.RelevantLinks = user.RelevantLinks ?? dbUser.RelevantLinks;
                        dbUser.ResumeLink = user.ResumeLink ?? dbUser.ResumeLink;
                        dbUser.Password = user.Password ?? dbUser.Password;
                        dbUser.Salt = user.Salt ?? dbUser.Salt;
                        dbUser.IsDeleted = user.IsDeleted;

                        await DbContext.SaveChangesAsync();
                        response = new DataResponse(DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on updating a user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on updating user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Adds a role to a user
        /// </summary>
        /// <param name="UserId">The user's user id</param>
        /// <param name="RoleId">The role id</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> AddRoleToUser(int UserId, int RoleId)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var user = await DbContext.Users.SingleOrDefaultAsync(v => v.UserId == UserId);

                    if (user == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                        return;
                    }

                    var role = await DbContext.Roles.SingleOrDefaultAsync(v => v.RoleId == RoleId);

                    if (role == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");
                        return;
                    }

                    var existingRoleMap = await DbContext.UserRole_Map.SingleOrDefaultAsync(v => v.UserId == UserId && v.RoleId == RoleId);

                    if (existingRoleMap != null)
                    {
                        existingRoleMap.IsDeleted = false;
                    }
                    else
                    {
                        UserRole_Map newRoleMap = new UserRole_Map
                        {
                            UserId = UserId,
                            RoleId = RoleId,
                            IsDeleted = false,
                        };

                        DbContext.UserRole_Map.Add(newRoleMap);
                    }

                    await DbContext.SaveChangesAsync();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                });
            }
            catch (SqlException ex)
            {
                logger.Error($"There was an sql exception while adding a role with role id {RoleId} to the user with user id {UserId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error($"There was an exception while adding a role with role id {RoleId} to the user with user id {UserId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Removes a role from a user
        /// </summary>
        /// <param name="UserId">The user's user id</param>
        /// <param name="RoleId">The role id</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> RemoveRoleFromUser(int UserId, int RoleId)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var user = DbContext.Users.SingleOrDefault(v => v.UserId == UserId);

                    if (user == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                        return;
                    }

                    var role = DbContext.Roles.SingleOrDefault(v => v.RoleId == RoleId);

                    if (role == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");
                        return;
                    }

                    var roleMap = await DbContext.UserRole_Map.SingleOrDefaultAsync(v => v.UserId == UserId && v.RoleId == RoleId && !v.IsDeleted);

                    if (roleMap == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Given role is not assigned to user");
                        return;
                    }
                    else
                    {
                        roleMap.IsDeleted = true;
                        await DbContext.SaveChangesAsync();
                        response = new DataResponse(DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error($"There was an sql excpetion while deleting the role with roleId {RoleId} from the user with the user id {UserId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error($"There was an excpetion while deleting the role with roleId {RoleId} from the user with the user id {UserId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Adds a new role
        /// </summary>
        /// <param name="role">The role to be added</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> AddRole(Models.Role role)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    DbContext.Roles.Add(role);
                    await DbContext.SaveChangesAsync();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                });
            }
            catch (SqlException ex)
            {
                logger.Error("There was an sql exception while adding a new role", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("There was an exception while adding a new role", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="roleId">The role id</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> DeleteRole(int roleId)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var role = await DbContext.Roles.SingleOrDefaultAsync(v => v.RoleId == roleId);

                    if (role == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");
                    }
                    else
                    {
                        role.IsDeleted = true;
                        await DbContext.SaveChangesAsync();
                        response = new DataResponse(DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error($"There was an sql exception while deleting role with roleId: {roleId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error($"There was an exception while deleting role with roleId: {roleId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Gets all active roles
        /// </summary>
        /// <returns>List of roles</returns>
        public async Task<DataResponse<List<Models.Role>>> GetRoles()
        {
            DataResponse<List<Models.Role>> response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var roles = await DbContext.Roles.Where(v => !v.IsDeleted).ToListAsync();
                    response = new DataResponse<List<Models.Role>>(roles, DataStatusCode.SUCCESS);
                });
            }
            catch (SqlException ex)
            {
                logger.Error("There was an sql exception while getting roles", ex);
                response = new DataResponse<List<Models.Role>>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("There was an exception while getting roles", ex);
                response = new DataResponse<List<Models.Role>>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Gets a role by role id
        /// </summary>
        /// <param name="roleId">The role id</param>
        /// <returns>The role</returns>
        public async Task<DataResponse<Models.Role>> GetRole(int roleId)
        {
            DataResponse<Models.Role> response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var role = await DbContext.Roles.SingleOrDefaultAsync(v => v.RoleId == roleId && !v.IsDeleted);

                    if (role == null)
                    {
                        response = new DataResponse<Models.Role>(DataStatusCode.INVALID, "Invalid role id");
                    }
                    else
                    {
                        response = new DataResponse<Models.Role>(role, DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error($"There was an sql exception while getting role with roleId: {roleId}", ex);
                response = new DataResponse<Models.Role>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error($"There was an exception while getting role with roleId: {roleId}", ex);
                response = new DataResponse<Models.Role>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Updates a role
        /// </summary>
        /// <param name="roleId">The role id of the role to updated</param>
        /// <param name="role">The updated role</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> UpdateRole(int roleId, Models.Role role)
        {
            DataResponse response = null;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var dbRole = await DbContext.Roles.SingleOrDefaultAsync(v => v.RoleId == roleId);

                    if (dbRole == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");
                    }
                    else
                    {
                        dbRole.Name = role.Name;
                        dbRole.Description = role.Description;
                        dbRole.IsDeleted = role.IsDeleted;
                        await DbContext.SaveChangesAsync();
                        response = new DataResponse(DataStatusCode.SUCCESS);
                    }
                });
            }
            catch (SqlException ex)
            {
                logger.Error($"There was an sql exception while updating the role with roleId: {roleId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error($"There was an exception while updating the role with roleId: {roleId}", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }
    }
}
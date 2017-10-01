using System;
using System.Linq;
using PortfolioAPI.Models;
using log4net;
using System.Data.SqlClient;
using PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors;
using System.Collections.Generic;
using PortfolioAPI.DataServices.DataAccessors.Class;

namespace PortfolioAPI.DataServices.DataAccessors
{
    /// <summary>
    /// Data accessor for interacting with users
    /// </summary>
    public class UserDataAccessor : IUserDataAccessor
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
        public UserDataAccessor(PortfolioDBDataContext ctx)
        {
            DbContext = ctx;
        }

        /// <summary>
        /// Add a github user to a user account
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="githubUser">The github account</param>
        /// <returns>Success determinator</returns>
        public DataResponse AddGithubAccount(int UserId, GithubUser githubUser)
        {
            DataResponse response;

            try
            {
                if (DbContext.Users.SingleOrDefault(v => v.UserId == UserId) == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                    return response;
                }

                githubUser.UserId = UserId;
                DbContext.GithubUsers.InsertOnSubmit(githubUser);
                DbContext.SubmitChanges();

                response = new DataResponse(DataStatusCode.SUCCESS);
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
        public DataResponse AddUser(User user)
        {
            DataResponse response;

            try
            {
                DbContext.Users.InsertOnSubmit(user);
                response = new DataResponse(DataStatusCode.SUCCESS);
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
        public DataResponse DeleteUser(int UserId)
        {
            DataResponse response;

            try
            {
                var user = DbContext.Users.SingleOrDefault(v => v.UserId == UserId);

                if (user == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                }
                else
                {
                    user.IsDeleted = true;
                    DbContext.SubmitChanges();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                }
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
        public DataResponse<User> GetUser(int UserId)
        {
            DataResponse<User> response;

            try
            {
                var user = DbContext.Users.SingleOrDefault(v => v.UserId == UserId);

                if (user == null)
                {
                    response = new DataResponse<User>(DataStatusCode.INVALID, "Invalid user id");
                }
                else
                {
                    response = new DataResponse<User>(user, DataStatusCode.SUCCESS);
                }
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on geting a user", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting user", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Gets a user by email address
        /// </summary>
        /// <param name="email">The user's email address / username</param>
        /// <returns>The user</returns>
        public DataResponse<User> GetUser(string email)
        {
            DataResponse<User> response;

            try
            {
                var user = DbContext.Users.SingleOrDefault(v => v.Email == email);

                if (user == null)
                {
                    response = new DataResponse<User>(DataStatusCode.INVALID, "Invalid user id");
                }
                else
                {
                    response = new DataResponse<User>(user, DataStatusCode.SUCCESS);
                } 
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on getting a user by email", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting user by email", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Removes all github accounts from a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <returns>Success determinator</returns>
        public DataResponse RemoveGithubAccounts(int UserId)
        {
            DataResponse response;

            try
            {
                var user = DbContext.Users.SingleOrDefault(v => v.UserId == UserId);

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

                    DbContext.SubmitChanges();

                    response = new DataResponse(DataStatusCode.SUCCESS);
                }                
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on removing github accounts from a user", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on removing github accounts from user", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Removes a github account from a given user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="GithubUsername">The github account's associated username</param>
        /// <returns>Success determinator</returns>
        public DataResponse RemoveGithubAccount(int UserId, string GithubUsername)
        {
            DataResponse response;

            try
            {
                var githubUser = DbContext.GithubUsers.SingleOrDefault(v => v.UserId == UserId && v.Username == GithubUsername && !v.IsDeleted);

                if (githubUser == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid userId or username");
                }
                else
                {
                    githubUser.IsDeleted = true;
                    DbContext.SubmitChanges();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                }
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on removing a github account from a user", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on removing github account from user", ex);
                response = new DataResponse<User>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="UserId">The user's id</param>
        /// <param name="user">The user</param>
        /// <returns>Success determinator</returns>
        public DataResponse UpdateUser(int UserId, User user)
        {
            DataResponse response;

            try
            {
                var dbUser = DbContext.Users.SingleOrDefault(v => v.UserId == UserId);

                if (dbUser == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                }
                else
                {
                    dbUser.Password = user.Password;
                    dbUser.Salt = user.Salt;
                    dbUser.IsDeleted = user.IsDeleted;
                    DbContext.SubmitChanges();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                }
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
        public DataResponse AddRoleToUser(int UserId, int RoleId)
        {
            DataResponse response;

            try
            {
                var user = DbContext.Users.SingleOrDefault(v => v.UserId == UserId);

                if (user == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                    return response;
                }

                var role = DbContext.Roles.SingleOrDefault(v => v.RoleId == RoleId);

                if (role == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");
                    return response;
                }

                var existingRoleMap = DbContext.UserRole_Maps.SingleOrDefault(v => v.UserId == UserId && v.RoleId == RoleId);

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

                    DbContext.UserRole_Maps.InsertOnSubmit(newRoleMap);
                }

                DbContext.SubmitChanges();
                response = new DataResponse(DataStatusCode.SUCCESS);
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
        public DataResponse RemoveRoleFromUser(int UserId, int RoleId)
        {
            DataResponse response;

            try
            {
                var user = DbContext.Users.SingleOrDefault(v => v.UserId == UserId);

                if (user == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid user id");
                    return response;
                }

                var role = DbContext.Roles.SingleOrDefault(v => v.RoleId == RoleId);
                
                if (role == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");
                    return response;
                }

                var roleMap = DbContext.UserRole_Maps.SingleOrDefault(v => v.UserId == UserId && v.RoleId == RoleId && !v.IsDeleted);

                if (roleMap == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Given role is not assigned to user");
                    return response;
                }
                else
                {
                    roleMap.IsDeleted = true;
                    DbContext.SubmitChanges();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                }
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
        public DataResponse AddRole(Role role)
        {
            DataResponse response;

            try
            {
                DbContext.Roles.InsertOnSubmit(role);
                DbContext.SubmitChanges();
                response = new DataResponse(DataStatusCode.SUCCESS);
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
        public DataResponse DeleteRole(int roleId)
        {
            DataResponse response;

            try
            {
                var role = DbContext.Roles.SingleOrDefault(v => v.RoleId == roleId);

                if (role == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");        
                }
                else
                {
                    role.IsDeleted = true;
                    DbContext.SubmitChanges();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                }
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
        public DataResponse<List<Role>> GetRoles()
        {
            DataResponse<List<Role>> response;

            try
            {
                var roles = DbContext.Roles.Where(v => !v.IsDeleted).ToList();
                response = new DataResponse<List<Role>>(roles, DataStatusCode.SUCCESS);
            }
            catch (SqlException ex)
            {
                logger.Error("There was an sql exception while getting roles", ex);
                response = new DataResponse<List<Role>>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("There was an exception while getting roles", ex);
                response = new DataResponse<List<Role>>(DataStatusCode.ERROR);
            }

            return response;
        }

        public DataResponse<Role> GetRole(int roleId)
        {
            DataResponse<Role> response;

            try
            {
                var role = DbContext.Roles.SingleOrDefault(v => v.RoleId == roleId && !v.IsDeleted);

                if (role == null)
                {
                    response = new DataResponse<Role>(DataStatusCode.INVALID, "Invalid role id");
                }
                else
                {
                    response = new DataResponse<Role>(role, DataStatusCode.SUCCESS);
                }
            }
            catch (SqlException ex)
            {
                logger.Error($"There was an sql exception while getting role with roleId: {roleId}", ex);
                response = new DataResponse<Role>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error($"There was an exception while getting role with roleId: {roleId}", ex);
                response = new DataResponse<Role>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Updates a role
        /// </summary>
        /// <param name="roleId">The role id of the role to updated</param>
        /// <param name="role">The updated role</param>
        /// <returns>Success determinator</returns>
        public DataResponse UpdateRole(int roleId, Role role)
        {
            DataResponse response;

            try
            {
                var dbRole = DbContext.Roles.SingleOrDefault(v => v.RoleId == roleId);

                if (dbRole == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid role id");
                }
                else
                {
                    dbRole.Name = role.Name;
                    dbRole.Description = role.Description;
                    dbRole.IsDeleted = role.IsDeleted;
                    DbContext.SubmitChanges();
                    response = new DataResponse(DataStatusCode.SUCCESS);
                }
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
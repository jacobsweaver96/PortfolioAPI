using PortfolioAPI.Models;
using SerializableClass = SandyModels.Models;
using System;
using log4net;
using System.Diagnostics;
using System.Linq;
using SandyUtils.Utils;

namespace PortfolioAPI.Util
{
    /// <summary>
    /// A utility for casting database access models to serializable models
    /// </summary>
    internal static class ModelConverter
    {
        private static ILog logger
        {
            get { return DependencyResolver.Resolve<ILog>(); }
        }

        // Do it like this in case we ever want to move this class
        private static string currNamespace
        {
            get { return typeof(ModelConverter).Namespace; }
        }

        /// <summary>
        /// Gets the serializable user model from the database access user model
        /// </summary>
        /// <param name="user">The database access user model </param>
        /// <returns>Serializable user model </returns>
        public static SerializableClass.User ToSerializableUser(User user)
        {
            SerializableClass.User sUser = null;

            if (user == null)
            {
                StackTrace stackTrace = new StackTrace();
                logger.Warn($"Passed a null value to {currNamespace}.ToSerializableUser from {stackTrace.GetFrame(1).GetMethod().Name}");
                return sUser;
            }

            try
            {
                sUser = new SerializableClass.User
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Password = user.Password,
                    Salt = user.Salt,
                    Roles = user.UserRole_Map
                            .Where(v => !v.IsDeleted)
                            .Select(v => ToSeriazlizableRole(v.Role))
                            .Where(v => !v.IsDeleted)
                            .ToList(),
                    IsDeleted = user.IsDeleted
                };
            }
            catch (Exception ex)
            {
                logger.Error("Exception on converting database user model to serializable user model", ex);
            }

            return sUser;
        }

        /// <summary>
        /// Gets the database access user model from the serializable user model
        /// </summary>
        /// <param name="user">The serializable user model</param>
        /// <returns>The database access user model</returns>
        public static User ToDbUser(SerializableClass.User user)
        {
            User dUser = null;

            if (user == null)
            {
                StackTrace stackTrace = new StackTrace();
                logger.Warn($"Passed a null value to {currNamespace}.ToDbUser from {stackTrace.GetFrame(1).GetMethod().Name}");
                return dUser;
            }

            try
            {
                dUser = new User
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Password = user.Password,
                    Salt = user.Salt,
                    IsDeleted = user.IsDeleted
                };
            }
            catch (Exception ex)
            {
                logger.Error("Exception on converting serializable user model to database user model", ex);
            }

            return dUser;
        }

        /// <summary>
        /// Gets the serializable github user model from the database access github user model
        /// </summary>
        /// <param name="githubUser"> The database access github user model </param>
        /// <returns> Serializable github user model </returns>
        public static SerializableClass.GithubUser ToSerializableGithubUser(GithubUser githubUser)
        {
            SerializableClass.GithubUser sGithubUser = null;

            if (githubUser == null)
            {
                StackTrace stackTrace = new StackTrace();
                logger.Warn($"Passed a null value to {currNamespace}.ToSerializableGithubUser from {stackTrace.GetFrame(1).GetMethod().Name}");
                return sGithubUser;
            }

            try
            {
                sGithubUser = new SerializableClass.GithubUser
                {
                    GithubUserId = githubUser.GithubUserId,
                    UserId = githubUser.UserId,
                    Username = githubUser.Username,
                    IsDeleted = githubUser.IsDeleted
                };
            }
            catch (Exception ex)
            {
                logger.Error("Exception on converting database github user model to serializable user model", ex);
            }

            return sGithubUser;
        }

        /// <summary>
        /// Gets the database access github user model from the serializable github user model
        /// </summary>
        /// <param name="githubUser">The serializable github user model</param>
        /// <returns>The database access github user model</returns>
        public static GithubUser ToDbGithubUser(SerializableClass.GithubUser githubUser)
        {
            GithubUser dGithubUser = null;

            if (githubUser == null)
            {
                StackTrace stackTrace = new StackTrace();
                logger.Warn($"Passed a null value to {currNamespace}.ToDbGithubUser from {stackTrace.GetFrame(1).GetMethod().Name}");
                return dGithubUser;
            }

            try
            {
                dGithubUser = new GithubUser
                {
                    GithubUserId = githubUser.GithubUserId,
                    UserId = githubUser.UserId,
                    Username = githubUser.Username,
                    IsDeleted = githubUser.IsDeleted
                };
            }
            catch (Exception ex)
            {
                logger.Error("Exception on converting serializable github user model to database github user model", ex);
            }

            return dGithubUser;
        }

        /// <summary>
        /// Gets the serializable role model from the database access role model
        /// </summary>
        /// <param name="role">The database access role model</param>
        /// <returns>Serializable role model</returns>
        public static SerializableClass.Role ToSeriazlizableRole(Role role)
        {
            SerializableClass.Role sRole = null;

            if (role == null)
            {
                StackTrace stackTrace = new StackTrace();
                logger.Warn($"Passed a null value to {currNamespace}.ToSerializableRole from {stackTrace.GetFrame(1).GetMethod().Name}");
                return sRole;
            }

            try
            {
                sRole = new SerializableClass.Role
                {
                    RoleId = role.RoleId,
                    Name = role.Name,
                    Description = role.Description,
                    IsDeleted = role.IsDeleted
                };
            }
            catch (Exception ex)
            {
                logger.Error("Exception on converting database role model to serializable role model", ex);
            }

            return sRole;
        }

        /// <summary>
        /// Gets the database access role model from the serializable role model
        /// </summary>
        /// <param name="role">The serializable role model</param>
        /// <returns>Database access role model</returns>
        public static Role ToDbRole(SerializableClass.Role role)
        {
            Role dRole = null;

            if (role == null)
            {
                StackTrace stackTrace = new StackTrace();
                logger.Warn($"Passed a null value to {currNamespace}.ToDbRole from {stackTrace.GetFrame(1).GetMethod().Name}");
                return dRole;
            }

            try
            {
                dRole = new Role
                {
                    RoleId = role.RoleId,
                    Name = role.Name,
                    Description = role.Description,
                    IsDeleted = role.IsDeleted
                };
            }
            catch (Exception ex)
            {
                logger.Error("Exception on converting serializable role model to database role model", ex);
            }

            return dRole;
        }
    }
}
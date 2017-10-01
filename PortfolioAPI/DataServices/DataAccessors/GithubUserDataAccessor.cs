using PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors;
using System;
using System.Linq;
using PortfolioAPI.Models;
using log4net;
using System.Data.SqlClient;
using PortfolioAPI.DataServices.DataAccessors.Class;

namespace PortfolioAPI.DataServices.DataAccessors
{
    /// <summary>
    /// Data accessor for interacting with github users
    /// </summary>
    public class GithubUserDataAccessor : IGithubUserDataAccessor
    {
        private PortfolioDBDataContext DbContext { get; set; }
        private ILog logger
        {
            get { return DependencyResolver.Resolve<ILog>(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbCxt">The database context</param>
        public GithubUserDataAccessor(PortfolioDBDataContext dbCxt)
        {
            DbContext = dbCxt;
        }

        /// <summary>
        /// Add a github user
        /// </summary>
        /// <param name="githubUser">The github user to add</param>
        /// <returns>Success determinator</returns>
        public DataResponse AddGithubUser(GithubUser githubUser)
        {
            DataResponse response;

            try
            {
                DbContext.GithubUsers.InsertOnSubmit(githubUser);
                DbContext.SubmitChanges();
                response = new DataResponse(DataStatusCode.SUCCESS);
                return response;
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on adding user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on adding user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Deletes a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <returns>Success determinator</returns>
        public DataResponse DeleteGithubUser(int githubUserId)
        {
            DataResponse response;

            try
            {
                GithubUser gu;

                if ((gu = DbContext.GithubUsers.SingleOrDefault(v => v.GithubUserId == githubUserId)) == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid github user id");
                    return response;
                }

                gu.IsDeleted = true;
                DbContext.SubmitChanges();

                response = new DataResponse(DataStatusCode.SUCCESS);
                return response;
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on deleting github user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on deleting github user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Get a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <returns>The github user</returns>
        public DataResponse<GithubUser> GetGithubUser(int githubUserId)
        {
            DataResponse<GithubUser> response;

            try
            {
                GithubUser gu;
                if ((gu = DbContext.GithubUsers.SingleOrDefault(v => v.GithubUserId == githubUserId)) == null)
                {
                    response = new DataResponse<GithubUser>(DataStatusCode.INVALID, "Invalid github user id");
                    return response;
                }

                response = new DataResponse<GithubUser>(gu, DataStatusCode.SUCCESS);
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on getting github user", ex);
                response = new DataResponse<GithubUser>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting github user", ex);
                response = new DataResponse<GithubUser>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Get a github user by email address
        /// </summary>
        /// <param name="email">The github user's email address</param>
        /// <returns>The github user</returns>
        public DataResponse<GithubUser> GetGithubUser(string email)
        {
            DataResponse<GithubUser> response;

            try
            {
                GithubUser gu;
                if ((gu = DbContext.GithubUsers.SingleOrDefault(v => v.Username == email)) == null)
                {
                    response = new DataResponse<GithubUser>(DataStatusCode.INVALID, "Invalid github user email");
                    return response;
                }

                response = new DataResponse<GithubUser>(gu, DataStatusCode.SUCCESS);
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on getting github user", ex);
                response = new DataResponse<GithubUser>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting github user", ex);
                response = new DataResponse<GithubUser>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Update a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <param name="githubUser">The github user to update</param>
        /// <returns>Success determinator</returns>
        public DataResponse UpdateGithubUser(int githubUserId, GithubUser githubUser)
        {
            DataResponse response;

            try
            {
                var dbGithubUser = DbContext.GithubUsers.SingleOrDefault(v => v.GithubUserId == githubUserId);

                if (dbGithubUser == null)
                {
                    response = new DataResponse(DataStatusCode.INVALID, "Invalid github user id");
                    return response;
                }

                dbGithubUser.IsDeleted = githubUser.IsDeleted;
                dbGithubUser.UserId = githubUser.UserId;
                dbGithubUser.Username = githubUser.Username;

                response = new DataResponse(DataStatusCode.SUCCESS);
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on updating github user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on updating gihub user", ex);
                response = new DataResponse(DataStatusCode.ERROR);
            }

            return response;
        }
    }
}
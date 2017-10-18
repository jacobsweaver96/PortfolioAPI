using PortfolioAPI.DataServices.DataServiceInterfaces.DataAccessors;
using System;
using System.Linq;
using PortfolioAPI.Models;
using log4net;
using System.Data.SqlClient;
using SandyModels.Models;
using SandyUtils.Utils;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PortfolioAPI.DataServices.DataAccessors
{
    /// <summary>
    /// Data accessor for interacting with github users
    /// </summary>
    public class GithubUserDataAccessor : IGithubUserDataAccessor
    {
        private string DbContextStr { get; set; }
        private ILog logger
        {
            get { return DependencyResolver.Resolve<ILog>(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctxStr">The database context</param>
        public GithubUserDataAccessor(string ctxStr)
        {
            DbContextStr = ctxStr;
        }

        /// <summary>
        /// Add a github user
        /// </summary>
        /// <param name="githubUser">The github user to add</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> AddGithubUser(Models.GithubUser githubUser)
        {
            DataResponse response;

            try
            {
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    DbContext.GithubUsers.Add(githubUser);
                    await DbContext.SaveChangesAsync();
                });
                
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
        public async Task<DataResponse> DeleteGithubUser(int githubUserId)
        {
            DataResponse response = null;

            try
            {
                Models.GithubUser gu;

                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    gu = await DbContext.GithubUsers.SingleOrDefaultAsync(v => v.GithubUserId == githubUserId);

                    if (gu == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid github user id");
                        return;
                    }

                    gu.IsDeleted = true;
                    await DbContext.SaveChangesAsync();

                    response = new DataResponse(DataStatusCode.SUCCESS);
                });

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
        public async Task<DataResponse<Models.GithubUser>> GetGithubUser(int githubUserId)
        {
            DataResponse<Models.GithubUser> response = null;

            try
            {
                Models.GithubUser gu;
                
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    gu = await DbContext.GithubUsers.SingleOrDefaultAsync(v => v.GithubUserId == githubUserId);

                    if (gu == null)
                    {
                        response = new DataResponse<Models.GithubUser>(DataStatusCode.INVALID, "Invalid github user id");
                        return;
                    }

                    response = new DataResponse<Models.GithubUser>(gu, DataStatusCode.SUCCESS);
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on getting github user", ex);
                response = new DataResponse<Models.GithubUser>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting github user", ex);
                response = new DataResponse<Models.GithubUser>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Get a github user by email address
        /// </summary>
        /// <param name="email">The github user's email address</param>
        /// <returns>The github user</returns>
        public async Task<DataResponse<Models.GithubUser>> GetGithubUser(string email)
        {
            DataResponse<Models.GithubUser> response = null;

            try
            {
                Models.GithubUser gu;

                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    gu = await DbContext.GithubUsers.SingleOrDefaultAsync(v => v.Username == email);

                    if (gu == null)
                    {
                        response = new DataResponse<Models.GithubUser>(DataStatusCode.INVALID, "Invalid github user email");
                        return;
                    }

                    response = new DataResponse<Models.GithubUser>(gu, DataStatusCode.SUCCESS);
                });
            }
            catch (SqlException ex)
            {
                logger.Error("Sql exception on getting github user", ex);
                response = new DataResponse<Models.GithubUser>(DataStatusCode.ERROR);
            }
            catch (Exception ex)
            {
                logger.Error("Exception on getting github user", ex);
                response = new DataResponse<Models.GithubUser>(DataStatusCode.ERROR);
            }

            return response;
        }

        /// <summary>
        /// Update a github user
        /// </summary>
        /// <param name="githubUserId">The github user's user id</param>
        /// <param name="githubUser">The github user to update</param>
        /// <returns>Success determinator</returns>
        public async Task<DataResponse> UpdateGithubUser(int githubUserId, Models.GithubUser githubUser)
        {
            DataResponse response = null;

            try
            {
                
                await DbCtxLifespanHelper.UseDataContext(DbContextStr, async (DbContext) =>
                {
                    var dbGithubUser = await DbContext.GithubUsers.SingleOrDefaultAsync(v => v.GithubUserId == githubUserId);

                    if (dbGithubUser == null)
                    {
                        response = new DataResponse(DataStatusCode.INVALID, "Invalid github user id");
                        return;
                    }

                    dbGithubUser.IsDeleted = githubUser.IsDeleted;
                    dbGithubUser.UserId = githubUser.UserId;
                    dbGithubUser.Username = githubUser.Username;

                    response = new DataResponse(DataStatusCode.SUCCESS);
                });
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
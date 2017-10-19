using System.Collections.Generic;
using System.Web.Http;
using SandyModels.Models;
using PortfolioAPI.Util;
using SandyModels.Models.ApiModels;
using System;
using System.Reflection;
using PortfolioAPI.Interfaces;
using RestEasy.Controllers;
using SandyUtils.Utils;
using RestEasy.Attributes;
using System.Threading.Tasks;

namespace PortfolioAPI.Controllers
{
    [RoutePrefix("api/Users")]
    public sealed class UsersController : RestController
    {
        private IDataAccessService DataAccessService
        {
            get
            {
                return DependencyResolver.Resolve<IDataAccessService>();
            }
        }

        protected override List<RestController> RelatedEndpoints
        {
            get
            {
                return new List<RestController>
                {
                    new GithubUsersController(),
                    new RoleController(),
                };
            }
        }

        [HttpGet]
        [Route("~/api/Users")]
        [RestInfo("GET", "Get URIs related to users")]
        public override ApiResponse Paths()
        {
            return CreateRestResponse();
        }
        
        [HttpGet]
        [Route("{UserId:int}")]
        [RestInfo("GET", "Get user with {UserId}")]
        [RequiresReadAccess]
        public async Task<ApiResponse<User>> Get(int UserId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(),
            async () =>
            {
                return await DataAccessService.UserDataAccessor.GetUser(UserId);
                
            }, (Models.User o) => { return ModelConverter.ToSerializableUser(o); });

            return await response;
        }

        [HttpGet]
        [Route("{Username}")]
        [RestInfo("GET", "Get user with {Username}")]
        [RequiresReadAccess]
        public async Task<ApiResponse<User>> Get(string Username)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(),
            async () =>
            {
                return await DataAccessService.UserDataAccessor.GetUser(Username);

            }, (Models.User o) => { return ModelConverter.ToSerializableUser(o); });

            return await response;
        }

        [HttpPost]
        [Route("")]
        [RestInfo("POST", "Update user", "User")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Post([FromBody]User value)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                var user = ModelConverter.ToDbUser(value);

                return await DataAccessService.UserDataAccessor.UpdateUser(user.UserId, user);
            });

            return await response;
        }
        
        [HttpPut]
        [Route("")]
        [RestInfo("PUT", "Create user", "User")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Put([FromBody]User value)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                var user = ModelConverter.ToDbUser(value);

                return await DataAccessService.UserDataAccessor.AddUser(user);
            });

            return await response;
        }

        [HttpPut]
        [Route("{userId:int}/Roles/{roleId:int}")]
        [RestInfo("PUT", "Add a role with {roleId} to user with {userId}")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Put(int userId, int roleId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.AddRoleToUser(userId, roleId);
            });

            return await response;
        }
        
        [HttpDelete]
        [Route("{UserId:int}")]
        [RestInfo("DELETE", "Delete user with {UserId}")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Delete(int UserId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.DeleteUser(UserId);
            });

            return await response;
        }

        [HttpDelete]
        [Route("{userId:int}/Roles/{roleId:int}")]
        [RestInfo("DELETE", "Remove a role with {roleId} from user with {userId}")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Delete(int userId, int roleId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.RemoveRoleFromUser(userId, roleId);
            });

            return await response;
        }

        [HttpPost]
        [Route("{UserName}/Password")]
        [RestInfo("POST", "Change the password of the user with {UserName}")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> UpdatePassword(string userName, [FromBody]User user)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(),
            async () =>
            {
                var dataResponse = await DataAccessService.UserDataAccessor.GetUser(userName);
                var dbUser = dataResponse.Value;

                dbUser.Password = user.Password;
                dbUser.Salt = user.Salt;

                return await DataAccessService.UserDataAccessor.UpdateUser(dbUser.UserId, dbUser);
            });

            return await response;
        }
    }
}

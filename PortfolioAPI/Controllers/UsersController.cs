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
        public ApiResponse Get(int UserId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(),
            () =>
            {
                return DataAccessService.UserDataAccessor.GetUser(UserId);
                
            }, (Models.User o) => { return ModelConverter.ToSerializableUser(o); });

            return response;
        }
        
        [HttpPost]
        [Route("")]
        [RestInfo("POST", "Update user", "User")]
        [RequiresWriteAccess]
        public ApiResponse Post([FromBody]User value)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            () =>
            {
                var user = ModelConverter.ToDbUser(value);

                return DataAccessService.UserDataAccessor.UpdateUser(user.UserId, user);
            });

            return response;
        }
        
        [HttpPut]
        [Route("")]
        [RestInfo("PUT", "Create user", "User")]
        [RequiresWriteAccess]
        public ApiResponse Put([FromBody]User value)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            () =>
            {
                var user = ModelConverter.ToDbUser(value);

                return DataAccessService.UserDataAccessor.AddUser(user);
            });

            return response;
        }

        [HttpPut]
        [Route("{userId:int}/Roles/{roleId:int}")]
        [RestInfo("PUT", "Add a role with {roleId} to user with {userId}")]
        [RequiresWriteAccess]
        public ApiResponse Put(int userId, int roleId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            () =>
            {
                return DataAccessService.UserDataAccessor.AddRoleToUser(userId, roleId);
            });

            return response;
        }
        
        [HttpDelete]
        [Route("{UserId:int}")]
        [RestInfo("DELETE", "Delete user with {UserId}")]
        [RequiresWriteAccess]
        public ApiResponse Delete(int UserId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            () =>
            {
                return DataAccessService.UserDataAccessor.DeleteUser(UserId);
            });

            return response;
        }

        [HttpDelete]
        [Route("{userId:int}/Roles/{roleId:int}")]
        [RestInfo("DELETE", "Remove a role with {roleId} from user with {userId}")]
        [RequiresWriteAccess]
        public ApiResponse Delete(int userId, int roleId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            () =>
            {
                return DataAccessService.UserDataAccessor.RemoveRoleFromUser(userId, roleId);
            });

            return response;
        }
    }
}

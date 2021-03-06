﻿using System.Collections.Generic;
using System.Linq;
using SandyModels.Models.ApiModels;
using System.Web.Http;
using SandyModels.Models;
using PortfolioAPI.Util;
using System.Reflection;
using PortfolioAPI.Interfaces;
using RestEasy.Controllers;
using SandyUtils.Utils;
using RestEasy.Attributes;
using System.Threading.Tasks;

namespace PortfolioAPI.Controllers
{
    [RoutePrefix("api/Roles")]
    public sealed class RoleController : RestController
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
                    new UsersController(),
                };
            }
        }

        [HttpGet]
        [Route("~/api/Roles")]
        [RestInfo("GET", "Get URIs related to roles")]
        public override ApiResponse Paths()
        {
            return CreateRestResponse();
        }

        [HttpGet]
        [Route("all")]
        [RestInfo("GET", "Get all roles")]
        [RequiresReadAccess]
        public async Task<ApiResponse<List<Role>>> Get()
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.GetRoles();
            }, (List<Models.Role> o) => { return o.Select(v => ModelConverter.ToSeriazlizableRole(v)).ToList(); });

            return await response;
        }

        [HttpGet]
        [Route("{roleId:int}")]
        [RestInfo("GET", "Get the role with {roleId}")]
        [RequiresReadAccess]
        public async Task<ApiResponse<Role>> Get(int roleId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.GetRole(roleId);
            }, (Models.Role o) => { return ModelConverter.ToSeriazlizableRole(o); });

            return await response;
        }

        [HttpPost]
        [Route("{roleId:int}")]
        [RestInfo("POST", "Update the role with {roleId}", "Role")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Post(int roleId, [FromBody]Role value)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.UpdateRole(roleId, ModelConverter.ToDbRole(value));
            });

            return await response;
        }

        [HttpPost]
        [Route("")]
        [RestInfo("PUT", "Add a new role", "Role")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Put([FromBody]Role value)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.AddRole(ModelConverter.ToDbRole(value));
            });

            return await response;
        }

        [HttpDelete]
        [Route("{roleId:int}")]
        [RestInfo("DELETE", "Delete the role with {roleId}")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Delete(int roleId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.UserDataAccessor.DeleteRole(roleId);
            });

            return await response;
        }
    }
}
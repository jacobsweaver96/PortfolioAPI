using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiModels.Models.Response;
using PortfolioAPI.ControllerAttributes;
using System.Web.Http;
using PortfolioModels.Models;
using PortfolioAPI.Util;

namespace PortfolioAPI.Controllers
{
    [RoutePrefix("api/Roles")]
    public sealed class RoleController : RestController
    {
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
        public ApiResponse Get()
        {
            var response = CreateRestResponse(() =>
            {
                return DataAccessService.UserDataAccessor.GetRoles();
            }, (List<Models.Role> o) => { return o.Select(v => ModelConverter.ToSeriazlizableRole(v)).ToList(); });

            return response;
        }

        [HttpGet]
        [Route("{roleId:int}")]
        [RestInfo("GET", "Get the role with {roleId}")]
        [RequiresReadAccess]
        public ApiResponse Get(int roleId)
        {
            var response = CreateRestResponse(() =>
            {
                return DataAccessService.UserDataAccessor.GetRole(roleId);
            }, (Models.Role o) => { return ModelConverter.ToSeriazlizableRole(o); });

            return response;
        }

        [HttpPost]
        [Route("{roleId:int}")]
        [RestInfo("POST", "Update the role with {roleId}", "Role")]
        [RequiresWriteAccess]
        public ApiResponse Post(int roleId, [FromBody]Role value)
        {
            var response = CreateRestResponse(() =>
            {
                return DataAccessService.UserDataAccessor.UpdateRole(roleId, ModelConverter.ToDbRole(value));
            });

            return response;
        }

        [HttpPost]
        [Route("")]
        [RestInfo("PUT", "Add a new role", "Role")]
        [RequiresWriteAccess]
        public ApiResponse Put([FromBody]Role value)
        {
            var response = CreateRestResponse(() =>
            {
                return DataAccessService.UserDataAccessor.AddRole(ModelConverter.ToDbRole(value));
            });

            return response;
        }

        [HttpDelete]
        [Route("{roleId:int}")]
        [RestInfo("DELETE", "Delete the role with {roleId}")]
        [RequiresWriteAccess]
        public ApiResponse Delete(int roleId)
        {
            var response = CreateRestResponse(() =>
            {
                return DataAccessService.UserDataAccessor.DeleteRole(roleId);
            });

            return response;
        }
    }
}
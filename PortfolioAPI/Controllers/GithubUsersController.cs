using SandyModels.Models.ApiModels;
using PortfolioAPI.Util;
using SandyModels.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using PortfolioAPI.Interfaces;
using RestEasy.Controllers;
using SandyUtils.Utils;
using RestEasy.Attributes;
using System.Threading.Tasks;

namespace PortfolioAPI.Controllers
{
    [RoutePrefix("api/GithubUsers")]
    public class GithubUsersController : RestController
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
            get { return new List<RestController>(); }
        }

        [HttpGet]
        [Route("~/api/GithubUsers")]
        [RestInfo("GET", "Get URIs related to github users")]
        public override ApiResponse Paths()
        {
            return CreateRestResponse();
        }

        [HttpGet]
        [Route("{GithubUserId:int}")]
        [RestInfo("GET", "Get github user with {GithubUserId}")]
        [RequiresReadAccess]
        public async Task<ApiResponse<GithubUser>> Get(int GithubUserId)
        {
            var response = CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.GithubUserDataAccessor.GetGithubUser(GithubUserId);
            }, (Models.GithubUser o) => { return ModelConverter.ToSerializableGithubUser(o); });

            return await response;
        }
        
        [HttpPost]
        [Route("")]
        [RestInfo("POST", "Update github user", "GithubUser")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Post([FromBody]GithubUser value)
        {
            return await CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                var gUser = ModelConverter.ToDbGithubUser(value);
                return await DataAccessService.GithubUserDataAccessor.UpdateGithubUser(value.GithubUserId, gUser);
            });
        }
        
        [HttpPut]
        [Route("")]
        [RestInfo("PUT", "Create github user", "GithubUser")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Put([FromBody]GithubUser value)
        {
            return await CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                var gUser = ModelConverter.ToDbGithubUser(value);
                return await DataAccessService.GithubUserDataAccessor.AddGithubUser(gUser);
            });
        }
        
        [HttpDelete]
        [Route("{GithubUserId:int}")]
        [RestInfo("DELETE", "Delete github user with {GithubUserId}")]
        [RequiresWriteAccess]
        public async Task<ApiResponse> Delete(int GithubUserId)
        {
            return await CreateRestResponse(MethodBase.GetCurrentMethod().GetCustomAttributesData(), 
            async () =>
            {
                return await DataAccessService.GithubUserDataAccessor.DeleteGithubUser(GithubUserId);
            });
        }
    }
}
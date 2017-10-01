using ApiModels.Models.Response;
using PortfolioAPI.ControllerAttributes;
using PortfolioAPI.Util;
using PortfolioModels.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace PortfolioAPI.Controllers
{
    [RoutePrefix("api/GithubUsers")]
    public class GithubUsersController : RestController
    {
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
        [Route("{UserId:int}")]
        [RestInfo("GET", "Get github user with {GithubUserId}")]
        [RequiresReadAccess]
        public ApiResponse Get(int GithubUserId)
        {
            var response = CreateRestResponse(() =>
            {
                return DataAccessService.GithubUserDataAccessor.GetGithubUser(GithubUserId);
            }, (Models.GithubUser o) => { return ModelConverter.ToSerializableGithubUser(o); });

            return response;
        }
        
        [HttpPost]
        [Route("")]
        [RestInfo("POST", "Update github user", "GithubUser")]
        [RequiresWriteAccess]
        public ApiResponse Post([FromBody]GithubUser value)
        {
            return CreateRestResponse(() =>
            {
                var gUser = ModelConverter.ToDbGithubUser(value);
                return DataAccessService.GithubUserDataAccessor.UpdateGithubUser(value.GithubUserId, gUser);
            }, null);
        }
        
        [HttpPut]
        [Route("")]
        [RestInfo("PUT", "Create github user", "GithubUser")]
        [RequiresWriteAccess]
        public ApiResponse Put([FromBody]GithubUser value)
        {
            return CreateRestResponse(() =>
            {
                var gUser = ModelConverter.ToDbGithubUser(value);
                return DataAccessService.GithubUserDataAccessor.AddGithubUser(gUser);
            });
        }
        
        [HttpDelete]
        [Route("{GithubUserId}")]
        [RestInfo("DELETE", "Delete github user with {GithubUserId}")]
        [RequiresWriteAccess]
        public ApiResponse Delete(int GithubUserId)
        {
            return CreateRestResponse(() =>
            {
                return DataAccessService.GithubUserDataAccessor.DeleteGithubUser(GithubUserId);
            });
        }
    }
}
using ApplicationServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsCore.Request;
using ModelsCore.Response;

namespace WebServiceCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TeamsController : ControllerBase
    {
        private IConfiguration _iConfig;
        public TeamsController(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }

        [Authorize]
        [HttpPost]
        [Route("SaveTeam")]
        public BasicResponse SaveTeam(SaveTeamRequest request)
        {
            var service = new TeamsAppService(_iConfig);
            return service.SaveTeam(request);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTeams")]
        public GetTeamsResponse GetAllTeams()
        {
            var service = new TeamsAppService(_iConfig);
            return service.GetAllTeams();
        }

        [Authorize]
        [HttpGet]
        [Route("GetTeam")]
        public GetTeamsResponse GetTeam(int Teamid)
        {
            var service = new TeamsAppService(_iConfig);
            return service.GetTeam(Teamid);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTeamsLogs")]
        public GetTeamLogsResponse GetTeamLogs()
        {
            var service = new TeamsAppService(_iConfig);
            return service.GetTeamLogs();
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteTeam")]
        public BasicResponse DeleteTeam(int Teamid)
        {
            var service = new TeamsAppService(_iConfig);
            return service.DeleteTeam(Teamid);
        }

    }
}

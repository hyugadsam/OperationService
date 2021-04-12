using ApplicationServices;
using Models.Request;
using Models.Response;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [RoutePrefix("Teams")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TeamsController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("SaveTeam")]
        [ActionName("v1")]
        public BasicResponse SaveTeam(SaveTeamRequest request)
        {
            var service = new TeamsAppService();
            return service.SaveTeam(request);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTeams")]
        [ActionName("v1")]
        public GetTeamsResponse GetAllTeams()
        {
            var service = new TeamsAppService();
            return service.GetAllTeams();
        }

        [Authorize]
        [HttpGet]
        [Route("GetTeam")]
        [ActionName("v1")]
        public GetTeamsResponse GetTeam(int Teamid)
        {
            var service = new TeamsAppService();
            return service.GetTeam(Teamid);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTeamsLogs")]
        [ActionName("v1")]
        public GetTeamLogsResponse GetTeamLogs()
        {
            var service = new TeamsAppService();
            return service.GetTeamLogs();
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteTeam")]
        [ActionName("v1")]
        public BasicResponse DeleteTeam(int Teamid)
        {
            var service = new TeamsAppService();
            return service.DeleteTeam(Teamid);
        }

    }
}
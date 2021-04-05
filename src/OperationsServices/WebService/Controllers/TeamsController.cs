using ApplicationServices;
using Models.Request;
using Models.Response;
using System.Web.Http;

namespace WebService.Controllers
{
    [RoutePrefix("Teams")]
    public class TeamsController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("SaveTeam")]
        public BasicResponse SaveTeam(SaveTeamRequest request)
        {
            var service = new TeamsAppService();
            return service.SaveTeam(request);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllTeams")]
        public GetTeamsResponse GetAllTeams()
        {
            var service = new TeamsAppService();
            return service.GetAllTeams();
        }

        [Authorize]
        [HttpGet]
        [Route("GetTeam")]
        public GetTeamsResponse GetTeam(int Teamid)
        {
            var service = new TeamsAppService();
            return service.GetTeam(Teamid);
        }
    }
}
using ApplicationServices;
using Models.Request;
using Models.Response;
using System.Web.Http;

namespace WebService.Controllers
{
    [RoutePrefix("Teams")]
    public class UsersController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("Save")]
        public BasicResponse Save(SaveUserRequest request)
        {
            var service = new UserAppService();
            return service.SaveUser(request);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdatePersonalInfo")]
        public BasicResponse UpdatePersonalInfo(SaveUserRequest request)
        {
            var service = new UserAppService();
            return service.UpdatePersonalInfo(request);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        public GetUsersResponse GetUser(int id)
        {
            var service = new UserAppService();
            return service.GetUser(id);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUsers")]
        public GetUsersResponse GetUsers()
        {
            var service = new UserAppService();
            return service.GetAllUsers();
        }

    }
}
using ApplicationServices;
using Models.Request;
using Models.Response;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [RoutePrefix("Users")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("Save")]
        [ActionName("v1")]
        public BasicResponse Save(SaveUserRequest request)
        {
            var service = new UserAppService();
            return service.SaveUser(request);
        }

        [Authorize]
        [HttpPost]
        [Route("UpdatePersonalInfo")]
        [ActionName("v1")]
        public BasicResponse UpdatePersonalInfo(SaveUserRequest request)
        {
            var service = new UserAppService();
            return service.UpdatePersonalInfo(request);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        [ActionName("v1")]
        public GetUsersResponse GetUser(int id)
        {
            var service = new UserAppService();
            return service.GetUser(id);
        }

        [Authorize]
        [HttpGet]
        [Route("GetUsers")]
        [ActionName("v1")]
        public GetUsersResponse GetUsers()
        {
            var service = new UserAppService();
            return service.GetAllUsers();
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteUser")]
        [ActionName("v1")]
        public BasicResponse DeleteUser(int Userid)
        {
            var service = new UserAppService();
            return service.DeleteUser(Userid);
        }

    }
}
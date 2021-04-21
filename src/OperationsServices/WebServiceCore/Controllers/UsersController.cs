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
    public class UsersController : ControllerBase
    {
        private IConfiguration _iConfig;
        public UsersController(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public GetUsersResponse GetUser(int id)
        {
            var service = new UserAppService(_iConfig);
            return service.GetUser(id);
        }

        [HttpGet]
        [Route("[action]")]
        public GetUsersResponse GetUsers()
        {
            var service = new UserAppService(_iConfig);
            return service.GetAllUsers();
        }

        [HttpPost]
        [Route("[action]")]
        public BasicResponse UpdatePersonalInfo(SaveUserRequest request)
        {
            var service = new UserAppService(_iConfig);
            return service.UpdateUserInfo(request);
        }

        [Authorize]
        [HttpPost]
        [Route("Save")]
        public BasicResponse Save(SaveUserRequest request)
        {
            var service = new UserAppService(_iConfig);
            return service.SaveUser(request);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteUser")]
        public BasicResponse DeleteUser(int Userid)
        {
            var service = new UserAppService(_iConfig);
            return service.DeleteUser(Userid);
        }

    }
}

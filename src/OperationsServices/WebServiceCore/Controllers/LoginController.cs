using ApplicationServicesCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsCore.Request;
using ModelsCore.Response;
using WebServiceCore.Security;

namespace WebServiceCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "v1|v2")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _iConfig;
        public LoginController(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }

        [HttpPost]
        [Route("[action]")]
        public AuthenticateResponse Authenticate(LoginRequest LoginReq)
        {
            if (string.IsNullOrEmpty(LoginReq?.Password) || string.IsNullOrEmpty(LoginReq?.Username))
                return new AuthenticateResponse
                {
                    isSaved = false,
                    Message = "Invalid Request"
                };

            var service = new AutenticationAppService(_iConfig);
            var response = service.isValid(LoginReq);
            if (response.isSaved)
            {
                response.Token = TokenHandler.GenerateToken(response.UserLogged, _iConfig); // TokenGenerator.GenerateTokenJwt(LoginReq.Username);
                return response;
            }
            return response;
        }
    }
}

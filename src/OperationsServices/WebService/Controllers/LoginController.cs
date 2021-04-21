using ApplicationServices;
using Models.Request;
using Models.Response;
using System.Web.Http;
using WebService.Security;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Login")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Authenticate")]
        [ActionName("v1|v2")] //Using ActionName only for filter version
        public AuthenticateResponse Authenticate([FromBody] LoginRequest LoginReq)
        {
            if (string.IsNullOrEmpty(LoginReq?.Password) || string.IsNullOrEmpty(LoginReq?.Username))
                return new AuthenticateResponse
                {
                    isSaved = false,
                    Message = "Invalid Request"
                };
            
            var service = new AutenticationAppService();
            var response = service.isValid(LoginReq);
            if (response.isSaved)
            {
                response.Token = TokenGenerator.GenerateTokenJwt(LoginReq.Username);
                return response;
            }
            else
                return new AuthenticateResponse
                {
                    isSaved = false,
                    Message = "Incorrect User or Password"
                };
        }


    }
}

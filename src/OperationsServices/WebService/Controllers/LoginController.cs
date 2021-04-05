using ApplicationServices;
using Models.Request;
using Models.Response;
using System.Net;
using System.Web.Http;
using WebService.Security;

namespace WebService.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Authenticate")]
        public AuthenticateResponse Authenticate([FromBody] LoginRequest LoginReq)
        {
            if (LoginReq == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            var service = new AutenticationAppService();
            var response = service.isValid(LoginReq);
            if (response.isSaved)
            {
                response.Token = TokenGenerator.GenerateTokenJwt(LoginReq.Username);
                return response;
            }
            else
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
    }
}

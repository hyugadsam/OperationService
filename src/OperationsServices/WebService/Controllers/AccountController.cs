using ApplicationServices;
using Models.Common;
using Models.Response;
using System.Web.Http;

namespace WebService.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("SaveAccount")]
        public BasicResponse SaveAccount(Account request)
        {
            var service = new AccountAppService();
            return service.SaveAccount(request);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAccounts")]
        public GetAccountsResponse GetAllAccounts()
        {
            var service = new AccountAppService();
            return service.GetAllAccounts();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAccount")]
        public GetAccountsResponse GetAccount(int Accountid)
        {
            var service = new AccountAppService();
            return service.GetAccount(Accountid);
        }
    }
}
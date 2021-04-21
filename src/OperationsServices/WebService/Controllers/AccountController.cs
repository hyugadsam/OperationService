using ApplicationServices;
using Models.Common;
using Models.Response;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [RoutePrefix("Account")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        [Authorize]
        [HttpPost]
        [Route("SaveAccount")]
        [ActionName("v1")]
        public BasicResponse SaveAccount(Account request)
        {
            var service = new AccountAppService();
            return service.SaveAccount(request);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAccounts")]
        [ActionName("v1")]
        public GetAccountsResponse GetAllAccounts()
        {
            var service = new AccountAppService();
            return service.GetAllAccounts();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAccount")]
        [ActionName("v1")]
        public GetAccountsResponse GetAccount(int Accountid)
        {
            var service = new AccountAppService();
            return service.GetAccount(Accountid);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteAccount")]
        [ActionName("v1")]
        public BasicResponse DeleteAccount(int Accountid)
        {
            var service = new AccountAppService();
            return service.DeleteAccount(Accountid);
        }


    }
}
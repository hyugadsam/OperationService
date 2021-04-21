using ApplicationServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsCore.Common;
using ModelsCore.Response;

namespace WebServiceCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AccountController : ControllerBase
    {
        private IConfiguration _iConfig;
        public AccountController(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }

        [Authorize]
        [HttpPost]
        [Route("SaveAccount")]
        public BasicResponse SaveAccount(Account request)
        {
            var service = new AccountAppService(_iConfig);
            return service.SaveAccount(request);
        }

        //[Authorize]
        [HttpGet]
        [Route("GetAllAccounts")]
        public GetAccountsResponse GetAllAccounts()
        {
            var service = new AccountAppService(_iConfig);
            return service.GetAllAccounts();
        }

        [Authorize]
        [HttpGet]
        [Route("GetAccount")]
        public GetAccountsResponse GetAccount(int Accountid)
        {
            var service = new AccountAppService(_iConfig);
            return service.GetAccount(Accountid);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteAccount")]
        public BasicResponse DeleteAccount(int Accountid)
        {
            var service = new AccountAppService(_iConfig);
            return service.DeleteAccount(Accountid);
        }

    }
}

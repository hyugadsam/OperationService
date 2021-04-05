using Models.Common;
using System.Collections.Generic;

namespace Models.Response
{
    public class GetAccountsResponse : BasicResponse
    {
        public List<Account> Accounts { get;set; }
    }
}

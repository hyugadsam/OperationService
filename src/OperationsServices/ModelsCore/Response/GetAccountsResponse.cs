using ModelsCore.Common;
using System.Collections.Generic;

namespace ModelsCore.Response
{
    public class GetAccountsResponse : BasicResponse
    {
        public List<Account> Accounts { get; set; }
    }
}

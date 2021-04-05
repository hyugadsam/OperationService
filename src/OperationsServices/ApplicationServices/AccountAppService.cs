using BDConection.Model;
using Models.Common;
using Models.Response;
using System;

namespace ApplicationServices
{
    public class AccountAppService: BaseMethods
    {
        public BasicResponse SaveAccount(Account request)
        {
            try
            {
                var Service = new AccountModel();
                return Service.SaveAccount(request);
            }
            catch (Exception ex)
            {
                SaveError("AccountAppService", "SaveAccount", ex.ToString(), DateTime.Now);
                return new BasicResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }

        }
        public GetAccountsResponse GetAllAccounts()
        {
            try
            {
                var Service = new AccountModel();
                return Service.GetAccounts(null);
            }
            catch (Exception ex)
            {
                SaveError("AccountAppService", "GetAllAccounts", ex.ToString(), DateTime.Now);
                return new GetAccountsResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }
        public GetAccountsResponse GetAccount(int Accountid)
        {
            try
            {
                var Service = new AccountModel();
                return Service.GetAccounts(Accountid);
            }
            catch (Exception ex)
            {
                SaveError("AccountAppService", "GetAccount", ex.ToString(), DateTime.Now);
                return new GetAccountsResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }
    }
}

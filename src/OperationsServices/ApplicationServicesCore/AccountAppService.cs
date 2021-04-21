using BdConectionCore.Services;
using Microsoft.Extensions.Configuration;
using ModelsCore.Common;
using ModelsCore.Response;
using System;

namespace ApplicationServicesCore
{
    public class AccountAppService : BaseMethods
    {
        private string Conection;
        public AccountAppService(IConfiguration iConfig)
        {
            Conection = iConfig.GetConnectionString("OperationBD");
            this.BaseConection = Conection;
        }
        public BasicResponse SaveAccount(Account request)
        {
            try
            {
                var Service = new AccountService(Conection);
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
                var Service = new AccountService(Conection);
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
                var Service = new AccountService(Conection);
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

        public BasicResponse DeleteAccount(int Accountid)
        {
            try
            {
                var Service = new AccountService(Conection);
                return Service.DeleteAccount(Accountid);
            }
            catch (Exception ex)
            {
                SaveError("AccountAppService", "DeleteAccount", ex.ToString(), DateTime.Now);
                return new GetAccountsResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }


    }
}
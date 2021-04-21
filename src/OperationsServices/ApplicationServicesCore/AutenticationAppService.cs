using ModelsCore.Request;
using ModelsCore.Response;
using BdConectionCore.Services;
using System;
using Microsoft.Extensions.Configuration;

namespace ApplicationServicesCore
{
    public class AutenticationAppService : BaseMethods
    {
        private string Conection;
        public AutenticationAppService(IConfiguration iConfig)
        {
            Conection = iConfig.GetConnectionString("OperationBD");
            this.BaseConection = Conection;
        }
        public AuthenticateResponse isValid(LoginRequest LoginReq)
        {
            try
            {
                //Validar usuario
                var UserService = new UserService(Conection);
                return UserService.isValid(LoginReq.Username, LoginReq.Password);
            }
            catch (Exception ex)
            {
                //log in data base
                SaveError("AutenticationAppService", "isValid", ex.ToString(), DateTime.Now);
                return new AuthenticateResponse
                {
                    isSaved = false,
                    Message = ex.ToString(),
                    Token = string.Empty
                };
            }
        }
    }
}

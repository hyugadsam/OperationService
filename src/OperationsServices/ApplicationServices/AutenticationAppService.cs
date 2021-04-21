using BDConection.Model;
using Models.Request;
using Models.Response;
using System;

namespace ApplicationServices
{
    public class AutenticationAppService : BaseMethods
    {
        public AuthenticateResponse isValid(LoginRequest LoginReq)
        {
            try
            {
                //Validar usuario
                var UserService = new UserModel();
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

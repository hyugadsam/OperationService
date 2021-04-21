using Microsoft.Extensions.Configuration;
using ModelsCore.Response;
using System;
using BdConectionCore.Services;
using ModelsCore.Request;
using ApplicationServicesCore.Validations;

namespace ApplicationServicesCore
{
    public class UserAppService : UsersValidations
    {
        private string Conection;
        public UserAppService(IConfiguration iConfig)
        {
            Conection = iConfig.GetConnectionString("OperationBD");
            this.BaseConection = Conection;
        }

        public BasicResponse UpdateUserInfo(SaveUserRequest request)
        {
            try
            {
                var Service = new UserService(Conection);
                return Service.UpdateUserInfo(request);
            }
            catch (Exception ex)
            {
                SaveError("UserAppService", "UpdateUserInfo", ex.ToString(), DateTime.Now);
                return new BasicResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }

        public GetUsersResponse GetAllUsers()
        {
            try
            {
                var Service = new UserService(Conection);
                return Service.GetUsers(null);
            }
            catch (Exception ex)
            {
                SaveError("UserAppService", "GetAllUsers", ex.ToString(), DateTime.Now);
                return new GetUsersResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }
        public GetUsersResponse GetUser(int Userid)
        {
            try
            {
                var Service = new UserService(Conection);
                return Service.GetUsers(Userid);
            }
            catch (Exception ex)
            {
                SaveError("UserAppService", "GetUser", ex.ToString(), DateTime.Now);
                return new GetUsersResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }

        public BasicResponse SaveUser(SaveUserRequest request)
        {
            try
            {
                var valid = SaveUserValidations(request); //Validaciones
                if (!valid.isSaved)
                {
                    return valid;
                }

                var Service = new UserService(Conection);
                return Service.SaveUser(request);
            }
            catch (Exception ex)
            {
                SaveError("UserAppService", "SaveUser", ex.ToString(), DateTime.Now);
                return new BasicResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }

        }

        public BasicResponse DeleteUser(int Userid)
        {
            try
            {
                var Service = new UserService(Conection);
                return Service.DeleteUser(Userid);
            }
            catch (Exception ex)
            {
                SaveError("UserAppService", "DeleteUser", ex.ToString(), DateTime.Now);
                return new BasicResponse
                {
                    isSaved = false,
                    Message = ex.ToString()
                };
            }
        }


    }
}

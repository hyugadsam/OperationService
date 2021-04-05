using ApplicationServices.Validations;
using BDConection.Model;
using Models.Request;
using Models.Response;
using System;

namespace ApplicationServices
{
    public class UserAppService : UsersValidations
    {
        public BasicResponse SaveUser(SaveUserRequest request)
        {
            try
            {
                var valid = SaveUserValidations(request); //Validaciones
                if (!valid.isSaved)
                {
                    return valid;
                }

                var Service = new UserModel();
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
        public BasicResponse UpdatePersonalInfo(SaveUserRequest request)
        {
            try
            {
                var Service = new UserModel();
                return Service.UpdateUserInfo(request);
            }
            catch (Exception ex)
            {
                SaveError("UserAppService", "UpdatePersonalInfo", ex.ToString(), DateTime.Now);
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
                var Service = new UserModel();
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
                var Service = new UserModel();
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

    }
}

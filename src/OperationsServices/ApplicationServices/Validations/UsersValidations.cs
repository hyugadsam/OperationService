using Models.Enums;
using Models.Request;
using Models.Response;

namespace ApplicationServices.Validations
{
    public class UsersValidations : BaseMethods
    {
        internal BasicResponse SaveUserValidations(SaveUserRequest request)
        {
            if (request?.UserInfo?.Roleid != (int)EnumRoles.FinalUser && request?.UserInfo?.Roleid != (int)EnumRoles.Admin)
            {
                return new BasicResponse
                {
                     isSaved = false,
                     Message = "RoleiD invalid"
                };
            }

            if (request?.UserInfo?.Userid == 0 && string.IsNullOrEmpty(request?.UserInfo?.UserLogin))
            {
                return new BasicResponse
                {
                    isSaved = false,
                    Message = "Invalid UserName / Userid invalid"
                };
            }

            if ((request?.UserInfo?.Userid == 0 && string.IsNullOrEmpty(request?.UserInfo?.Password)) || 
                (request?.UserInfo?.Userid > 0 && request?.HasNewPassword == true && string.IsNullOrEmpty(request?.UserInfo?.Password)) )
            {
                return new BasicResponse
                {
                    isSaved = false,
                    Message = "Provide a Password"
                };
            }


            return new BasicResponse { isSaved = true };
        }
    }
}

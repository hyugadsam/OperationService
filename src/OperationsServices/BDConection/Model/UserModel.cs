using BDConection.BDModel;
using BDConection.Security;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace BDConection.Model
{
    public class UserModel
    {

        public AuthenticateResponse isValid(string UserLogin, string Password)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    var result = context.Users.Where(r => r.UserLogin == UserLogin).ToList();
                    if (result?.Count == 0)
                    {
                        return new AuthenticateResponse
                        {
                            isSaved = false,
                            Message = "Usuario no encontrado"
                        };
                    }

                    var user = result.First();
                    if (user.Password.Equals(Hash.HashPasword(Password, user.Salt)))
                    {
                        return new AuthenticateResponse
                        {
                            isSaved = true,
                            Message = "Success",
                            UserLogged = new Models.Common.User
                            {
                                FullName = user.FullName,
                                InsertDate = user.InsertDate,
                                isActive = user.isActive,
                                Roleid = user.Roleid,
                                Userid = user.Userid,
                                UserLogin = user.UserLogin,
                                Email = user.Email,
                                EnglishLevel = user.EnglishLevel,
                                KnowlEdge = user.KnowlEdge,
                                UrlResume = user.UrlResume
                            }
                        };
                    }
                    else
                    {
                        return new AuthenticateResponse
                        {
                            isSaved = false,
                            Message = "Contraseña invalida"
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return new AuthenticateResponse
                //{
                //    isSaved = false,
                //    Message = ex.Message
                //};
            }
        }

        public GetUsersResponse GetUsers(int? Userid)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    var result = context.St_GetUsers(Userid).ToList();
                    if (result?.Count == 0)
                    {
                        return new GetUsersResponse
                        {
                            isSaved = false,
                            Message = "0 Users or Error"
                        };
                    }
                    List<Models.Common.User> users = result.Select(u => new Models.Common.User
                    {
                        InsertDate = u.InsertDate,
                        isActive = u.isActive,
                        Roleid = u.Roleid,
                        Userid = u.Userid,
                        UserLogin = u.UserLogin,
                        FullName = u.FullName,
                        Email = u.Email,
                        EnglishLevel = u.EnglishLevel,
                        KnowlEdge = u.KnowlEdge,
                        UrlResume = u.UrlResume,
                        Description = u.Description
                    }).ToList();

                    return new GetUsersResponse
                    {
                        isSaved = true,
                        Message = "Success",
                        UsersList = users
                    };

                }
            }
            catch (Exception ex)
            {
                //return new GetUsersResponse
                //{
                //    isSaved = false,
                //    Message = ex.Message
                //};
                throw ex;
            }
        }

        public BasicResponse SaveUser(SaveUserRequest request)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    if (request.UserInfo.Userid > 0) //Usuario existente
                    {
                        var u = context.St_GetUsers(request.UserInfo.Userid).ToList();
                        if (u?.Count > 0)
                        {
                            if (request.HasNewPassword)
                            {
                                request.UserInfo.Salt = Hash.GetNewSalt();
                                request.UserInfo.Password = Hash.HashPasword(request.UserInfo.Password, request.UserInfo.Salt);
                            }
                        }
                        else
                        {
                            return new BasicResponse
                            {
                                isSaved = false,
                                Message = "User not Found for update"
                            };
                        }
                    }
                    else //Usuario nuevo
                    {
                        request.UserInfo.Salt = Hash.GetNewSalt();
                        request.UserInfo.Password = Hash.HashPasword(request.UserInfo.Password, request.UserInfo.Salt);
                    }

                    System.Data.Entity.Core.Objects.ObjectParameter HasError = new System.Data.Entity.Core.Objects.ObjectParameter("HasError", false);
                    context.St_InsertUpdateUser(request.UserInfo.Userid, request.UserInfo.FullName, request.UserInfo.Roleid, request.UserInfo.Email, request.UserInfo.Password,
                        request.UserInfo.UserLogin, request.UserInfo.Salt, request.HasNewPassword, HasError);


                    if (Convert.ToBoolean(HasError.Value))
                    {
                        return new BasicResponse
                        {
                            isSaved = false,
                            Message = "Insert error"
                        };
                    }
                    
                    return new BasicResponse
                    {
                        isSaved = true,
                        Message = request.UserInfo.Userid > 0 ? "Update Success" : "Insert Success",
                    };

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return new BasicResponse
                //{
                //    isSaved = false,
                //    Message = ex.Message
                //};
            }
        }

        public BasicResponse UpdateUserInfo(SaveUserRequest request)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    System.Data.Entity.Core.Objects.ObjectParameter HasError = new System.Data.Entity.Core.Objects.ObjectParameter("HasError", false);
                    context.St_UpdateUserProfile(request.UserInfo.Userid, request.UserInfo.FullName, request.UserInfo.EnglishLevel, request.UserInfo.KnowlEdge, request.UserInfo.UrlResume, HasError);


                    if (Convert.ToBoolean(HasError.Value))
                    {
                        return new BasicResponse
                        {
                            isSaved = false,
                            Message = "Update Error"
                        };
                    }

                    return new BasicResponse
                    {
                        isSaved = true,
                        Message = "Update Success",
                    };

                }
            }
            catch (Exception ex)
            {
                //LogError
                throw ex;
                //return new BasicResponse
                //{
                //    isSaved = false,
                //    Message = ex.Message
                //};
            }
        }

        public BasicResponse DeleteUser(int Userid)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    System.Data.Entity.Core.Objects.ObjectParameter HasError = new System.Data.Entity.Core.Objects.ObjectParameter("HasError", false);
                    var result = context.St_DeleteUser(Userid, HasError);
                    if (Convert.ToBoolean(HasError.Value))
                    {
                        return new BasicResponse
                        {
                            isSaved = false,
                            Message = "Delete Error"
                        };
                    }

                    return new BasicResponse
                    {
                        isSaved = true,
                        Message = "Delete Success"
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return new BasicResponse
                //{
                //    isSaved = false,
                //    Message = ex.Message
                //};
            }
        }


    }
}

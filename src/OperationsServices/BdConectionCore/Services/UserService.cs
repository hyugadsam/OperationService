using BdConectionCore.BDConection;
using BdConectionCore.Security;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelsCore.Common;
using ModelsCore.Request;
using ModelsCore.Response;
using System;
using System.Linq;
using System.Transactions;

namespace BdConectionCore.Services
{
    public class UserService
    {
        private string _conection;
        public UserService(string con)
        {
            this._conection = con;
        }
        public AuthenticateResponse isValid(string UserLogin, string Password)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
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
                            UserLogged = new User
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
            }
        }

        public GetUsersResponse GetUsers(int? Userid)
        {
            try
            {
                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions //No lock for the select
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }))
                {
                    using (var context = new OperationsContext(_conection))
                    {
                        var result = context.Users.Where(r => r.Userid == (Userid == null ? r.Userid : Userid)).ToList();
                        var users = result.Where(r => r.Userid != 1).Select(u =>
                        new User
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
                            UrlResume = u.UrlResume
                        }).ToList();

                        return new GetUsersResponse
                        {
                            isSaved = users.Count > 0,
                            Message = users.Count > 0 ? "Success" : "0 Users",
                            UsersList = users
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BasicResponse SaveUser(SaveUserRequest request)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    if (request.UserInfo.Userid > 0) //Usuario existente
                    {
                        var u = GetUsers(request.UserInfo.Userid);
                        if (u.isSaved && u.UsersList.Count > 0)
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

                    #region Params

                    var pUserid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Userid",
                        Value = request.UserInfo.Userid
                    };

                    var pFullName = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@FullName",
                        Value = request.UserInfo.FullName
                    };

                    var pRoleid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Roleid",
                        Value = request.UserInfo.Roleid
                    };

                    var pEmail = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@Email",
                        Value = request.UserInfo.Email
                    };

                    var pPassword = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@Password",
                        Value = request.UserInfo.Password
                    };

                    var pUserLogin = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@UserLogin",
                        Value = request.UserInfo.UserLogin
                    };

                    var pSalt = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@Salt",
                        Value = request.UserInfo.Salt
                    };

                    var pPasswordEdited = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@PasswordEdited",
                        Value = request.HasNewPassword
                    };

                    var pHasError = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@HasError",
                        Value = true,
                        Direction = System.Data.ParameterDirection.InputOutput
                    };


                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[St_InsertUpdateUser] @Userid, @FullName, @Roleid, @Email, @Password, @UserLogin, @Salt, @PasswordEdited, @HasError OUTPUT",
                                                        pUserid, pFullName, pRoleid, pEmail, pPassword, pUserLogin, pSalt, pPasswordEdited, pHasError);


                    if (Convert.ToBoolean(pHasError.Value))
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
            }
        }

        public BasicResponse UpdateUserInfo(SaveUserRequest request)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    #region Params

                    var pUserid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Userid",
                        Value = request.UserInfo.Userid
                    };

                    var pFullName = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@FullName",
                        Value = request.UserInfo.FullName
                    };

                    var pEnglishLevel = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@EnglishLevel",
                        Value = request.UserInfo.EnglishLevel
                    };

                    var pKnowlEdge = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@KnowlEdge",
                        Value = request.UserInfo.KnowlEdge
                    };

                    var pUrlResume = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@UrlResume",
                        Value = request.UserInfo.UrlResume
                    };

                    var pHasError = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@HasError",
                        Value = true,
                        Direction = System.Data.ParameterDirection.InputOutput
                    };


                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[St_UpdateUserProfile] @Userid, @FullName, @EnglishLevel, @KnowlEdge, @UrlResume, @HasError OUTPUT",
                                                        pUserid, pFullName, pEnglishLevel, pKnowlEdge, pUrlResume, pHasError);

                    if (Convert.ToBoolean(pHasError.Value))
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
            }
        }

        public BasicResponse DeleteUser(int Userid)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    #region Params

                    var pUserid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Userid",
                        Value = Userid
                    };

                    var pHasError = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@HasError",
                        Value = true,
                        Direction = System.Data.ParameterDirection.InputOutput
                    };


                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[St_DeleteUser] @Userid, @HasError OUTPUT", pUserid, pHasError);

                    if (Convert.ToBoolean(pHasError.Value))
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
            }
        }

    }
}

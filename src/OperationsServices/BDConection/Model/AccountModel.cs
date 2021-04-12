using BDConection.BDModel;
using Models.Common;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BDConection.Model
{
    public class AccountModel
    {
        public GetAccountsResponse GetAccounts(int? Accountid)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    var result = context.St_GetAccounts(Accountid).ToList();
                    if (result?.Count == 0)
                    {
                        return new GetAccountsResponse
                        {
                            isSaved = false,
                            Message = "0 Users or Error"
                        };
                    }
                    List<Account> acc = result.Select(u => new Account
                    {
                        Teamid = u.Teamid,
                        TeamName = u.TeamName,
                        Accountid = u.Accountid,
                        AccountName = u.AccountName,
                        ClientName = u.ClientName,
                        OperatorName = u.OperatorName
                    }).ToList();

                    return new GetAccountsResponse
                    {
                        isSaved = true,
                        Message = "Success",
                        Accounts = acc
                    };

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return new GetAccountsResponse
                //{
                //    isSaved = false,
                //    Message = ex.Message
                //};
            }
        }

        public BasicResponse SaveAccount(Account request)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    if (request.Accountid > 0) //Cuenta existente
                    {
                        var u = context.St_GetAccounts(request.Accountid).ToList();
                        if (u?.Count == 0)
                        {
                            return new BasicResponse
                            {
                                isSaved = false,
                                Message = "Account not Found for update"
                            };
                        }
                    }

                    System.Data.Entity.Core.Objects.ObjectParameter HasError = new System.Data.Entity.Core.Objects.ObjectParameter("HasError", false);
                    context.St_InsertUpdateAccount(request.Accountid, request.AccountName, request.ClientName, request.OperatorName, request.Teamid, HasError);


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
                        Message = request.Accountid > 0 ? "Update Success" : "Insert Success",
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
                //;
            }
        }

        public BasicResponse DeleteAccount(int Accountid)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    System.Data.Entity.Core.Objects.ObjectParameter HasError = new System.Data.Entity.Core.Objects.ObjectParameter("HasError", false);
                    var result = context.St_DeleteAccount(Accountid, HasError);
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

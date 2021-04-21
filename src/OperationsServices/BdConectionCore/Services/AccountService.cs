using BdConectionCore.BDConection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ModelsCore.Common;
using ModelsCore.Response;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BdConectionCore.Services
{
    public class AccountService
    {
        private string _conection;
        public AccountService(string con)
        {
            this._conection = con;
        }


        public GetAccountsResponse GetAccounts(int? Accountid)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    var result = context.Accounts.GroupJoin(context.Teams,
                        a => a.Teamid,
                        t => t.Teamid,
                        (a, t) => new
                        {
                            b = a,
                            c = t
                        })
                        .SelectMany(
                          temp => temp.c.DefaultIfEmpty(),
                          (temp, p) =>
                             new
                             {
                                 d = temp.b,
                                 e = p
                             })
                        .Where(r => r.d.Accountid == (Accountid == null ? r.d.Accountid : Accountid))
                        .Select(a =>
                        new Account
                        {
                            Teamid = a.e.Teamid,
                            TeamName = a.e.TeamName,
                            Accountid = a.d.Accountid,
                            AccountName = a.d.AccountName,
                            ClientName = a.d.ClientName,
                            OperatorName = a.d.OperatorName
                        })
                        .ToList();

                    //var result = context.Accounts.Join(context.Teams,
                    //    a => a.Teamid,
                    //    t => t.Teamid,
                    //    (a, t) => new Account
                    //    {
                    //        Teamid = t.Teamid,
                    //        TeamName = t.TeamName,
                    //        Accountid = a.Accountid,
                    //        AccountName = a.AccountName,
                    //        ClientName = a.ClientName,
                    //        OperatorName = a.OperatorName
                    //    }
                    //    ).DefaultIfEmpty().Where(r => r.Accountid == (Accountid == null ? r.Accountid : Accountid)).ToList();

                    if (result?.Count == 0)
                    {
                        return new GetAccountsResponse
                        {
                            isSaved = false,
                            Message = "0 Users or Error"
                        };
                    }
                   
                    return new GetAccountsResponse
                    {
                        isSaved = true,
                        Message = "Success",
                        Accounts = result
                    };

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BasicResponse SaveAccount(Account request)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    if (request.Accountid > 0) //Cuenta existente
                    {
                        var u = GetAccounts(request.Accountid);
                        if (u.Accounts.Count == 0)
                        {
                            return new BasicResponse
                            {
                                isSaved = false,
                                Message = "Account not Found for update"
                            };
                        }
                    }

                    #region Params

                    var pAccountid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Accountid",
                        Value = request.Accountid
                    };

                    var pAccountName = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@AccountName",
                        Value = request.AccountName
                    };

                    var pClientName = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@ClientName",
                        Value = request.ClientName
                    };

                    var pOperatorName = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@OperatorName",
                        Value = request.OperatorName
                    };

                    var pTeamid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Teamid",
                        Value = request.Teamid
                    };

                    var pHasError = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@HasError",
                        Value = true,
                        Direction = System.Data.ParameterDirection.InputOutput
                    };

                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[St_InsertUpdateAccount] @Accountid, @AccountName, @ClientName, @OperatorName, @Teamid, @HasError OUTPUT",
                                                        pAccountid, pAccountName, pClientName, pOperatorName, pTeamid, pHasError);

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
                        Message = request.Accountid > 0 ? "Update Success" : "Insert Success",
                    };

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BasicResponse DeleteAccount(int Accountid)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    #region Params

                    var pAccountid = new SqlParameter
                    {
                        DbType = System.Data.DbType.Int32,
                        ParameterName = "@Accountid",
                        Value = Accountid
                    };

                    var pHasError = new SqlParameter
                    {
                        DbType = System.Data.DbType.Boolean,
                        ParameterName = "@HasError",
                        Value = true,
                        Direction = System.Data.ParameterDirection.InputOutput
                    };

                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[St_DeleteAccount] @Accountid, @HasError OUTPUT", pAccountid, pHasError);

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

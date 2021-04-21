using BdConectionCore.BDConection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;


namespace BdConectionCore.Services
{
    public class LoggerService
    {
        private string _conection;
        public LoggerService(string con)
        {
            this._conection = con;
        }


        public void LogError(string Class, string Method, string ErrorMessage, DateTime DateOfInsert)
        {
            try
            {
                using (var context = new OperationsContext(_conection))
                {
                    #region Params

                    var pClass = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@Class",
                        Value = Class
                    };

                    var pMethod = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@Method",
                        Value = Method
                    };

                    var pErrorMessage = new SqlParameter
                    {
                        DbType = System.Data.DbType.String,
                        ParameterName = "@ErrorMessage",
                        Value = ErrorMessage
                    };

                    var pDateOfInsert = new SqlParameter
                    {
                        DbType = System.Data.DbType.DateTime,
                        ParameterName = "@DateOfInsert",
                        Value = DateOfInsert
                    };

                    #endregion

                    context.Database.ExecuteSqlRaw("EXEC [dbo].[st_InsertAppLogs] @Class, @Method, @ErrorMessage, @DateOfInsert", pClass, pMethod, pErrorMessage, pDateOfInsert);
                }
            }
            catch (Exception ex)
            {
                //Logg Ex in a file
            }
        }



    }
}

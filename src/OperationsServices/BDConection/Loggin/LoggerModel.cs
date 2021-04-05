using BDConection.BDModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDConection.Loggin
{
    public class LoggerModel
    {
        public void LogError(string Class, string Method, string ErrorMessage, DateTime DateOfInsert)
        {
            try
            {
                using (var context = new OperationsEntities())
                {
                    context.st_InsertAppLogs(Class, Method, ErrorMessage, DateOfInsert);
                }
            }
            catch (Exception ex)
            {
                //Logg Ex in a file
            }
        }

    }
}

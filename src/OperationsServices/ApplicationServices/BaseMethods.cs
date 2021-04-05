using BDConection.Loggin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public class BaseMethods
    {
        internal void SaveError(string Class, string Method, string ErrorMessage, DateTime DateOfInsert)
        {
            new LoggerModel().LogError(Class, Method, ErrorMessage, DateOfInsert);
        }

    }
}

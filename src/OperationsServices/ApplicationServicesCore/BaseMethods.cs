using BdConectionCore.Services;
using System;

namespace ApplicationServicesCore
{
    public class BaseMethods
    {
        internal string BaseConection;

        internal void SaveError(string Class, string Method, string ErrorMessage, DateTime DateOfInsert)
        {
            new LoggerService(BaseConection).LogError(Class, Method, ErrorMessage, DateOfInsert);
        }

    }
}

using BDConection.Dto.Common;
using BDConection.Dto.Response;
using BDConection.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public isValidResponse isValid(string UserLogin, string Password)
        {
            var model = new UserModel();
            return model.isValid(UserLogin, Password);
        }

        public GetUsersResponse GetUsers()
        {
            var model = new UserModel();
            return model.GetUsers();
        }

        public BasicResponse SaveUser(User user)
        {
            var model = new UserModel();
            return model.SaveUser(user);
        }

        public BasicResponse GeneratePaysheets(DateTime date)
        {
            var model = new FinanceModel();
            return model.GeneratePaysheets(date);
        }

        public GetPaySheetsResponse GetPaySheets(int? Userid)
        {
            var model = new FinanceModel();
            return model.GetPaySheets(Userid);
        }

        public GetPaySheetsResponse GetPaySheetById(int id)
        {
            var model = new FinanceModel();
            return model.GetPaySheetById(id);
        }

    }
}

using BDConection.Dto.Common;
using BDConection.Dto.Response;
using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Text;

namespace WebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        isValidResponse isValid(string UserLogin, string Password);

        [OperationContract]
        GetUsersResponse GetUsers();

        [OperationContract]
        BasicResponse SaveUser(User user);

        [OperationContract]
        BasicResponse GeneratePaysheets(DateTime date);

        [OperationContract]
        GetPaySheetsResponse GetPaySheets(int? Userid);

        [OperationContract]
        GetPaySheetsResponse GetPaySheetById(int id);

        // TODO: agregue aquí sus operaciones de servicio
    }

}

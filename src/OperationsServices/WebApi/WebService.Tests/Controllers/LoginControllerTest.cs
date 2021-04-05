using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Request;
using Models.Response;
using System;
using System.Web.Http.Results;
using WebService.Controllers;

namespace WebService.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void Login()
        {
            LoginController controller = new LoginController();
            var req = new LoginRequest
            {
                Username = "admin",
                Password = "admin"
            };

            var response = controller.Authenticate(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsNotNull(response.UserLogged);
            Assert.IsFalse(string.IsNullOrEmpty(response.Token));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Common;
using System;
using System.Linq;
using WebService.Controllers;

namespace WebService.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Save()
        {
            AccountController controller = new AccountController();
            var req = new Account
            {
                Accountid = 0,
                AccountName = "Account",
                ClientName = "Client",
                OperatorName = "Operator",
                Teamid = 1
            };

            var response = controller.SaveAccount(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

        [TestMethod]
        public void Update()
        {
            AccountController controller = new AccountController();
            var req = new Account
            {
                Accountid = 1,
                AccountName = "Super Account",
                ClientName = "Super Client",
                OperatorName = "Super Operator",
                Teamid = 1
            };

            var response = controller.SaveAccount(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

        [TestMethod]
        public void GetAllAccounts()
        {
            AccountController controller = new AccountController();

            var response = controller.GetAllAccounts();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsTrue(response.Accounts?.Count > 0);
        }

        [TestMethod]
        public void GetAccount()
        {
            AccountController controller = new AccountController();

            var response = controller.GetAccount(1);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsTrue(response.Accounts?.Count > 0);
        }

        [TestMethod]
        public void DeleteAccount()
        {
            AccountController controller = new AccountController();
            var accounts = controller.GetAllAccounts();
            Assert.IsTrue(accounts.isSaved);
            Assert.IsTrue(accounts.Accounts?.Count > 0);

            var response = controller.DeleteAccount(accounts.Accounts.Last().Accountid);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

    }
}

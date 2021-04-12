using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Request;
using System;
using System.Linq;
using WebService.Controllers;

namespace WebService.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void Save()
        {
            UsersController controller = new UsersController();
            var req = new SaveUserRequest
            {
                UserInfo = new Models.Common.User
                {
                    UserLogin = "admin",
                    Password = "admin",
                    Email = "Admin@arkusnexus.com",
                    FullName = "Admin",
                    Roleid = 2
                },
                HasNewPassword = false
            };

            var response = controller.Save(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

        [TestMethod]
        public void SaveUserError()
        {
            UsersController controller = new UsersController();
            var req = new SaveUserRequest
            {
                UserInfo = new Models.Common.User
                {
                    UserLogin = "",
                    Password = "Test",
                    Email = "SuperAdmin@arkusnexus.com",
                    FullName = "Super Admin",
                    Roleid = 2
                },
                HasNewPassword = false
            };

            var response = controller.Save(req);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.isSaved);
            Assert.IsTrue(response.Message.Contains("User"));
        }

        [TestMethod]
        public void SaveRoleError()
        {
            UsersController controller = new UsersController();
            var req = new SaveUserRequest
            {
                UserInfo = new Models.Common.User
                {
                    UserLogin = "superadmin",
                    Password = "Test",
                    Email = "SuperAdmin@arkusnexus.com",
                    FullName = "Super Admin",
                    Roleid = 1
                },
                HasNewPassword = false
            };

            var response = controller.Save(req);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.isSaved);
            Assert.IsTrue(response.Message.Contains("Role"));
        }

        [TestMethod]
        public void SavePasswordError()
        {
            UsersController controller = new UsersController();
            var req = new SaveUserRequest
            {
                UserInfo = new Models.Common.User
                {
                    UserLogin = "superadmin",
                    Password = string.Empty,
                    Email = "SuperAdmin@arkusnexus.com",
                    FullName = "Super Admin",
                    Roleid = 2,
                    Userid = 2
                },
                HasNewPassword = true
            };

            var response = controller.Save(req);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.isSaved);
            Assert.IsTrue(response.Message.Contains("Password"));
        }

        [TestMethod]
        public void Update()
        {
            UsersController controller = new UsersController();
            var req = new SaveUserRequest
            {
                UserInfo = new Models.Common.User
                {
                    UserLogin = "superadmin",
                    Password = "superadmin",
                    Email = "SuperAdmin@arkusnexus.com",
                    FullName = "Super Admin",
                    Roleid = 1,
                    Userid = 1
                },
                HasNewPassword = true
            };

            var response = controller.Save(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

        [TestMethod]
        public void UpdatePersonalInfo()
        {
            UsersController controller = new UsersController();
            var req = new SaveUserRequest
            {
                UserInfo = new Models.Common.User
                {
                    FullName = "Super Admin c:",
                    Userid = 1,
                    UrlResume = "https://www.google.com",
                    EnglishLevel = "Beginner",
                    KnowlEdge = "Angular Test"
                }
            };

            var response = controller.UpdatePersonalInfo(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            UsersController controller = new UsersController();
            var response = controller.GetUsers();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsNotNull(response.UsersList);
            Assert.IsTrue(response.UsersList.Count > 0);
        }

        [TestMethod]
        public void GetUserById()
        {
            UsersController controller = new UsersController();
            var response = controller.GetUser(1);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsNotNull(response.UsersList);
            Assert.IsTrue(response.UsersList.Count > 0);
        }

        [TestMethod]
        public void DeleteUser()
        {
            UsersController controller = new UsersController();
            var users = controller.GetUsers();
            Assert.IsTrue(users.isSaved);
            Assert.IsTrue(users.UsersList?.Count > 0);

            var response = controller.DeleteUser(users.UsersList.Last().Userid);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }


    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Request;
using System;
using System.Linq;
using WebService.Controllers;

namespace WebService.Tests.Controllers
{
    [TestClass]
    public class TeamsControllerTest
    {
        [TestMethod]
        public void Save()
        {
            TeamsController controller = new TeamsController();
            var req = new SaveTeamRequest
            {
                TeamName = "Team",
                Users = new System.Collections.Generic.List<int> { 1 }
            };

            var response = controller.SaveTeam(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

        [TestMethod]
        public void Update()
        {
            TeamsController controller = new TeamsController();
            var req = new SaveTeamRequest
            {
                Teamid = 1,
                TeamName = "Super Team",
                Users = new System.Collections.Generic.List<int> { 1, 2 }
            };

            var response = controller.SaveTeam(req);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

        [TestMethod]
        public void GetAllTeams()
        {
            TeamsController controller = new TeamsController();
            
            var response = controller.GetAllTeams();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsTrue(response.Teams?.Count > 0);
        }

        [TestMethod]
        public void GetTeam()
        {
            TeamsController controller = new TeamsController();

            var response = controller.GetTeam(1);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsTrue(response.Teams?.Count > 0);
        }

        [TestMethod]
        public void GetAllTeamsLogs()
        {
            TeamsController controller = new TeamsController();

            var response = controller.GetTeamLogs();

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
            Assert.IsTrue(response.Logs?.Count > 0);
        }

        [TestMethod]
        public void DeleteTeam()
        {
            TeamsController controller = new TeamsController();
            var teams = controller.GetAllTeams();
            Assert.IsTrue(teams.isSaved);
            Assert.IsTrue(teams.Teams?.Count > 0);

            var response = controller.DeleteTeam(teams.Teams.Last().Teamid);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.isSaved);
        }

    }
}

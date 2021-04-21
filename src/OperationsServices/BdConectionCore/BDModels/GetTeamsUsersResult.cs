using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdConectionCore.BDModels
{
    public class GetTeamsUsersResult
    {
        public int Teamid { get; set; }
        public string TeamName { get; set; }
        public int? UserId { get; set; }
        public string UserLogin { get; set; }
        public string FullName { get; set; }
    }
}

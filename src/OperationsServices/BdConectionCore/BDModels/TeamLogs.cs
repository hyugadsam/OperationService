using System;
using System.ComponentModel.DataAnnotations;

namespace BdConectionCore.BDModels
{
    public class TeamLogs
    {
        [Key]
        public int TeamLogid { get; set; }
        public int Teamid { get; set; }
        public DateTime DateofMovement { get; set; }
        public string OldUsers { get; set; }
        public string NewUsers { get; set; }

    }

}

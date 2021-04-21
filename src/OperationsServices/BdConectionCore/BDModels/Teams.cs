using System.ComponentModel.DataAnnotations;

namespace BdConectionCore.BDModels
{
    public class Teams
    {
        [Key]
        public int Teamid { get; set; }
        public string TeamName { get; set; }
    }
}

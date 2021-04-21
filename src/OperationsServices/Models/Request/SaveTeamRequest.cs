using System.Collections.Generic;


namespace Models.Request
{
    public class SaveTeamRequest
    {
        public int Teamid { get; set; }
        public string TeamName { get; set; }
        public List<int> Users { get; set; }
    }
}

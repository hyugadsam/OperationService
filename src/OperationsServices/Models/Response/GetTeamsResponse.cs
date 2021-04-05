using Models.Common;
using System.Collections.Generic;

namespace Models.Response
{
    public class GetTeamsResponse : BasicResponse
    {
        public List<Team> Teams { get; set; }
    }
}

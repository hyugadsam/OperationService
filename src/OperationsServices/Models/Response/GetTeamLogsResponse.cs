using Models.Common;
using System.Collections.Generic;

namespace Models.Response
{
    public class GetTeamLogsResponse : BasicResponse
    {
        public List<TeamLog> Logs { get; set; }
    }
}

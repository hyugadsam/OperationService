using ModelsCore.Common;
using System.Collections.Generic;

namespace ModelsCore.Response
{
    public class GetTeamLogsResponse : BasicResponse
    {
        public List<TeamLog> Logs { get; set; }
    }
}

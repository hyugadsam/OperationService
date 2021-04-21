using ModelsCore.Common;
using ModelsCore.Response;
using System.Collections.Generic;

namespace ModelsCore.Request
{
    public class GetTeamsResponse : BasicResponse
    {
        public List<Team>  Teams { get; set; }
    }
}
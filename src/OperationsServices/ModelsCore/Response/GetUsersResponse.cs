using ModelsCore.Common;
using System.Collections.Generic;

namespace ModelsCore.Response
{
    public class GetUsersResponse : BasicResponse
    {
        public List<User> UsersList { get; set; }
        //public GetUsersResponse() { UsersList = new List<User>(); }
    }
}

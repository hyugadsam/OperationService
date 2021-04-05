using Models.Common;
using System.Collections.Generic;

namespace Models.Response
{
    public class GetUsersResponse : BasicResponse
    {
        public List<User> UsersList;

        public GetUsersResponse() { UsersList = new List<User>(); }
    }
}

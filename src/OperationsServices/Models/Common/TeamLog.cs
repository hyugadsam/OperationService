using System;
using System.Collections.Generic;

namespace Models.Common
{
    public class TeamLog:Team
    {
        public List<User> NewUsers { get; set; }
        public DateTime DateOfMovement { get; set; }

        public TeamLog()
        {
            NewUsers = new List<User>();
            Users = new List<User>();
        }

    }
}

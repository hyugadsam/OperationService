﻿using System.Collections.Generic;

namespace ModelsCore.Common
{
    public class Team
    {
        public int Teamid { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public Team() { Users = new List<User>(); }
    }
}

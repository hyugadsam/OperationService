﻿using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class SaveUserRequest
    {
        public User UserInfo { get; set; }
        public bool HasNewPassword { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C.R.E.A.M.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }

    }
}
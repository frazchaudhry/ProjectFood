﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectFood.Domain.Entities
{
    public class Role : IdentityRole<int, UserRole>
    {
        public Role() : base() {}

        public Role(string name)
        {
            Name = name;
        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Models
{
    public class Role : IdentityRole
    {
        // Navigation properties
        public ICollection<UserHasRole> UserRoles { get; set; }
    }
}

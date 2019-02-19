﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocean.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ocean.Context
{
    public class OceanDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public OceanDbContext(DbContextOptions<OceanDbContext> options) : base(options)
        {
        }

       public DbSet<AppUser> AppUsers { get; set; }
    }
}

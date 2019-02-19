using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocean.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Ocean.Context
{
    public class OceanDbContext : DbContext
    {
        public OceanDbContext(DbContextOptions<OceanDbContext> options) : base(options)
        {
        }
    }
}

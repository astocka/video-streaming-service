using System;
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
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryVideo> CategoryVideos { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorVideo> ActorVideos { get; set; }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Ocean.Context;
using Ocean.Models;
using System;

namespace Ocean.Migrations
{
    [DbContext(typeof(OceanDbContext))]
    partial class OceanDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Ocean.Models.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("ActorId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Ocean.Models.ActorVideo", b =>
                {
                    b.Property<int>("ActorVideoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<int>("VideoId");

                    b.HasKey("ActorVideoId");

                    b.HasIndex("ActorId");

                    b.HasIndex("VideoId");

                    b.ToTable("ActorVideos");
                });

            modelBuilder.Entity("Ocean.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Ocean.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ocean.Models.CategoryVideo", b =>
                {
                    b.Property<int>("CategoryVideoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("VideoId");

                    b.HasKey("CategoryVideoId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("VideoId");

                    b.ToTable("CategoryVideos");
                });

            modelBuilder.Entity("Ocean.Models.ProfilePicture", b =>
                {
                    b.Property<int>("ProfilePictureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<string>("ThumbnailFilePath")
                        .IsRequired();

                    b.HasKey("ProfilePictureId");

                    b.ToTable("ProfilePictures");
                });

            modelBuilder.Entity("Ocean.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<int>("Rate");

                    b.Property<int>("UserProfileId");

                    b.Property<int>("VideoId");

                    b.HasKey("RatingId");

                    b.HasIndex("UserProfileId");

                    b.HasIndex("VideoId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Ocean.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppUserId");

                    b.Property<bool?>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ProfilePictureId");

                    b.HasKey("UserProfileId");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Ocean.Models.UserProfileVideo", b =>
                {
                    b.Property<int>("UserProfileVideoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserProfileId");

                    b.Property<int>("VideoId");

                    b.HasKey("UserProfileVideoId");

                    b.HasIndex("UserProfileId");

                    b.HasIndex("VideoId");

                    b.ToTable("UserProfileVideos");
                });

            modelBuilder.Entity("Ocean.Models.Video", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("VideoPath")
                        .IsRequired();

                    b.Property<int>("YearReleased");

                    b.HasKey("VideoId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Ocean.Models.Viewing", b =>
                {
                    b.Property<int>("ViewingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateView");

                    b.Property<int>("UserProfileId");

                    b.Property<int>("VideoId");

                    b.HasKey("ViewingId");

                    b.HasIndex("UserProfileId");

                    b.HasIndex("VideoId");

                    b.ToTable("Viewings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Ocean.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Ocean.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ocean.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Ocean.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ocean.Models.ActorVideo", b =>
                {
                    b.HasOne("Ocean.Models.Actor", "Actors")
                        .WithMany("ActorVideo")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ocean.Models.Video", "Videos")
                        .WithMany("ActorVideo")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ocean.Models.CategoryVideo", b =>
                {
                    b.HasOne("Ocean.Models.Category", "Category")
                        .WithMany("CategoryVideo")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ocean.Models.Video", "Video")
                        .WithMany("CategoryVideo")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ocean.Models.Rating", b =>
                {
                    b.HasOne("Ocean.Models.UserProfile", "UserProfile")
                        .WithMany("Ratings")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ocean.Models.Video", "Video")
                        .WithMany("Ratings")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ocean.Models.UserProfile", b =>
                {
                    b.HasOne("Ocean.Models.AppUser", "AppUser")
                        .WithMany("MyProfiles")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ocean.Models.ProfilePicture", "ProfilePicture")
                        .WithMany("UserProfiles")
                        .HasForeignKey("ProfilePictureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ocean.Models.UserProfileVideo", b =>
                {
                    b.HasOne("Ocean.Models.UserProfile", "UserProfile")
                        .WithMany("UserProfileVideo")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ocean.Models.Video", "Video")
                        .WithMany("UserProfileVideo")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ocean.Models.Viewing", b =>
                {
                    b.HasOne("Ocean.Models.UserProfile", "UserProfile")
                        .WithMany("Viewings")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ocean.Models.Video", "Video")
                        .WithMany("Viewings")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

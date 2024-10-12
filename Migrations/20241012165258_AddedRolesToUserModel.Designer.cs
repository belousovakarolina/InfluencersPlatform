﻿// <auto-generated />
using System;
using InfluencersPlatformBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfluencersPlatformBackend.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241012165258_AddedRolesToUserModel")]
    partial class AddedRolesToUserModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InfluencersPlatformBackend.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FollowersCountFrom")
                        .HasColumnType("int");

                    b.Property<int>("FollowersCountTo")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.CompanyProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<double?>("YearlyIncome")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyProfiles");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.InfluencerProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FbFollowerCount")
                        .HasColumnType("int");

                    b.Property<int?>("IgFollowerCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TiktokFollowerCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("InfluencerProfiles");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyProfileId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InfluencerId")
                        .HasColumnType("int");

                    b.Property<int?>("InfluencerProfileId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAboutInfluencer")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Stars")
                        .HasColumnType("int");

                    b.Property<bool>("Verified")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompanyProfileId");

                    b.HasIndex("InfluencerId");

                    b.HasIndex("InfluencerProfileId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.CompanyProfile", b =>
                {
                    b.HasOne("InfluencersPlatformBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.InfluencerProfile", b =>
                {
                    b.HasOne("InfluencersPlatformBackend.Models.Category", "Category")
                        .WithMany("Influencers")
                        .HasForeignKey("CategoryId");

                    b.HasOne("InfluencersPlatformBackend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.Review", b =>
                {
                    b.HasOne("InfluencersPlatformBackend.Models.User", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfluencersPlatformBackend.Models.CompanyProfile", null)
                        .WithMany("Reviews")
                        .HasForeignKey("CompanyProfileId");

                    b.HasOne("InfluencersPlatformBackend.Models.User", "Influencer")
                        .WithMany()
                        .HasForeignKey("InfluencerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InfluencersPlatformBackend.Models.InfluencerProfile", null)
                        .WithMany("Reviews")
                        .HasForeignKey("InfluencerProfileId");

                    b.Navigation("Company");

                    b.Navigation("Influencer");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.Category", b =>
                {
                    b.Navigation("Influencers");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.CompanyProfile", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("InfluencersPlatformBackend.Models.InfluencerProfile", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VacationPortal.DataAccess.Data;

namespace VacationPortal.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220304134357_initialSchemaAndSeedData")]
    partial class initialSchemaAndSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("VacationPortal.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2022, 3, 4, 17, 43, 56, 532, DateTimeKind.Local).AddTicks(3497),
                            FullName = "Human Resource",
                            ModelStatus = 0,
                            ShortName = "HR"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2022, 3, 4, 17, 43, 56, 536, DateTimeKind.Local).AddTicks(5118),
                            FullName = "Information Technology",
                            ModelStatus = 0,
                            ShortName = "IT"
                        });
                });

            modelBuilder.Entity("VacationPortal.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2022, 3, 4, 17, 43, 56, 588, DateTimeKind.Local).AddTicks(8092),
                            ModelStatus = 0,
                            Name = "Assistant"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2022, 3, 4, 17, 43, 56, 588, DateTimeKind.Local).AddTicks(8531),
                            ModelStatus = 0,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2022, 3, 4, 17, 43, 56, 588, DateTimeKind.Local).AddTicks(8540),
                            ModelStatus = 0,
                            Name = "Director"
                        });
                });

            modelBuilder.Entity("VacationPortal.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("VacationPortal.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "bb3f3dd2-c8d0-4941-a5b3-8771487cb67e",
                            ModelStatus = 0,
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("VacationPortal.Models.VacationApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DaysOfVacation")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("VacationApplications");
                });

            modelBuilder.Entity("VacationPortal.Models.VacationInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("VacationInfos");
                });

            modelBuilder.Entity("VacationPortal.Models.Employee", b =>
                {
                    b.HasBaseType("VacationPortal.Models.User");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "201f1af5-5c3e-4d2a-80df-ea489519e44f",
                            CreatedDate = new DateTime(2022, 3, 4, 17, 43, 56, 542, DateTimeKind.Local).AddTicks(3303),
                            Email = "elgun@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            ModelStatus = 0,
                            NormalizedEmail = "ELGUN@GMAIL.COM",
                            NormalizedUserName = "ELGUN@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEH11w8b1HX0dxDQ+CXeF8LhqYyiAu2OKt4jKtx/HECJjI904654Ndpi2Ic6otVvYYg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cb65be85-590a-4549-ba35-fc45d04c5d0a",
                            TwoFactorEnabled = false,
                            UserName = "elgun@gmail.com",
                            DepartmentId = 2,
                            FirstName = "Elgun",
                            LastName = "Abasquliyev",
                            PositionId = 2
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "0c6ba57f-3a02-4b02-b002-479b8e733763",
                            CreatedDate = new DateTime(2022, 3, 4, 17, 43, 56, 563, DateTimeKind.Local).AddTicks(4946),
                            Email = "senan@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            ModelStatus = 0,
                            NormalizedEmail = "SENAN@GMAIL.COM",
                            NormalizedUserName = "SENAN@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEFUYRbnavZ6VPDKaM7Em7e32EeF7BQdDTswOUQkCe5Zho8UD5Rvf1QlqWf17g9UZlg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "640dd97f-254e-47e1-9526-0d5cf031f8db",
                            TwoFactorEnabled = false,
                            UserName = "senan@gmail.com",
                            DepartmentId = 1,
                            FirstName = "Senan",
                            LastName = "Memmedov",
                            PositionId = 2
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("VacationPortal.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("VacationPortal.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("VacationPortal.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("VacationPortal.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VacationPortal.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("VacationPortal.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VacationPortal.Models.VacationApplication", b =>
                {
                    b.HasOne("VacationPortal.Models.Employee", "Employee")
                        .WithMany("VacationApplications")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("VacationPortal.Models.VacationInfo", b =>
                {
                    b.HasOne("VacationPortal.Models.Position", "Position")
                        .WithMany("VacationInfos")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("VacationPortal.Models.Employee", b =>
                {
                    b.HasOne("VacationPortal.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("VacationPortal.Models.User", null)
                        .WithOne()
                        .HasForeignKey("VacationPortal.Models.Employee", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("VacationPortal.Models.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("VacationPortal.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("VacationPortal.Models.Position", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("VacationInfos");
                });

            modelBuilder.Entity("VacationPortal.Models.Employee", b =>
                {
                    b.Navigation("VacationApplications");
                });
#pragma warning restore 612, 618
        }
    }
}
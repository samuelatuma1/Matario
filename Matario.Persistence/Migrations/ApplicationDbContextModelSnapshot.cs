﻿// <auto-generated />
using System;
using Matario.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Matario.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 9, 27, 9, 24, 11, 880, DateTimeKind.Utc).AddTicks(4040),
                            Description = "Allows users with permission to create Managers",
                            Name = "Create Manager",
                            RecordStatus = 1,
                            UpdatedAt = new DateTime(2023, 9, 27, 9, 24, 11, 880, DateTimeKind.Utc).AddTicks(4050)
                        });
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<bool>("Revoked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UseageCount")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2023, 9, 27, 9, 24, 11, 887, DateTimeKind.Utc).AddTicks(8430),
                            CreatedBy = 0L,
                            Description = "Super Admin Priviledges",
                            Name = "SuperAdmin",
                            RecordStatus = 1,
                            UpdatedAt = new DateTime(2023, 9, 27, 9, 24, 11, 887, DateTimeKind.Utc).AddTicks(8440)
                        });
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.RolesPermissions", b =>
                {
                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolesPermissions");
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RecordStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.UsersRoles", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.RefreshToken", b =>
                {
                    b.HasOne("Matario.Domain.Entities.AuthenticationModule.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.RolesPermissions", b =>
                {
                    b.HasOne("Matario.Domain.Entities.AuthenticationModule.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Matario.Domain.Entities.AuthenticationModule.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Matario.Domain.Entities.AuthenticationModule.UsersRoles", b =>
                {
                    b.HasOne("Matario.Domain.Entities.AuthenticationModule.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Matario.Domain.Entities.AuthenticationModule.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

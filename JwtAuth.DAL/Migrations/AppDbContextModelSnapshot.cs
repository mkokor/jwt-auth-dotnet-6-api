﻿// <auto-generated />
using System;
using JwtAuth.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JwtAuth.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JwtAuth.DAL.Entities.RefreshToken", b =>
                {
                    b.Property<int>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("expires_at");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int")
                        .HasColumnName("owner_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("value");

                    b.HasKey("RefreshTokenId");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("JwtAuth.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("last_name");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FirstName = "John",
                            LastName = "Doe",
                            PasswordHash = new byte[] { 225, 249, 6, 99, 109, 102, 230, 15, 125, 169, 154, 117, 16, 100, 214, 79, 222, 59, 130, 25, 187, 102, 23, 17, 14, 242, 189, 105, 227, 227, 6, 129, 143, 46, 7, 13, 212, 224, 146, 103, 127, 16, 88, 111, 154, 27, 212, 14, 255, 208, 33, 72, 145, 252, 52, 194, 58, 100, 63, 186, 166, 35, 61, 18 },
                            PasswordSalt = new byte[] { 4, 125, 39, 243, 171, 211, 5, 165, 165, 11, 77, 55, 155, 237, 82, 229, 136, 128, 124, 10, 208, 235, 109, 27, 26, 207, 85, 164, 250, 33, 77, 206, 46, 192, 14, 113, 64, 235, 100, 27, 108, 41, 95, 147, 165, 154, 70, 19, 249, 40, 16, 116, 100, 76, 1, 201, 66, 65, 137, 61, 233, 210, 190, 93, 225, 222, 193, 255, 110, 251, 46, 177, 185, 129, 18, 40, 148, 4, 223, 204, 14, 26, 215, 142, 179, 68, 119, 57, 89, 27, 141, 71, 106, 187, 19, 207, 88, 139, 155, 18, 65, 103, 249, 168, 173, 144, 118, 241, 47, 28, 170, 212, 149, 141, 99, 151, 170, 139, 129, 100, 147, 133, 192, 235, 108, 112, 87, 26 },
                            Role = "Admin",
                            Username = "johndoe"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

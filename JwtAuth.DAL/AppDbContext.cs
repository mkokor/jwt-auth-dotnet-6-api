using JwtAuth.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<User> Users { get; set; }

        private void HashPassword(string plaintextPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plaintextPassword));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User
            HashPassword("12345678Aa!", out byte[] adminPasswordHash, out byte[] adminPasswordSalt);

            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasData(new List<User>
                {
                    new User
                    {
                        UserId = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Username = "johndoe",
                        Role = "Admin",
                        PasswordHash = adminPasswordHash,
                        PasswordSalt = adminPasswordSalt
                    }
                });

            modelBuilder.Entity<User>()
                .Property(user => user.UserId)
                .HasColumnName("id");

            modelBuilder.Entity<User>()
                .Property(user => user.FirstName)
                .HasColumnName("first_name");

            modelBuilder.Entity<User>()
                .Property(user => user.LastName)
                .HasColumnName("last_name");
            #endregion
        }
    }
}
using JwtAuth.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User
            modelBuilder.Entity<User>()
                .ToTable("users");

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
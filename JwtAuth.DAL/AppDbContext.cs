using JwtAuth.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuth.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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

            #region RefreshToken
            modelBuilder.Entity<RefreshToken>()
                .ToTable("refresh_tokens");

            modelBuilder.Entity<RefreshToken>()
                .Property(refreshToken => refreshToken.RefreshTokenId)
                .HasColumnName("id");

            modelBuilder.Entity<RefreshToken>()
                .Property(refreshToken => refreshToken.Value)
                .HasColumnName("value");

            modelBuilder.Entity<RefreshToken>()
                .Property(refreshToken => refreshToken.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<RefreshToken>()
                .Property(refreshToken => refreshToken.ExpiresAt)
                .HasColumnName("expires_at");

            modelBuilder.Entity<RefreshToken>()
                .Property(refreshToken => refreshToken.OwnerId)
                .HasColumnName("owner_id");
            #endregion
        }
    }
}
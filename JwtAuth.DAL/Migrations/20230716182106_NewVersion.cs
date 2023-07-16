using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtAuth.DAL.Migrations
{
    public partial class NewVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    owner_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "first_name", "last_name", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { 1, "John", "Doe", new byte[] { 225, 249, 6, 99, 109, 102, 230, 15, 125, 169, 154, 117, 16, 100, 214, 79, 222, 59, 130, 25, 187, 102, 23, 17, 14, 242, 189, 105, 227, 227, 6, 129, 143, 46, 7, 13, 212, 224, 146, 103, 127, 16, 88, 111, 154, 27, 212, 14, 255, 208, 33, 72, 145, 252, 52, 194, 58, 100, 63, 186, 166, 35, 61, 18 }, new byte[] { 4, 125, 39, 243, 171, 211, 5, 165, 165, 11, 77, 55, 155, 237, 82, 229, 136, 128, 124, 10, 208, 235, 109, 27, 26, 207, 85, 164, 250, 33, 77, 206, 46, 192, 14, 113, 64, 235, 100, 27, 108, 41, 95, 147, 165, 154, 70, 19, 249, 40, 16, 116, 100, 76, 1, 201, 66, 65, 137, 61, 233, 210, 190, 93, 225, 222, 193, 255, 110, 251, 46, 177, 185, 129, 18, 40, 148, 4, 223, 204, 14, 26, 215, 142, 179, 68, 119, 57, 89, 27, 141, 71, 106, 187, 19, 207, 88, 139, 155, 18, 65, 103, 249, 168, 173, 144, 118, 241, 47, 28, 170, 212, 149, 141, 99, 151, 170, 139, 129, 100, 147, 133, 192, 235, 108, 112, 87, 26 }, "Admin", "johndoe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

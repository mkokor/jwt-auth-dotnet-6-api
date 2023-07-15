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
                values: new object[] { 1, "John", "Doe", new byte[] { 63, 136, 106, 26, 23, 9, 151, 95, 249, 5, 219, 25, 17, 13, 180, 105, 6, 27, 15, 29, 208, 72, 154, 253, 92, 46, 204, 165, 86, 70, 12, 74, 151, 71, 238, 96, 34, 201, 136, 60, 164, 227, 109, 28, 182, 80, 142, 155, 173, 20, 164, 26, 243, 118, 225, 186, 253, 37, 35, 122, 214, 168, 165, 40 }, new byte[] { 64, 191, 234, 112, 7, 59, 97, 244, 123, 216, 131, 239, 154, 254, 136, 110, 214, 217, 0, 102, 61, 6, 252, 77, 38, 43, 249, 37, 86, 60, 87, 27, 248, 6, 75, 171, 68, 81, 41, 63, 69, 221, 124, 118, 253, 74, 33, 51, 17, 13, 87, 128, 133, 208, 222, 143, 62, 36, 111, 85, 155, 5, 242, 66, 188, 135, 172, 190, 154, 144, 189, 210, 129, 123, 240, 55, 141, 19, 56, 84, 31, 37, 196, 168, 219, 49, 212, 86, 86, 200, 93, 99, 31, 202, 78, 195, 98, 221, 26, 180, 211, 94, 29, 85, 180, 219, 236, 46, 160, 98, 158, 30, 207, 18, 104, 111, 158, 145, 144, 223, 167, 32, 238, 57, 73, 57, 139, 121 }, "Admin", "johndoe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

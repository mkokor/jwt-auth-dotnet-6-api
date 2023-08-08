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

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    value_hash = table.Column<byte[]>(type: "longblob", nullable: false),
                    value_salt = table.Column<byte[]>(type: "longblob", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    owner_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "first_name", "last_name", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { 1, "John", "Doe", new byte[] { 113, 37, 9, 78, 97, 203, 134, 230, 56, 123, 217, 43, 43, 145, 21, 74, 88, 87, 181, 42, 242, 37, 125, 97, 221, 117, 126, 6, 198, 206, 127, 196, 162, 1, 218, 163, 187, 166, 111, 200, 176, 173, 131, 107, 148, 204, 29, 101, 181, 51, 202, 43, 135, 132, 28, 153, 204, 36, 251, 247, 30, 151, 29, 25 }, new byte[] { 213, 106, 229, 84, 147, 34, 85, 3, 101, 208, 96, 40, 48, 169, 223, 21, 158, 100, 238, 47, 96, 171, 255, 82, 145, 186, 69, 76, 133, 79, 155, 49, 227, 57, 39, 225, 7, 31, 99, 58, 70, 194, 71, 164, 107, 250, 115, 174, 63, 179, 109, 17, 221, 154, 152, 117, 123, 69, 216, 55, 19, 109, 255, 100, 69, 88, 237, 169, 1, 243, 234, 6, 44, 134, 81, 172, 128, 22, 211, 88, 23, 118, 39, 254, 137, 219, 186, 22, 135, 0, 227, 205, 98, 115, 229, 54, 96, 213, 16, 154, 207, 50, 42, 208, 199, 98, 49, 143, 217, 198, 250, 164, 30, 161, 241, 177, 205, 144, 112, 135, 102, 223, 106, 43, 215, 241, 202, 2 }, "Admin", "johndoe" });

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_owner_id",
                table: "refresh_tokens",
                column: "owner_id");
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

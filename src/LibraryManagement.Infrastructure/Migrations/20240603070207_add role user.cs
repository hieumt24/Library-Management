using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addroleuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24c286ba-a8cd-4a5f-b0be-05d067424aac", "843ef537-0078-496e-8fbd-ac24d3caca8d", "SuperUser", "SUPERUSER" },
                    { "332ffd7b-2305-49a5-a479-24307421df4a", "332ffd7b-2305-49a5-a479-24307421df4a", "User", "USER" },
                    { "843ef537-0078-496e-8fbd-ac24d3caca8d", "843ef537-0078-496e-8fbd-ac24d3caca8d", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "24c286ba-a8cd-4a5f-b0be-05d067424aac");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "332ffd7b-2305-49a5-a479-24307421df4a");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "843ef537-0078-496e-8fbd-ac24d3caca8d");
        }
    }
}

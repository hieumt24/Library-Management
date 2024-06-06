using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updateroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "d54e5765-0289-43d7-8177-2a010ac2e8e8");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "843ef537-0078-496e-8fbd-ac24d3caca8d",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "SuperUser", "SUPERUSER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "843ef537-0078-496e-8fbd-ac24d3caca8d",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d54e5765-0289-43d7-8177-2a010ac2e8e8", "843ef537-0078-496e-8fbd-ac24d3caca8d", "SuperUser", "SUPERUSER" });
        }
    }
}

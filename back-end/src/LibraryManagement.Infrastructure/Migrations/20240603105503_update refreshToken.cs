using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_User_UserId",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "24c286ba-a8cd-4a5f-b0be-05d067424aac");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "ReplacedByToken",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Revoked",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "RevokedByIp",
                table: "RefreshTokens",
                newName: "JwtId");

            migrationBuilder.RenameColumn(
                name: "Expires",
                table: "RefreshTokens",
                newName: "ExpiryDate");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "RefreshTokens",
                newName: "CreationDate");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RefreshTokens",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Invalidated",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Used",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e3bcb81-43a0-464e-897c-6e1ca36ab234", "843ef537-0078-496e-8fbd-ac24d3caca8d", "SuperUser", "SUPERUSER" });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_User_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_User_UserId",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "0e3bcb81-43a0-464e-897c-6e1ca36ab234");

            migrationBuilder.DropColumn(
                name: "Invalidated",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Used",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "JwtId",
                table: "RefreshTokens",
                newName: "RevokedByIp");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "RefreshTokens",
                newName: "Expires");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "RefreshTokens",
                newName: "Created");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RefreshTokens",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReplacedByToken",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Revoked",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24c286ba-a8cd-4a5f-b0be-05d067424aac", "843ef537-0078-496e-8fbd-ac24d3caca8d", "SuperUser", "SUPERUSER" });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_User_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}

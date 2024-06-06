using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetypebookborrowingRequestIdversion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequestDetails_BookBorrowingRequests_BookBorrowingRequestId1",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookBorrowingRequestDetails_BookBorrowingRequestId1",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropColumn(
                name: "BookBorrowingRequestId1",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookBorrowingRequestId",
                table: "BookBorrowingRequestDetails",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_BookBorrowingRequestId",
                table: "BookBorrowingRequestDetails",
                column: "BookBorrowingRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequestDetails_BookBorrowingRequests_BookBorrowingRequestId",
                table: "BookBorrowingRequestDetails",
                column: "BookBorrowingRequestId",
                principalTable: "BookBorrowingRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBorrowingRequestDetails_BookBorrowingRequests_BookBorrowingRequestId",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookBorrowingRequestDetails_BookBorrowingRequestId",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BookBorrowingRequestId",
                table: "BookBorrowingRequestDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BookBorrowingRequestId1",
                table: "BookBorrowingRequestDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingRequestDetails_BookBorrowingRequestId1",
                table: "BookBorrowingRequestDetails",
                column: "BookBorrowingRequestId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBorrowingRequestDetails_BookBorrowingRequests_BookBorrowingRequestId1",
                table: "BookBorrowingRequestDetails",
                column: "BookBorrowingRequestId1",
                principalTable: "BookBorrowingRequests",
                principalColumn: "Id");
        }
    }
}

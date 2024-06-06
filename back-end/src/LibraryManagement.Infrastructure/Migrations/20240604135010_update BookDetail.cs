using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBookDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BookBorrowingRequestDetails");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BookBorrowingRequestDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "BookBorrowingRequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BookBorrowingRequestDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BookBorrowingRequestDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BookBorrowingRequestDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

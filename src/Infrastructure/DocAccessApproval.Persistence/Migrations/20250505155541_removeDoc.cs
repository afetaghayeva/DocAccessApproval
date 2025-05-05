using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocAccessApproval.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeDoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Documents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Documents",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }
    }
}

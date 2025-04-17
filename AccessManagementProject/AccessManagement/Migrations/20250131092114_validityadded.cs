using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessManagement.Migrations
{
    /// <inheritdoc />
    public partial class validityadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "validity",
                table: "userEmail",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "validity",
                table: "userEmail");
        }
    }
}

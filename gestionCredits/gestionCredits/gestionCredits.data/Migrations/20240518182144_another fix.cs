using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestionCredits.data.Migrations
{
    /// <inheritdoc />
    public partial class anotherfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "filepath",
                table: "CreditApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "filepath",
                table: "CreditApplications");
        }
    }
}

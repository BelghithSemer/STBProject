using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestionCredits.data.Migrations
{
    /// <inheritdoc />
    public partial class files3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "filepath",
                table: "CreditApplications",
                newName: "FilePaths");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePaths",
                table: "CreditApplications",
                newName: "filepath");
        }
    }
}

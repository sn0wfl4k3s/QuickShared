using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickShared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mssqlonprem_migration_479 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_path",
                schema: "quickshared",
                table: "manager_files",
                newName: "file_url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "file_url",
                schema: "quickshared",
                table: "manager_files",
                newName: "file_path");
        }
    }
}

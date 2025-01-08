using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeTracker.Context.Migrations
{
    /// <inheritdoc />
    public partial class keyadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DesignationKey",
                table: "Designations",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentKey",
                table: "Departments",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignationKey",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "DepartmentKey",
                table: "Departments");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanSolidApp.src.Data.Persistence.Migrations
{
    public partial class ChangeLeaveAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "LeaveAllocations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "LeaveAllocations");
        }
    }
}

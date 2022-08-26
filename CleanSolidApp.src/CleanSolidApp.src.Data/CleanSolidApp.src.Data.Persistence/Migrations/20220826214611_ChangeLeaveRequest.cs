using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanSolidApp.src.Data.Persistence.Migrations
{
    public partial class ChangeLeaveRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestingEmployeeID",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestingEmployeeID",
                table: "LeaveRequests");
        }
    }
}

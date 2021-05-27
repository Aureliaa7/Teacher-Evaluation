using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class AddedMaxNumberOfAttendances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxNumberOfAttendances",
                table: "TaughtSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxNumberOfAttendances",
                table: "TaughtSubjects");
        }
    }
}

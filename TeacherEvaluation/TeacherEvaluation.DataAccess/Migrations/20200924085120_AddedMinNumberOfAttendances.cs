using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class AddedMinNumberOfAttendances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForEnrollmentState",
                table: "Forms");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentState",
                table: "Forms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinNumberOfAttendances",
                table: "Forms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollmentState",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "MinNumberOfAttendances",
                table: "Forms");

            migrationBuilder.AddColumn<int>(
                name: "ForEnrollmentState",
                table: "Forms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

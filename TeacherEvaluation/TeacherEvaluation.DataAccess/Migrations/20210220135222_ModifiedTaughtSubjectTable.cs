using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class ModifiedTaughtSubjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "TaughtSubjects",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "StudyProgramme",
                table: "TaughtSubjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyYear",
                table: "TaughtSubjects",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semester",
                table: "TaughtSubjects");

            migrationBuilder.DropColumn(
                name: "StudyProgramme",
                table: "TaughtSubjects");

            migrationBuilder.DropColumn(
                name: "StudyYear",
                table: "TaughtSubjects");
        }
    }
}

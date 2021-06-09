using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class DBUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Answers",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
               name: "FreeFormAnswer",
               table: "Answers",
               type: "nvarchar(max)",
               nullable: true,
               defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
               name: "FreeFormAnswer",
               table: "Answers",
               type: "nvarchar(max)",
               nullable: false,
               defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class AddedFormsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionWithOptionAnswers_Form_FormId",
                table: "QuestionWithOptionAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Form",
                table: "Form");

            migrationBuilder.RenameTable(
                name: "Form",
                newName: "Forms");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forms",
                table: "Forms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionWithOptionAnswers_Forms_FormId",
                table: "QuestionWithOptionAnswers",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionWithOptionAnswers_Forms_FormId",
                table: "QuestionWithOptionAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forms",
                table: "Forms");

            migrationBuilder.RenameTable(
                name: "Forms",
                newName: "Form");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Form",
                table: "Form",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionWithOptionAnswers_Form_FormId",
                table: "QuestionWithOptionAnswers",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

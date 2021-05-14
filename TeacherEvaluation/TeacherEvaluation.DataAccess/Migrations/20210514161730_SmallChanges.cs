using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class SmallChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerToQuestions_Enrollments_EnrollmentId",
                table: "AnswerToQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerToQuestions_Questions_QuestionId",
                table: "AnswerToQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerToQuestions",
                table: "AnswerToQuestions");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "AnswerToQuestions");

            migrationBuilder.RenameTable(
                name: "AnswerToQuestions",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerToQuestions_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerToQuestions_EnrollmentId",
                table: "Answers",
                newName: "IX_Answers_EnrollmentId");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Enrollments_EnrollmentId",
                table: "Answers",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Enrollments_EnrollmentId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "AnswerToQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "AnswerToQuestions",
                newName: "IX_AnswerToQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_EnrollmentId",
                table: "AnswerToQuestions",
                newName: "IX_AnswerToQuestions_EnrollmentId");

            migrationBuilder.AddColumn<int>(
                name: "Answer",
                table: "AnswerToQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerToQuestions",
                table: "AnswerToQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestions_Enrollments_EnrollmentId",
                table: "AnswerToQuestions",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestions_Questions_QuestionId",
                table: "AnswerToQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

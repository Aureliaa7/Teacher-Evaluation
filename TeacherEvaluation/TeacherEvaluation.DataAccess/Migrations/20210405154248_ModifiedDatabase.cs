using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class ModifiedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerToQuestionWithTexts_Enrollments_EnrollmentId",
                table: "AnswerToQuestionWithTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerToQuestionWithTexts_Questions_QuestionId",
                table: "AnswerToQuestionWithTexts");

            migrationBuilder.DropTable(
                name: "AnswerToQuestionWithOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerToQuestionWithTexts",
                table: "AnswerToQuestionWithTexts");

            migrationBuilder.RenameTable(
                name: "AnswerToQuestionWithTexts",
                newName: "AnswerToQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerToQuestionWithTexts_QuestionId",
                table: "AnswerToQuestions",
                newName: "IX_AnswerToQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerToQuestionWithTexts_EnrollmentId",
                table: "AnswerToQuestions",
                newName: "IX_AnswerToQuestions_EnrollmentId");

            migrationBuilder.AddColumn<bool>(
                name: "HasFreeFormAnswer",
                table: "Questions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Answer",
                table: "AnswerToQuestions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AnswerToQuestions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeForm",
                table: "AnswerToQuestions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FreeFormAnswer",
                table: "AnswerToQuestions",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestions_Questions_QuestionId",
                table: "AnswerToQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "HasFreeFormAnswer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AnswerToQuestions");

            migrationBuilder.DropColumn(
                name: "IsFreeForm",
                table: "AnswerToQuestions");

            migrationBuilder.DropColumn(
                name: "FreeFormAnswer",
                table: "AnswerToQuestions");

            migrationBuilder.RenameTable(
                name: "AnswerToQuestions",
                newName: "AnswerToQuestionWithTexts");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerToQuestions_QuestionId",
                table: "AnswerToQuestionWithTexts",
                newName: "IX_AnswerToQuestionWithTexts_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerToQuestions_EnrollmentId",
                table: "AnswerToQuestionWithTexts",
                newName: "IX_AnswerToQuestionWithTexts_EnrollmentId");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "AnswerToQuestionWithTexts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerToQuestionWithTexts",
                table: "AnswerToQuestionWithTexts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AnswerToQuestionWithOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerToQuestionWithOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerToQuestionWithOptions_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerToQuestionWithOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_EnrollmentId",
                table: "AnswerToQuestionWithOptions",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_QuestionId",
                table: "AnswerToQuestionWithOptions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestionWithTexts_Enrollments_EnrollmentId",
                table: "AnswerToQuestionWithTexts",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestionWithTexts_Questions_QuestionId",
                table: "AnswerToQuestionWithTexts",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

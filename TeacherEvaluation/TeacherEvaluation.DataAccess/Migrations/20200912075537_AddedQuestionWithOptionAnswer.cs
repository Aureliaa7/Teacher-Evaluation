using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class AddedQuestionWithOptionAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionWithOptionAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Question = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionWithOptionAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnswerToQuestionWithOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    FormId = table.Column<Guid>(nullable: false),
                    QuestionWithOptionAnswerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerToQuestionWithOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerToQuestionWithOptions_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerToQuestionWithOptions_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerToQuestionWithOptions_QuestionWithOptionAnswers_QuestionWithOptionAnswerId",
                        column: x => x.QuestionWithOptionAnswerId,
                        principalTable: "QuestionWithOptionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_EnrollmentId",
                table: "AnswerToQuestionWithOptions",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_FormId",
                table: "AnswerToQuestionWithOptions",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_QuestionWithOptionAnswerId",
                table: "AnswerToQuestionWithOptions",
                column: "QuestionWithOptionAnswerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerToQuestionWithOptions");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "QuestionWithOptionAnswers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class DatabaseChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerToQuestionWithOptions_Form_FormId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.DropIndex(
                name: "IX_AnswerToQuestionWithOptions_FormId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "FormId",
                table: "QuestionWithOptionAnswers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Form",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionWithOptionAnswers_FormId",
                table: "QuestionWithOptionAnswers",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionWithOptionAnswers_Form_FormId",
                table: "QuestionWithOptionAnswers",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionWithOptionAnswers_Form_FormId",
                table: "QuestionWithOptionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionWithOptionAnswers_FormId",
                table: "QuestionWithOptionAnswers");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "QuestionWithOptionAnswers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Form");

            migrationBuilder.AddColumn<Guid>(
                name: "FormId",
                table: "AnswerToQuestionWithOptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_FormId",
                table: "AnswerToQuestionWithOptions",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestionWithOptions_Form_FormId",
                table: "AnswerToQuestionWithOptions",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

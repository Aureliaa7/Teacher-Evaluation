using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class ModifiedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerToQuestionWithOptions_QuestionWithOptionAnswers_QuestionWithOptionAnswerId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.DropIndex(
                name: "IX_AnswerToQuestionWithOptions_QuestionWithOptionAnswerId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.DropColumn(
                name: "QuestionWithOptionAnswerId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "AnswerToQuestionWithOptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_QuestionId",
                table: "AnswerToQuestionWithOptions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestionWithOptions_QuestionWithOptionAnswers_QuestionId",
                table: "AnswerToQuestionWithOptions",
                column: "QuestionId",
                principalTable: "QuestionWithOptionAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerToQuestionWithOptions_QuestionWithOptionAnswers_QuestionId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.DropIndex(
                name: "IX_AnswerToQuestionWithOptions_QuestionId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "AnswerToQuestionWithOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionWithOptionAnswerId",
                table: "AnswerToQuestionWithOptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerToQuestionWithOptions_QuestionWithOptionAnswerId",
                table: "AnswerToQuestionWithOptions",
                column: "QuestionWithOptionAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerToQuestionWithOptions_QuestionWithOptionAnswers_QuestionWithOptionAnswerId",
                table: "AnswerToQuestionWithOptions",
                column: "QuestionWithOptionAnswerId",
                principalTable: "QuestionWithOptionAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

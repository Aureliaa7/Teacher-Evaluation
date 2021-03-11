using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class RemovedRedundantFieldsFromSubjectsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_StudyDomains_StudyDomainId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_StudyDomainId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudyDomainId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudyProgramme",
                table: "Subjects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudyDomainId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyProgramme",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudyDomainId",
                table: "Subjects",
                column: "StudyDomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_StudyDomains_StudyDomainId",
                table: "Subjects",
                column: "StudyDomainId",
                principalTable: "StudyDomains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

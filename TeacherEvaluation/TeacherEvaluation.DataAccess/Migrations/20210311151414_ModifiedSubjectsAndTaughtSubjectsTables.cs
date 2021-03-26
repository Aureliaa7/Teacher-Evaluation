using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class ModifiedSubjectsAndTaughtSubjectsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "SpecializationId",
                table: "Subjects",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StudyDomainId",
                table: "Subjects",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "StudyProgramme",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyYear",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SpecializationId",
                table: "Subjects",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudyDomainId",
                table: "Subjects",
                column: "StudyDomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Specializations_SpecializationId",
                table: "Subjects",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_StudyDomains_StudyDomainId",
                table: "Subjects",
                column: "StudyDomainId",
                principalTable: "StudyDomains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Specializations_SpecializationId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_StudyDomains_StudyDomainId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SpecializationId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_StudyDomainId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudyDomainId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudyProgramme",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudyYear",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "TaughtSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyProgramme",
                table: "TaughtSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyYear",
                table: "TaughtSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

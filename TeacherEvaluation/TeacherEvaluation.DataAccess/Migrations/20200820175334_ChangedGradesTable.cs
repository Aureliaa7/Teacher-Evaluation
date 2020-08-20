using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class ChangedGradesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("065b4886-25eb-4f7a-aa4a-fa08ce5c79f2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12579959-46a0-4ea8-990a-5cf67e446fdf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("15c003a8-36c0-4c3d-887f-f8851a2d15f7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8a5bb92b-5606-4a7d-8303-e6f13c670536"));

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Grades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Grades",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("38fbaf3f-1ca2-4f9a-818d-bf7cd98541e6"), "d415202c-f07e-4886-9122-421b5599103a", "Administrator", "ADMINISTRATOR" },
                    { new Guid("8460ae5c-f0ea-4a42-a09b-e4865a30f20e"), "317bfa2f-6593-4d68-bbd5-3eb06ab67b8f", "Dean", "DEAN" },
                    { new Guid("08b599e5-709a-4249-bfc8-10e7d1ff8f34"), "3430cbe2-c331-4736-9541-00680223f20a", "Student", "STUDENT" },
                    { new Guid("6ffafc63-baf7-4651-b218-5296a48efa1c"), "7f3a1fb8-5e71-4912-bd48-712aab437cbd", "Teacher", "TEACHER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("08b599e5-709a-4249-bfc8-10e7d1ff8f34"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("38fbaf3f-1ca2-4f9a-818d-bf7cd98541e6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6ffafc63-baf7-4651-b218-5296a48efa1c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8460ae5c-f0ea-4a42-a09b-e4865a30f20e"));

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Grades",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Grades",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("15c003a8-36c0-4c3d-887f-f8851a2d15f7"), "a4a528a0-1078-4d02-afad-2df6d9fda207", "Administrator", "ADMINISTRATOR" },
                    { new Guid("8a5bb92b-5606-4a7d-8303-e6f13c670536"), "bd909787-cf4f-407c-9d1c-e15a86fb0e50", "Dean", "DEAN" },
                    { new Guid("065b4886-25eb-4f7a-aa4a-fa08ce5c79f2"), "495fe70b-9c74-41a9-a4e5-aa1358473187", "Student", "STUDENT" },
                    { new Guid("12579959-46a0-4ea8-990a-5cf67e446fdf"), "2b7df56b-2494-47c8-a9c1-5b94e1052f96", "Teacher", "TEACHER" }
                });
        }
    }
}

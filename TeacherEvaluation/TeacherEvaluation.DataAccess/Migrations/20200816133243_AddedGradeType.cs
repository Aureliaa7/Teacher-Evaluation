using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class AddedGradeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("177420fa-fca8-4151-aac5-61f1b9e4e6bf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("604b78ad-3dbe-4bc3-9223-bf0ba1d32f9f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e97dc45e-ce5a-484d-b87d-1465c4c12254"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f9bce92d-04e0-4b11-a93d-460c1c09a481"));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfCredits",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Grades",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "NumberOfCredits",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Grades");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("604b78ad-3dbe-4bc3-9223-bf0ba1d32f9f"), "2bad2c78-6b63-4031-91ab-bfedb05ffb14", "Administrator", "ADMINISTRATOR" },
                    { new Guid("f9bce92d-04e0-4b11-a93d-460c1c09a481"), "ca1a02c8-a275-42c2-bba6-cb00f55e6147", "Dean", "DEAN" },
                    { new Guid("177420fa-fca8-4151-aac5-61f1b9e4e6bf"), "7a2d3496-45b2-48c2-a5c7-be1eacab25d3", "Student", "STUDENT" },
                    { new Guid("e97dc45e-ce5a-484d-b87d-1465c4c12254"), "b836cfa6-08e0-46f0-b104-1000d93b3432", "Teacher", "TEACHER" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class RemovedAddRolesFromContextClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

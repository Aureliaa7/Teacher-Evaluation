using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherEvaluation.DataAccess.Migrations
{
    public partial class AddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0d9092da-2b51-4458-bb96-a6876fe62fb2"), "13ca3a21-f9c4-474e-8a00-980d2537164d", "Administrator", "ADMINISTRATOR" },
                    { new Guid("9cf1b1f5-bb32-4bcf-af7e-de0472f97f3e"), "2a0ea62f-fc91-4c63-8c2f-daf0177719d4", "Dean", "DEAN" },
                    { new Guid("0574a5e6-16b9-4dba-9640-236c483b811b"), "cd77fd76-c622-45e9-9fdd-46df27b4f009", "Student", "STUDENT" },
                    { new Guid("90403302-3dea-4a0c-84fa-5c84505c0b38"), "67abbe24-7f18-4330-950b-6c0577497402", "Teacher", "TEACHER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0574a5e6-16b9-4dba-9640-236c483b811b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0d9092da-2b51-4458-bb96-a6876fe62fb2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("90403302-3dea-4a0c-84fa-5c84505c0b38"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9cf1b1f5-bb32-4bcf-af7e-de0472f97f3e"));
        }
    }
}

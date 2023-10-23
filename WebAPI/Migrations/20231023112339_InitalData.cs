using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "SchoolId", "Address", "City", "Country", "NameAndNumber" },
                values: new object[,]
                {
                    { new Guid("03544a2b-3d4a-437f-9037-e67d1e8de1ef"), "ул. Коваленко, д.13", "Саранск", "Россия", "Школа №34" },
                    { new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"), " б-р Степана Эрьзи, 28A", "Саранск", "Россия", "Школа №11" }
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "NumberOfSeats", "SchoolId", "Type" },
                values: new object[,]
                {
                    { new Guid("92cb4401-f6e6-4a64-975e-1234c8df0f8c"), 20, new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"), "Physics classroom" },
                    { new Guid("b5674d25-e907-494a-83d1-2b8a37e3a4bc"), 18, new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"), "Literature classroom" },
                    { new Guid("f32ff1c1-2831-4b89-a557-6b0621479c1d"), 15, new Guid("03544a2b-3d4a-437f-9037-e67d1e8de1ef"), "Chemistry classroom" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 35, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Kane Miller", "Administrator" },
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 26, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sam Raiden", "Software developer" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 30, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Jana McLeaf", "Software developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("92cb4401-f6e6-4a64-975e-1234c8df0f8c"));

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("b5674d25-e907-494a-83d1-2b8a37e3a4bc"));

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("f32ff1c1-2831-4b89-a557-6b0621479c1d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolId",
                keyValue: new Guid("03544a2b-3d4a-437f-9037-e67d1e8de1ef"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "SchoolId",
                keyValue: new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"));

            migrationBuilder.DropColumn(
                name: "City",
                table: "Schools");
        }
    }
}

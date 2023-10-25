using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("9af5a2a7-0b76-4e21-ae21-d352084cb901"));

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("af3d2de6-f7df-48fd-bcf5-fec0b229f673"));

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("f930109c-a226-4f71-b116-36e42168ad1e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0aa6e92d-2aa0-4389-9385-88f8cd234d21", "9b414b6a-3dee-4400-86d4-a7be54a3c725", "Administrator", "ADMINISTRATOR" },
                    { "dec0a608-be71-4757-a96c-8baf141f5dd1", "75870c1b-2492-43eb-bda2-0caff1752be9", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "NumberOfSeats", "SchoolId", "Type" },
                values: new object[,]
                {
                    { new Guid("2e151968-ebc4-406d-b3ce-2eee61f469a4"), 18, new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"), "Literature classroom" },
                    { new Guid("bf44b2dd-f559-4e38-ab09-6a23a4a6e121"), 15, new Guid("03544a2b-3d4a-437f-9037-e67d1e8de1ef"), "Chemistry classroom" },
                    { new Guid("d129e475-22a2-45ce-98ad-4e5daf7bfafa"), 20, new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"), "Physics classroom" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aa6e92d-2aa0-4389-9385-88f8cd234d21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dec0a608-be71-4757-a96c-8baf141f5dd1");

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("2e151968-ebc4-406d-b3ce-2eee61f469a4"));

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("bf44b2dd-f559-4e38-ab09-6a23a4a6e121"));

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "ClassroomId",
                keyValue: new Guid("d129e475-22a2-45ce-98ad-4e5daf7bfafa"));

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "ClassroomId", "NumberOfSeats", "SchoolId", "Type" },
                values: new object[,]
                {
                    { new Guid("9af5a2a7-0b76-4e21-ae21-d352084cb901"), 18, new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"), "Literature classroom" },
                    { new Guid("af3d2de6-f7df-48fd-bcf5-fec0b229f673"), 15, new Guid("03544a2b-3d4a-437f-9037-e67d1e8de1ef"), "Chemistry classroom" },
                    { new Guid("f930109c-a226-4f71-b116-36e42168ad1e"), 20, new Guid("3687d01d-bfbc-44f4-a837-9447ec8190be"), "Physics classroom" }
                });
        }
    }
}

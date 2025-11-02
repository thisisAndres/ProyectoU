using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Persons_OwnerId",
                table: "Vehicles");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 3, "ana@correo.com", "Ana", "Torres" },
                    { 4, "luis@correo.com", "Luis", "Pérez" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "Model", "OwnerId", "Plate" },
                values: new object[,]
                {
                    { 3, "Honda", "Civic", null, "KLM456" },
                    { 4, "Toyota", "Yaris", 3, "ABC123" },
                    { 5, "Hyundai", "Accent", 4, "XYZ789" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Persons_OwnerId",
                table: "Vehicles",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Persons_OwnerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles");

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Persons_OwnerId",
                table: "Vehicles",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}

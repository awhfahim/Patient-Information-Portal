using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDropdownTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Allergies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1506a76e-7e7a-4c66-9376-fa18b8c5823d"), "Peanut" },
                    { new Guid("6d298df9-bf03-469f-be93-fb62d73078c2"), "Dust Mite" },
                    { new Guid("6f0c798c-bdb0-43a8-a2ab-e23a74bbb99a"), "Shellfish" },
                    { new Guid("ac98f120-06a6-4a6b-8736-08c523b01e39"), "Pollen" },
                    { new Guid("fcba47b1-961c-4b9d-9a39-58ee4edc027e"), "Penicillin" }
                });

            migrationBuilder.InsertData(
                table: "DiseaseInfos",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10ae8881-bac9-4bc8-b2b9-e2515e510db4"), "Tuberculosis" },
                    { new Guid("5a590390-2adf-4013-8084-15e0a9824063"), "Diarrhea" },
                    { new Guid("7a8c12b1-19e8-454a-8d5e-1b84d9ea3fb1"), "Malaria" },
                    { new Guid("9246d879-589e-4cd1-aa66-54d44f5eb334"), "Pneumonia" },
                    { new Guid("dc239fcc-e87b-4f44-8f12-79c7fc05173a"), "HIV/AIDS" }
                });

            migrationBuilder.InsertData(
                table: "NCDs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3d4e9629-4fe3-4865-96ab-183b6f542c39"), "Chronic Respiratory" },
                    { new Guid("844153bb-9033-4718-9f24-2a73ab0430fd"), "Cardiovascular" },
                    { new Guid("a4f38f24-9fc2-4a5b-85dd-0053f24271aa"), "Diabetes" },
                    { new Guid("aad6e4c3-4a09-48e3-b77d-88f43b7f95bc"), "Mental Health Disorders" },
                    { new Guid("c4468bb9-60d2-44e9-a656-de576c88bf3d"), "Cancer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: new Guid("1506a76e-7e7a-4c66-9376-fa18b8c5823d"));

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: new Guid("6d298df9-bf03-469f-be93-fb62d73078c2"));

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: new Guid("6f0c798c-bdb0-43a8-a2ab-e23a74bbb99a"));

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: new Guid("ac98f120-06a6-4a6b-8736-08c523b01e39"));

            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: new Guid("fcba47b1-961c-4b9d-9a39-58ee4edc027e"));

            migrationBuilder.DeleteData(
                table: "DiseaseInfos",
                keyColumn: "Id",
                keyValue: new Guid("10ae8881-bac9-4bc8-b2b9-e2515e510db4"));

            migrationBuilder.DeleteData(
                table: "DiseaseInfos",
                keyColumn: "Id",
                keyValue: new Guid("5a590390-2adf-4013-8084-15e0a9824063"));

            migrationBuilder.DeleteData(
                table: "DiseaseInfos",
                keyColumn: "Id",
                keyValue: new Guid("7a8c12b1-19e8-454a-8d5e-1b84d9ea3fb1"));

            migrationBuilder.DeleteData(
                table: "DiseaseInfos",
                keyColumn: "Id",
                keyValue: new Guid("9246d879-589e-4cd1-aa66-54d44f5eb334"));

            migrationBuilder.DeleteData(
                table: "DiseaseInfos",
                keyColumn: "Id",
                keyValue: new Guid("dc239fcc-e87b-4f44-8f12-79c7fc05173a"));

            migrationBuilder.DeleteData(
                table: "NCDs",
                keyColumn: "Id",
                keyValue: new Guid("3d4e9629-4fe3-4865-96ab-183b6f542c39"));

            migrationBuilder.DeleteData(
                table: "NCDs",
                keyColumn: "Id",
                keyValue: new Guid("844153bb-9033-4718-9f24-2a73ab0430fd"));

            migrationBuilder.DeleteData(
                table: "NCDs",
                keyColumn: "Id",
                keyValue: new Guid("a4f38f24-9fc2-4a5b-85dd-0053f24271aa"));

            migrationBuilder.DeleteData(
                table: "NCDs",
                keyColumn: "Id",
                keyValue: new Guid("aad6e4c3-4a09-48e3-b77d-88f43b7f95bc"));

            migrationBuilder.DeleteData(
                table: "NCDs",
                keyColumn: "Id",
                keyValue: new Guid("c4468bb9-60d2-44e9-a656-de576c88bf3d"));
        }
    }
}

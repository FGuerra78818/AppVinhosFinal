using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppVinhosFinal.Migrations
{
    /// <inheritdoc />
    public partial class f3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuintaId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4633d7f-e6ec-4311-a32f-94a5b64c1f0d", "AQAAAAIAAYagAAAAEMNcrTkkRPwefWPr5TcVOdhqoVEpY5kkf3iYptxbJ4p9tTSkLBu38xIqW8S04XUBSg==", "8516cc6a-bfa5-4ef3-ac37-6522ea913bb9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c830f1d3-ad09-4192-b199-3b9e666199c1", "AQAAAAIAAYagAAAAEBTK4ReXHnDf19RcquDavLTEVvnmEKiMLM5zWvhWuDHgWlbPwJFX9he5wDIn8GcCQQ==", "b340aa6f-a7e6-48a9-a76b-abc62d6ec6ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a807b23-765c-4bbc-9b5a-8dd30a2dee2f", "AQAAAAIAAYagAAAAEChhfpOMzuD8TYEo4LNuL3aoCaV1ozkEVu4xbIrquwdHRlEfR33UyWtjAelA8KfJmQ==", "f880de55-1fea-4e41-9c19-4633ed56eeec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7376f75-f836-4dec-8f5e-5c33be9bcf50", "AQAAAAIAAYagAAAAECxuVR+aA9LhbvSq1rViGUNNAtCB02mx8bGeKKd9D5BbSmpoCQnjAi+8SglHZcws5A==", "d8dff279-13a5-4f2e-b4e8-f6c6003ddede" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd911641-3b59-44e5-be5a-3185f2634e34", "AQAAAAIAAYagAAAAEKWkbB+dBe5jXIF4SQPWfLx7OnHIGaA6xCHpUvO/2DvG5wmE0cAlpHCTD0pIFQLMZQ==", "6cb184da-c9c0-4180-8d50-0518164c16b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81102241-232a-4334-b2e4-3428a82284ec", "AQAAAAIAAYagAAAAEDYlwbU0Y5vVgBtb1xAC/Wb8gMk3+97YqmWQx+bLu06qgPPZ6I7FumMvIBI+bjjaSw==", "a475d617-a7a9-4a07-837e-9e5bfb39b922" });

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 6,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 7,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 8,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 9,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 10,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 11,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 12,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 13,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 14,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 15,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 16,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 17,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 18,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 19,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 20,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 21,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 22,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 23,
                column: "CapacidadeFria",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 24,
                column: "CapacidadeFria",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a10a750-9f9f-42ab-b00f-7a92faf04ef3", "AQAAAAIAAYagAAAAEJ87f7T0vBoBQgP8jzbb+hu9CJTfIu5nO9HAS4n/r3EqRc+2k2FpfjxSqQp1oHRi3A==", "8084b8d8-cd11-4b1a-af5e-d169424185c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8653afc0-365d-44af-adb4-24151f10d7ca", "AQAAAAIAAYagAAAAEMbSdO+ysIaL3e74MfMtTiy4wNFwefF8/yIcwIIHxqGFKJ3xYEsyI5moYThpH4N8sg==", "586ac775-dc99-49ee-b1f7-cd11f94313c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77eb8aa5-a637-41b3-a462-0b90556b88d2", "AQAAAAIAAYagAAAAEHJAFgEWseqm91GP6JERHuEZgYSO9g0Y796J4EhhOcmdNGgrmC3n7D4jMI0PfD0WIg==", "ae7c6076-5a6d-46cf-a53e-0fc9019f0f20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "620069c3-00b1-4883-956a-2419cfd027e3", "AQAAAAIAAYagAAAAEESbjKcLfjs1l04lUpNjHAGZsW6UryIFPpWboyW+5xvc3dcHtfp6On3+9zE5eLm36g==", "03de34cc-b48c-4550-a451-3e5356a3ce66" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11df1678-e026-4ad8-993e-ae09f3929b23", "AQAAAAIAAYagAAAAEO2T0PlxnwJc5sGHqyahSRlR+E06W84dNR9SMMDgDysuMK6d8MRoOhGKAW3tp2wYUg==", "b741c082-e69c-4930-8cc6-87df84d1724b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29c1584f-9f11-4c6b-b6c3-5ffc2ea8bc2b", "AQAAAAIAAYagAAAAEO7M8yS/6bLcctRf+0p3vbvB+itNF+fmItz6CweYWsHSEqnykEOuSphzFrRvaUj4PA==", "1cc6ab5c-5e3e-4dce-92d4-c22b6c2a018b" });

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CapacidadeFria",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CapacidadeFria",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 3,
                column: "CapacidadeFria",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 4,
                column: "CapacidadeFria",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 6,
                column: "CapacidadeFria",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 7,
                column: "CapacidadeFria",
                value: 35);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 8,
                column: "CapacidadeFria",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 9,
                column: "CapacidadeFria",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 10,
                column: "CapacidadeFria",
                value: 45);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 11,
                column: "CapacidadeFria",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 12,
                column: "CapacidadeFria",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 13,
                column: "CapacidadeFria",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 14,
                column: "CapacidadeFria",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 15,
                column: "CapacidadeFria",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 16,
                column: "CapacidadeFria",
                value: 80);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 17,
                column: "CapacidadeFria",
                value: 35);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 18,
                column: "CapacidadeFria",
                value: 70);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 19,
                column: "CapacidadeFria",
                value: 45);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 20,
                column: "CapacidadeFria",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 21,
                column: "CapacidadeFria",
                value: 40);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 22,
                column: "CapacidadeFria",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 23,
                column: "CapacidadeFria",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Vinhos",
                keyColumn: "Id",
                keyValue: 24,
                column: "CapacidadeFria",
                value: 80);
        }
    }
}

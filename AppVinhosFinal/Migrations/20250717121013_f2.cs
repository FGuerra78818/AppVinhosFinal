using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppVinhosFinal.Migrations
{
    /// <inheritdoc />
    public partial class f2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacidadeFria",
                table: "Vinhos",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                keyValue: 5,
                column: "CapacidadeFria",
                value: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeFria",
                table: "Vinhos");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e888211-7371-43a1-be31-d40dc430fd97", "AQAAAAIAAYagAAAAELLCoXq/PfEOhGCBMAekWan3EhKiP1o8jTfxxToumGLDSkJ+FvRghRxKp5zfo2Y/iw==", "ea78c9ab-8fca-4fe6-a9d0-4d199f59b442" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b2bb763-aa52-4b5f-8f05-5f31f07db971", "AQAAAAIAAYagAAAAEPYYnHHbYEn4hswjiV35PkDWtci6+dhQeYE6fw23YvPvXt726idAqU2JapOY1uL/gQ==", "e95fd4d5-043d-4ada-a11d-5d01751b0139" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ded0d9bd-a0bb-4798-9929-a69814976e2a", "AQAAAAIAAYagAAAAEKnMuNMNFR4PaxMnfbcZyTOxscJDV9ryitz+ALU0z76evWgXkRLR/ucTB59+h4UwAw==", "b7b7cf21-9aec-4e6a-9f19-32fd8a29f127" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78ea84b1-e027-49c9-814d-0cc00b7bcfdd", "AQAAAAIAAYagAAAAEM/v/q/E2qJq+LwnmMMT5G/eaplrJblxocE2HJf6XoBkB++tdjxKsJwBGZ1stBHoRQ==", "2c962c9c-f923-4f06-84e9-d2b7cf0767f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5839f2d3-d1bc-4a27-a4f0-b0a474020ccc", "AQAAAAIAAYagAAAAEDmH/8K5OeUZbB3N9K2SW/0wx4h0l1IzHinuyth5f2J+xVKinGANEx/DjQpOQnfIXg==", "4128bfcb-eba0-4fa1-b384-17514da192b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8300612-bc1c-465f-ac0e-ad8e7daa161b", "AQAAAAIAAYagAAAAEEpVr4W9ISkzbQvqxhsjYeUoeO5VzYQFaeR30jqi/qY/wkzQDmpQ4N2GfdnAmUXPaQ==", "c848c8d4-98a5-4352-beda-5d08c366c956" });
        }
    }
}

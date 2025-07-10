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
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAprovacao",
                table: "Pedidos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataAprovacao",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataAprovacao",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataAprovacao",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 4,
                column: "DataAprovacao",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 5,
                column: "DataAprovacao",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 6,
                column: "DataAprovacao",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 7,
                column: "DataAprovacao",
                value: null);

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 8,
                column: "DataAprovacao",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAprovacao",
                table: "Pedidos");
        }
    }
}

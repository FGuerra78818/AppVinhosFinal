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
                name: "Tipo",
                table: "PedidoVinhos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 7,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 8,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 9,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 10,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 11,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 12,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 13,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 14,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 15,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 16,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 17,
                column: "Tipo",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PedidoVinhos",
                keyColumn: "Id",
                keyValue: 18,
                column: "Tipo",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "PedidoVinhos");
        }
    }
}

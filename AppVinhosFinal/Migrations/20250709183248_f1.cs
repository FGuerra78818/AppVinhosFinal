using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppVinhosFinal.Migrations
{
    /// <inheritdoc />
    public partial class f1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogsCopos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantidadeVendida = table.Column<int>(type: "int", nullable: false),
                    DataHoraVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comprador = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsCopos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quintas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumeroMaxVinhoFrio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quintas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    QuintaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccounts_Quintas_QuintaId",
                        column: x => x.QuintaId,
                        principalTable: "Quintas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    QuantidadeFria = table.Column<int>(type: "int", nullable: false),
                    IdQuinta = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vinhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vinhos_Quintas_IdQuinta",
                        column: x => x.IdQuinta,
                        principalTable: "Quintas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoVinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdVinho = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoVinhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoVinhos_Pedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoVinhos_Vinhos_IdVinho",
                        column: x => x.IdVinho,
                        principalTable: "Vinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LogsCopos",
                columns: new[] { "Id", "Comprador", "DataHoraVenda", "QuantidadeVendida" },
                values: new object[,]
                {
                    { 1, "Cliente X", new DateTime(2025, 7, 11, 10, 15, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 2, null, new DateTime(2025, 7, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 3, "Cliente Y", new DateTime(2025, 7, 10, 18, 30, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 4, "Cliente Z", new DateTime(2025, 7, 9, 20, 45, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, "Cliente W", new DateTime(2025, 7, 9, 19, 20, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 6, "Sem Venda", new DateTime(2025, 7, 8, 17, 10, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 7, null, new DateTime(2025, 7, 7, 16, 5, 0, 0, DateTimeKind.Unspecified), 50 },
                    { 8, "Cliente K", new DateTime(2025, 7, 6, 21, 30, 0, 0, DateTimeKind.Unspecified), 7 }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "DataPedido", "Estado" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 11, 10, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { 2, new DateTime(2025, 7, 10, 14, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, new DateTime(2025, 7, 9, 16, 45, 0, 0, DateTimeKind.Utc), 2 },
                    { 4, new DateTime(2025, 7, 9, 9, 15, 0, 0, DateTimeKind.Utc), 1 },
                    { 5, new DateTime(2025, 7, 8, 11, 5, 0, 0, DateTimeKind.Utc), 0 },
                    { 6, new DateTime(2025, 7, 8, 18, 20, 0, 0, DateTimeKind.Utc), 1 },
                    { 7, new DateTime(2025, 7, 7, 19, 30, 0, 0, DateTimeKind.Utc), 2 },
                    { 8, new DateTime(2025, 7, 7, 13, 40, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.InsertData(
                table: "Quintas",
                columns: new[] { "Id", "Nome", "NumeroMaxVinhoFrio" },
                values: new object[,]
                {
                    { 1, "Quinta Modelo", 20 },
                    { 2, "Quinta Vazia", 0 },
                    { 3, "Quinta dos Sonhos", 50 },
                    { 4, "Vinhedo Real", 30 },
                    { 5, "Encostas do Douro", 40 },
                    { 6, "Vale do Champagne", 60 }
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "CreatedAt", "Email", "MustChangePassword", "Password", "QuintaId", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 11, 7, 0, 0, 0, DateTimeKind.Unspecified), "admin@vinhos.pt", false, "Admin123!", 1, "Admin", "admin" },
                    { 2, new DateTime(2025, 7, 11, 8, 0, 0, 0, DateTimeKind.Unspecified), "staff01@vinhos.pt", false, "Staff123!", 3, "Staff", "staff01" },
                    { 3, new DateTime(2025, 7, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), "visit1@vinhos.pt", true, "Guest123!", 2, "User", "visitante1" },
                    { 4, new DateTime(2025, 7, 10, 14, 30, 0, 0, DateTimeKind.Unspecified), "user01@vinhos.pt", false, "User01!", 1, "User", "user01" },
                    { 5, new DateTime(2025, 7, 10, 16, 45, 0, 0, DateTimeKind.Unspecified), "convid@vinhos.pt", false, "Conv123!", 5, "User", "convidado" },
                    { 6, new DateTime(2025, 7, 9, 12, 20, 0, 0, DateTimeKind.Unspecified), "guest2@vinhos.pt", true, "G2pass!", 6, "User", "guest2" }
                });

            migrationBuilder.InsertData(
                table: "Vinhos",
                columns: new[] { "Id", "Estado", "IdQuinta", "Nome", "Quantidade", "QuantidadeFria" },
                values: new object[,]
                {
                    { 1, 0, 1, "Tinto Teste", 50, 10 },
                    { 2, 1, 1, "Branco Oculto", 30, 0 },
                    { 3, 0, 3, "Rosé Primavera", 100, 60 },
                    { 4, 0, 4, "Espumante Real", 80, 80 },
                    { 5, 0, 5, "Loureiro Verde", 120, 70 },
                    { 6, 0, 5, "Alvarinho Clássico", 60, 30 },
                    { 7, 0, 2, "Tinto Intenso", 40, 20 },
                    { 8, 0, 6, "Branco Cítrico", 25, 25 },
                    { 9, 1, 6, "Vinho de Mesa", 200, 0 },
                    { 10, 0, 3, "Tinto Reserva", 75, 15 },
                    { 11, 0, 4, "Branco Seco", 90, 45 },
                    { 12, 0, 1, "Vinha Velha", 30, 5 }
                });

            migrationBuilder.InsertData(
                table: "PedidoVinhos",
                columns: new[] { "Id", "IdPedido", "IdVinho", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 1, 5 },
                    { 2, 1, 3, 2 },
                    { 3, 1, 5, 1 },
                    { 4, 1, 12, 1 },
                    { 5, 2, 2, 10 },
                    { 6, 2, 4, 4 },
                    { 7, 3, 11, 20 },
                    { 8, 4, 1, 3 },
                    { 9, 4, 6, 6 },
                    { 10, 4, 9, 2 },
                    { 11, 5, 7, 8 },
                    { 12, 5, 8, 8 },
                    { 13, 6, 10, 5 },
                    { 14, 6, 3, 10 },
                    { 15, 6, 12, 2 },
                    { 16, 6, 5, 3 },
                    { 17, 8, 1, 3 },
                    { 18, 8, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoVinhos_IdPedido",
                table: "PedidoVinhos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoVinhos_IdVinho",
                table: "PedidoVinhos",
                column: "IdVinho");

            migrationBuilder.CreateIndex(
                name: "IX_Quintas_Nome",
                table: "Quintas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_QuintaId",
                table: "UserAccounts",
                column: "QuintaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserName",
                table: "UserAccounts",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vinhos_IdQuinta",
                table: "Vinhos",
                column: "IdQuinta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsCopos");

            migrationBuilder.DropTable(
                name: "PedidoVinhos");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Vinhos");

            migrationBuilder.DropTable(
                name: "Quintas");
        }
    }
}

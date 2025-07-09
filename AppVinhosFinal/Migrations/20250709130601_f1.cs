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
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdVinho = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoVinhos", x => x.IdPedido);
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

            migrationBuilder.CreateTable(
                name: "VinhosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVinho = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinhosStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VinhosStock_Vinhos_IdVinho",
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
                    { 1, "Cliente X", new DateTime(2025, 7, 9, 10, 15, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 2, null, new DateTime(2025, 7, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 3, "Cliente Y", new DateTime(2025, 7, 8, 18, 30, 0, 0, DateTimeKind.Unspecified), 25 },
                    { 4, "Cliente Z", new DateTime(2025, 7, 7, 20, 45, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "DataPedido", "Estado" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 9, 8, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { 2, new DateTime(2025, 7, 8, 12, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, new DateTime(2025, 7, 7, 15, 45, 0, 0, DateTimeKind.Utc), 2 },
                    { 4, new DateTime(2025, 7, 6, 10, 15, 0, 0, DateTimeKind.Utc), 1 }
                });

            migrationBuilder.InsertData(
                table: "Quintas",
                columns: new[] { "Id", "Nome", "NumeroMaxVinhoFrio" },
                values: new object[,]
                {
                    { 1, "Quinta Modelo", 20 },
                    { 2, "Quinta Vazia", 0 },
                    { 3, "Quinta dos Sonhos", 50 },
                    { 4, "Vinhedo Real", 30 }
                });

            migrationBuilder.InsertData(
                table: "UserAccounts",
                columns: new[] { "Id", "CreatedAt", "Email", "MustChangePassword", "Password", "QuintaId", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 9, 7, 0, 0, 0, DateTimeKind.Unspecified), "admin@vinhos.pt", false, "Admin123!", 1, "Admin", "admin" },
                    { 2, new DateTime(2025, 7, 9, 8, 0, 0, 0, DateTimeKind.Unspecified), "staff01@vinhos.pt", false, "Staff123!", 3, "Staff", "staff01" },
                    { 3, new DateTime(2025, 7, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), "guest@vinhos.pt", true, "Guest123!", 2, "User", "visitante" },
                    { 4, new DateTime(2025, 7, 8, 14, 30, 0, 0, DateTimeKind.Unspecified), "user01@vinhos.pt", false, "User01!", 1, "User", "user01" }
                });

            migrationBuilder.InsertData(
                table: "Vinhos",
                columns: new[] { "Id", "Estado", "IdQuinta", "Nome", "Quantidade", "QuantidadeFria" },
                values: new object[,]
                {
                    { 1, 0, 1, "Tinto Teste", 50, 10 },
                    { 2, 1, 1, "Branco Oculto", 30, 0 },
                    { 3, 0, 3, "Rosé Primavera", 100, 60 },
                    { 4, 0, 4, "Espumante Real", 80, 80 }
                });

            migrationBuilder.InsertData(
                table: "PedidoVinhos",
                columns: new[] { "IdPedido", "IdVinho", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 5 },
                    { 2, 1, 10 },
                    { 3, 2, 3 },
                    { 4, 3, 20 }
                });

            migrationBuilder.InsertData(
                table: "VinhosStock",
                columns: new[] { "Id", "Estado", "IdVinho", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 1, 10 },
                    { 2, 0, 1, 40 },
                    { 3, 1, 2, 0 },
                    { 4, 0, 2, 30 },
                    { 5, 1, 3, 60 },
                    { 6, 0, 3, 40 },
                    { 7, 1, 4, 80 },
                    { 8, 0, 4, 0 }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_VinhosStock_IdVinho",
                table: "VinhosStock",
                column: "IdVinho");
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
                name: "VinhosStock");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Vinhos");

            migrationBuilder.DropTable(
                name: "Quintas");
        }
    }
}

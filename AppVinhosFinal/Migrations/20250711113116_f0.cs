using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppVinhosFinal.Migrations
{
    /// <inheritdoc />
    public partial class f0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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
                    DataAprovacao = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    QuintaId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Quintas_QuintaId",
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "MustChangePassword", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "QuintaId", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "9e888211-7371-43a1-be31-d40dc430fd97", new DateTime(2025, 7, 11, 7, 0, 0, 0, DateTimeKind.Utc), "admin@vinhos.pt", true, false, null, false, "ADMIN@VINHOS.PT", "ADMIN", "AQAAAAIAAYagAAAAELLCoXq/PfEOhGCBMAekWan3EhKiP1o8jTfxxToumGLDSkJ+FvRghRxKp5zfo2Y/iw==", null, false, null, "Admin", "ea78c9ab-8fca-4fe6-a9d0-4d199f59b442", false, "admin" },
                    { 2, 0, "9b2bb763-aa52-4b5f-8f05-5f31f07db971", new DateTime(2025, 7, 11, 8, 0, 0, 0, DateTimeKind.Utc), "staff01@vinhos.pt", true, false, null, false, "STAFF01@VINHOS.PT", "STAFF01", "AQAAAAIAAYagAAAAEPYYnHHbYEn4hswjiV35PkDWtci6+dhQeYE6fw23YvPvXt726idAqU2JapOY1uL/gQ==", null, false, null, "Staff", "e95fd4d5-043d-4ada-a11d-5d01751b0139", false, "staff01" }
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
                columns: new[] { "Id", "DataAprovacao", "DataPedido", "Estado" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 7, 11, 8, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { 2, new DateTime(2025, 7, 11, 9, 35, 0, 0, DateTimeKind.Utc), new DateTime(2025, 7, 11, 9, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, null, new DateTime(2025, 7, 10, 14, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 4, new DateTime(2025, 7, 11, 16, 10, 0, 0, DateTimeKind.Utc), new DateTime(2025, 7, 10, 16, 5, 0, 0, DateTimeKind.Utc), 1 },
                    { 5, null, new DateTime(2025, 7, 10, 18, 20, 0, 0, DateTimeKind.Utc), 0 },
                    { 6, new DateTime(2025, 7, 10, 10, 17, 0, 0, DateTimeKind.Utc), new DateTime(2025, 7, 9, 10, 15, 0, 0, DateTimeKind.Utc), 1 },
                    { 7, null, new DateTime(2025, 7, 9, 12, 45, 0, 0, DateTimeKind.Utc), 0 },
                    { 8, null, new DateTime(2025, 7, 8, 14, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 9, new DateTime(2025, 7, 9, 16, 35, 0, 0, DateTimeKind.Utc), new DateTime(2025, 7, 8, 16, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { 10, null, new DateTime(2025, 7, 7, 11, 15, 0, 0, DateTimeKind.Utc), 0 },
                    { 11, null, new DateTime(2025, 7, 7, 13, 25, 0, 0, DateTimeKind.Utc), 2 },
                    { 12, new DateTime(2025, 7, 7, 15, 50, 0, 0, DateTimeKind.Utc), new DateTime(2025, 7, 6, 15, 45, 0, 0, DateTimeKind.Utc), 1 }
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "MustChangePassword", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "QuintaId", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 3, 0, "ded0d9bd-a0bb-4798-9929-a69814976e2a", new DateTime(2025, 7, 11, 9, 0, 0, 0, DateTimeKind.Utc), "visit1@vinhos.pt", true, false, null, true, "VISIT1@VINHOS.PT", "VISITANTE1", "AQAAAAIAAYagAAAAEKnMuNMNFR4PaxMnfbcZyTOxscJDV9ryitz+ALU0z76evWgXkRLR/ucTB59+h4UwAw==", null, false, 2, "User", "b7b7cf21-9aec-4e6a-9f19-32fd8a29f127", false, "visitante1" },
                    { 4, 0, "78ea84b1-e027-49c9-814d-0cc00b7bcfdd", new DateTime(2025, 7, 10, 14, 30, 0, 0, DateTimeKind.Utc), "user01@vinhos.pt", true, false, null, false, "USER01@VINHOS.PT", "USER01", "AQAAAAIAAYagAAAAEM/v/q/E2qJq+LwnmMMT5G/eaplrJblxocE2HJf6XoBkB++tdjxKsJwBGZ1stBHoRQ==", null, false, 1, "User", "2c962c9c-f923-4f06-84e9-d2b7cf0767f0", false, "user01" },
                    { 5, 0, "5839f2d3-d1bc-4a27-a4f0-b0a474020ccc", new DateTime(2025, 7, 10, 16, 45, 0, 0, DateTimeKind.Utc), "convid@vinhos.pt", true, false, null, false, "CONVID@VINHOS.PT", "CONVIDADO", "AQAAAAIAAYagAAAAEDmH/8K5OeUZbB3N9K2SW/0wx4h0l1IzHinuyth5f2J+xVKinGANEx/DjQpOQnfIXg==", null, false, 5, "User", "4128bfcb-eba0-4fa1-b384-17514da192b7", false, "convidado" },
                    { 6, 0, "d8300612-bc1c-465f-ac0e-ad8e7daa161b", new DateTime(2025, 7, 9, 12, 20, 0, 0, DateTimeKind.Utc), "guest2@vinhos.pt", true, false, null, true, "GUEST2@VINHOS.PT", "GUEST2", "AQAAAAIAAYagAAAAEEpVr4W9ISkzbQvqxhsjYeUoeO5VzYQFaeR30jqi/qY/wkzQDmpQ4N2GfdnAmUXPaQ==", null, false, 6, "User", "c848c8d4-98a5-4352-beda-5d08c366c956", false, "guest2" }
                });

            migrationBuilder.InsertData(
                table: "Vinhos",
                columns: new[] { "Id", "Estado", "IdQuinta", "Nome", "Quantidade", "QuantidadeFria" },
                values: new object[,]
                {
                    { 1, 0, 1, "Tinto Clássico", 50, 10 },
                    { 2, 0, 1, "Branco Seco", 40, 5 },
                    { 3, 0, 1, "Rosé Alegre", 60, 20 },
                    { 4, 0, 1, "Espumante Delicado", 30, 30 },
                    { 5, 0, 2, "Tinto Intenso II", 45, 0 },
                    { 6, 0, 2, "Branco Floral", 25, 25 },
                    { 7, 0, 2, "Rosé de Verão", 70, 35 },
                    { 8, 1, 2, "Espumante Brut", 80, 50 },
                    { 9, 0, 3, "Tinto Reserva", 75, 15 },
                    { 10, 0, 3, "Branco Suave", 90, 45 },
                    { 11, 0, 3, "Rosé Primavera", 100, 60 },
                    { 12, 0, 3, "Espumante Rosé", 50, 50 },
                    { 13, 0, 4, "Tinto Envelhecido", 40, 20 },
                    { 14, 0, 4, "Branco Cítrico", 60, 30 },
                    { 15, 1, 4, "Rosé Aromático", 55, 25 },
                    { 16, 0, 4, "Espumante Premium", 80, 80 },
                    { 17, 0, 5, "Tinto Ensolarado", 65, 35 },
                    { 18, 0, 5, "Branco Seco Especial", 110, 70 },
                    { 19, 0, 5, "Rosé Tropical", 85, 45 },
                    { 20, 0, 5, "Espumante de Honra", 90, 60 },
                    { 21, 0, 6, "Tinto Forte", 80, 40 },
                    { 22, 0, 6, "Branco Frutado", 95, 50 },
                    { 23, 0, 6, "Rosé Encantado", 120, 60 },
                    { 24, 0, 6, "Espumante Real", 100, 80 }
                });

            migrationBuilder.InsertData(
                table: "PedidoVinhos",
                columns: new[] { "Id", "IdPedido", "IdVinho", "Quantidade", "Tipo" },
                values: new object[,]
                {
                    { 1, 1, 1, 5, 0 },
                    { 2, 2, 2, 3, 0 },
                    { 3, 2, 4, 2, 0 },
                    { 4, 3, 9, 10, 0 },
                    { 5, 4, 10, 15, 0 },
                    { 6, 4, 12, 5, 0 },
                    { 7, 6, 13, 2, 0 },
                    { 8, 6, 13, 3, 0 },
                    { 9, 7, 17, 5, 0 },
                    { 10, 7, 19, 4, 0 },
                    { 11, 8, 21, 6, 0 },
                    { 12, 9, 22, 8, 0 },
                    { 13, 9, 23, 2, 0 },
                    { 14, 9, 24, 1, 0 },
                    { 15, 10, 5, 7, 0 },
                    { 16, 10, 6, 3, 0 },
                    { 17, 10, 7, 1, 0 },
                    { 18, 11, 12, 0, 0 },
                    { 19, 12, 3, 10, 0 },
                    { 20, 12, 1, 2, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_QuintaId",
                table: "AspNetUsers",
                column: "QuintaId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_Vinhos_IdQuinta",
                table: "Vinhos",
                column: "IdQuinta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LogsCopos");

            migrationBuilder.DropTable(
                name: "PedidoVinhos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Vinhos");

            migrationBuilder.DropTable(
                name: "Quintas");
        }
    }
}

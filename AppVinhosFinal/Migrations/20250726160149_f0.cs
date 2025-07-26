using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsCopos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuantidadeVendida = table.Column<int>(type: "integer", nullable: false),
                    DataHoraVenda = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Comprador = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsCopos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuintaId = table.Column<int>(type: "integer", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataPedido = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAprovacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quintas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NumeroMaxVinhoFrio = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quintas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "boolean", nullable: false),
                    QuintaId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeFria = table.Column<int>(type: "integer", nullable: false),
                    CapacidadeFria = table.Column<int>(type: "integer", nullable: false),
                    IdQuinta = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPedido = table.Column<int>(type: "integer", nullable: false),
                    IdVinho = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
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
                    { 1, 0, "291c2f92-03cc-4503-b5d1-8b218398e7fb", new DateTime(2025, 7, 11, 7, 0, 0, 0, DateTimeKind.Utc), "admin@vinhos.pt", true, false, null, false, "ADMIN@VINHOS.PT", "ADMIN", "AQAAAAIAAYagAAAAEO8iofkkhWRBNQcPkXccBXQ65huiErAJJcgVebPwaZZXGjDR5j4mtmfnGB0lgOOjQg==", null, false, null, "Admin", "b8c88586-0b9e-417d-a197-5639d5565133", false, "admin" },
                    { 2, 0, "29171b24-b5c3-4ad8-ae64-3734b05061bc", new DateTime(2025, 7, 11, 8, 0, 0, 0, DateTimeKind.Utc), "staff01@vinhos.pt", true, false, null, false, "STAFF01@VINHOS.PT", "STAFF01", "AQAAAAIAAYagAAAAEIOHA5A0gYXxuOqORW1iT6maou2mVvZOJHWbMo/SBc4YFl7I6pBuu8CruYX/y+Iw/g==", null, false, null, "Staff", "31e0de36-b637-4349-862f-a4728b6a7e8b", false, "staff01" }
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
                    { 3, 0, "9b500abf-b4c8-45ea-a36f-ba3fc4e9ad07", new DateTime(2025, 7, 11, 9, 0, 0, 0, DateTimeKind.Utc), "visit1@vinhos.pt", true, false, null, true, "VISIT1@VINHOS.PT", "VISITANTE1", "AQAAAAIAAYagAAAAEFhp7WjlPuxISOnedWYfiIFMfrESfSbBmxJhd3PUrvz+9E2B/0Isle6cdb9f7ZM8Ow==", null, false, 2, "User", "7a503764-15e5-43c4-8b9f-9413d99fb7c9", false, "visitante1" },
                    { 4, 0, "00efb5f8-206f-4562-b25f-89c8009495ac", new DateTime(2025, 7, 10, 14, 30, 0, 0, DateTimeKind.Utc), "user01@vinhos.pt", true, false, null, false, "USER01@VINHOS.PT", "USER01", "AQAAAAIAAYagAAAAEFhwfFR2V9+2lldE4Wm1YoTwgGMR7T5EOgU6dTa8JkXu8eUZVIo87LsoMgpQTTcIXg==", null, false, 1, "User", "98312e59-9e9f-4208-b537-5e6f6d060dd6", false, "user01" },
                    { 5, 0, "32dae27b-e17c-458f-8c23-e7c84e833566", new DateTime(2025, 7, 10, 16, 45, 0, 0, DateTimeKind.Utc), "convid@vinhos.pt", true, false, null, false, "CONVID@VINHOS.PT", "CONVIDADO", "AQAAAAIAAYagAAAAEOgSsyWLFtq1/ZaiRX9Uds5To6jMaZk80jZ7zzFa+jmOJ9ZQvtoAUIycoFD5Yv/Rhg==", null, false, 5, "User", "dc3c0e0e-c0ed-4765-88a1-68fdf6089c3b", false, "convidado" },
                    { 6, 0, "dc97975f-4437-4363-b53d-9877d58d9a8d", new DateTime(2025, 7, 9, 12, 20, 0, 0, DateTimeKind.Utc), "guest2@vinhos.pt", true, false, null, true, "GUEST2@VINHOS.PT", "GUEST2", "AQAAAAIAAYagAAAAEHusYjgay17N1b4Y4qeZILOnBhZ04W48epLU9xso4G7LlQsd3awvpAIQfve8P3z96g==", null, false, 6, "User", "6057d2dd-ab9a-44a4-9819-a84bfbfc7808", false, "guest2" }
                });

            migrationBuilder.InsertData(
                table: "Vinhos",
                columns: new[] { "Id", "CapacidadeFria", "Estado", "IdQuinta", "Nome", "Quantidade", "QuantidadeFria" },
                values: new object[,]
                {
                    { 1, 0, 0, 1, "Tinto Clássico", 50, 10 },
                    { 2, 0, 0, 1, "Branco Seco", 40, 5 },
                    { 3, 0, 0, 1, "Rosé Alegre", 60, 20 },
                    { 4, 0, 0, 1, "Espumante Delicado", 30, 30 },
                    { 5, 0, 0, 2, "Tinto Intenso II", 45, 0 },
                    { 6, 0, 0, 2, "Branco Floral", 25, 25 },
                    { 7, 0, 0, 2, "Rosé de Verão", 70, 35 },
                    { 8, 0, 1, 2, "Espumante Brut", 80, 50 },
                    { 9, 0, 0, 3, "Tinto Reserva", 75, 15 },
                    { 10, 0, 0, 3, "Branco Suave", 90, 45 },
                    { 11, 0, 0, 3, "Rosé Primavera", 100, 60 },
                    { 12, 0, 0, 3, "Espumante Rosé", 50, 50 },
                    { 13, 0, 0, 4, "Tinto Envelhecido", 40, 20 },
                    { 14, 0, 0, 4, "Branco Cítrico", 60, 30 },
                    { 15, 0, 1, 4, "Rosé Aromático", 55, 25 },
                    { 16, 0, 0, 4, "Espumante Premium", 80, 80 },
                    { 17, 0, 0, 5, "Tinto Ensolarado", 65, 35 },
                    { 18, 0, 0, 5, "Branco Seco Especial", 110, 70 },
                    { 19, 0, 0, 5, "Rosé Tropical", 85, 45 },
                    { 20, 0, 0, 5, "Espumante de Honra", 90, 60 },
                    { 21, 0, 0, 6, "Tinto Forte", 80, 40 },
                    { 22, 0, 0, 6, "Branco Frutado", 95, 50 },
                    { 23, 0, 0, 6, "Rosé Encantado", 120, 60 },
                    { 24, 0, 0, 6, "Espumante Real", 100, 80 }
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
                    { 7, 5, 13, 2, 0 },
                    { 8, 5, 13, 3, 0 },
                    { 9, 6, 17, 5, 0 },
                    { 10, 6, 19, 4, 0 },
                    { 11, 7, 21, 6, 0 },
                    { 12, 8, 22, 8, 0 },
                    { 13, 8, 23, 2, 0 },
                    { 14, 8, 24, 1, 0 },
                    { 15, 9, 5, 7, 0 },
                    { 16, 9, 6, 3, 0 },
                    { 17, 9, 7, 1, 0 },
                    { 18, 10, 12, 2, 0 },
                    { 19, 11, 3, 10, 0 },
                    { 20, 11, 1, 2, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

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
                name: "Notifications");

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

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppVinhosFinal.Models;
using AppVinhosFinal.Entities;
using Microsoft.AspNetCore.Identity;

namespace AppVinhosFinal.Data.Seeders
{
    public class QuintasSeeder : IEntityTypeConfiguration<Quintas>
    {
        public void Configure(EntityTypeBuilder<Quintas> builder)
        {
            builder.HasData(
                new Quintas { Id = 1, Nome = "Quinta Modelo", NumeroMaxVinhoFrio = 20 },
                new Quintas { Id = 2, Nome = "Quinta Vazia", NumeroMaxVinhoFrio = 0 },
                new Quintas { Id = 3, Nome = "Quinta dos Sonhos", NumeroMaxVinhoFrio = 50 },
                new Quintas { Id = 4, Nome = "Vinhedo Real", NumeroMaxVinhoFrio = 30 },
                new Quintas { Id = 5, Nome = "Encostas do Douro", NumeroMaxVinhoFrio = 40 },
                new Quintas { Id = 6, Nome = "Vale do Champagne", NumeroMaxVinhoFrio = 60 }
            );
        }
    }

    public class VinhosSeeder : IEntityTypeConfiguration<Vinhos>
    {
        public void Configure(EntityTypeBuilder<Vinhos> builder)
        {
            builder.HasData(
                // Quinta 1 (Id = 1)
                new Vinhos { Id = 1, Nome = "Tinto Clássico", Quantidade = 50, QuantidadeFria = 10, IdQuinta = 1, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 2, Nome = "Branco Seco", Quantidade = 40, QuantidadeFria = 5, IdQuinta = 1, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 3, Nome = "Rosé Alegre", Quantidade = 60, QuantidadeFria = 20, IdQuinta = 1, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 4, Nome = "Espumante Delicado", Quantidade = 30, QuantidadeFria = 30, IdQuinta = 1, Estado = EstadoVinho.Visible },

                // Quinta 2 (Id = 2)
                new Vinhos { Id = 5, Nome = "Tinto Intenso II", Quantidade = 45, QuantidadeFria = 0, IdQuinta = 2, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 6, Nome = "Branco Floral", Quantidade = 25, QuantidadeFria = 25, IdQuinta = 2, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 7, Nome = "Rosé de Verão", Quantidade = 70, QuantidadeFria = 35, IdQuinta = 2, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 8, Nome = "Espumante Brut", Quantidade = 80, QuantidadeFria = 50, IdQuinta = 2, Estado = EstadoVinho.Hidden },

                // Quinta 3 (Id = 3)
                new Vinhos { Id = 9, Nome = "Tinto Reserva", Quantidade = 75, QuantidadeFria = 15, IdQuinta = 3, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 10, Nome = "Branco Suave", Quantidade = 90, QuantidadeFria = 45, IdQuinta = 3, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 11, Nome = "Rosé Primavera", Quantidade = 100, QuantidadeFria = 60, IdQuinta = 3, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 12, Nome = "Espumante Rosé", Quantidade = 50, QuantidadeFria = 50, IdQuinta = 3, Estado = EstadoVinho.Visible },

                // Quinta 4 (Id = 4)
                new Vinhos { Id = 13, Nome = "Tinto Envelhecido", Quantidade = 40, QuantidadeFria = 20, IdQuinta = 4, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 14, Nome = "Branco Cítrico", Quantidade = 60, QuantidadeFria = 30, IdQuinta = 4, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 15, Nome = "Rosé Aromático", Quantidade = 55, QuantidadeFria = 25, IdQuinta = 4, Estado = EstadoVinho.Hidden },
                new Vinhos { Id = 16, Nome = "Espumante Premium", Quantidade = 80, QuantidadeFria = 80, IdQuinta = 4, Estado = EstadoVinho.Visible },

                // Quinta 5 (Id = 5)
                new Vinhos { Id = 17, Nome = "Tinto Ensolarado", Quantidade = 65, QuantidadeFria = 35, IdQuinta = 5, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 18, Nome = "Branco Seco Especial", Quantidade = 110, QuantidadeFria = 70, IdQuinta = 5, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 19, Nome = "Rosé Tropical", Quantidade = 85, QuantidadeFria = 45, IdQuinta = 5, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 20, Nome = "Espumante de Honra", Quantidade = 90, QuantidadeFria = 60, IdQuinta = 5, Estado = EstadoVinho.Visible },

                // Quinta 6 (Id = 6)
                new Vinhos { Id = 21, Nome = "Tinto Forte", Quantidade = 80, QuantidadeFria = 40, IdQuinta = 6, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 22, Nome = "Branco Frutado", Quantidade = 95, QuantidadeFria = 50, IdQuinta = 6, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 23, Nome = "Rosé Encantado", Quantidade = 120, QuantidadeFria = 60, IdQuinta = 6, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 24, Nome = "Espumante Real", Quantidade = 100, QuantidadeFria = 80, IdQuinta = 6, Estado = EstadoVinho.Visible }
            );
        }
    }

    public class PedidosSeeder : IEntityTypeConfiguration<Pedidos>
    {
        public void Configure(EntityTypeBuilder<Pedidos> builder)
        {
            builder.HasData(
                new Pedidos { Id = 1, DataPedido = new DateTime(2025, 7, 11, 8, 0, 0, DateTimeKind.Utc), Estado = EstadoPedido.PorAprovar },
                new Pedidos { Id = 2, DataPedido = new DateTime(2025, 7, 11, 9, 30, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado, DataAprovacao = new DateTime(2025, 7, 11, 9, 35, 0, DateTimeKind.Utc) },
                new Pedidos { Id = 3, DataPedido = new DateTime(2025, 7, 10, 14, 0, 0, DateTimeKind.Utc), Estado = EstadoPedido.Cancelado },
                new Pedidos { Id = 4, DataPedido = new DateTime(2025, 7, 10, 16, 5, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado, DataAprovacao = new DateTime(2025, 7, 11, 16, 10, 0, DateTimeKind.Utc) },
                new Pedidos { Id = 5, DataPedido = new DateTime(2025, 7, 10, 18, 20, 0, DateTimeKind.Utc), Estado = EstadoPedido.PorAprovar },
                new Pedidos { Id = 6, DataPedido = new DateTime(2025, 7, 9, 10, 15, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado, DataAprovacao = new DateTime(2025, 7, 10, 10, 17, 0, DateTimeKind.Utc) },
                new Pedidos { Id = 7, DataPedido = new DateTime(2025, 7, 9, 12, 45, 0, DateTimeKind.Utc), Estado = EstadoPedido.PorAprovar },
                new Pedidos { Id = 8, DataPedido = new DateTime(2025, 7, 8, 14, 0, 0, DateTimeKind.Utc), Estado = EstadoPedido.Cancelado },
                new Pedidos { Id = 9, DataPedido = new DateTime(2025, 7, 8, 16, 30, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado, DataAprovacao = new DateTime(2025, 7, 9, 16, 35, 0, DateTimeKind.Utc) },
                new Pedidos { Id = 10, DataPedido = new DateTime(2025, 7, 7, 11, 15, 0, DateTimeKind.Utc), Estado = EstadoPedido.PorAprovar },
                new Pedidos { Id = 11, DataPedido = new DateTime(2025, 7, 7, 13, 25, 0, DateTimeKind.Utc), Estado = EstadoPedido.Cancelado },
                new Pedidos { Id = 12, DataPedido = new DateTime(2025, 7, 6, 15, 45, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado, DataAprovacao = new DateTime(2025, 7, 7, 15, 50, 0, DateTimeKind.Utc) }
            );
        }
    }

    public class PedidoVinhoSeeder : IEntityTypeConfiguration<PedidoVinho>
    {
        public void Configure(EntityTypeBuilder<PedidoVinho> builder)
        {
            builder.HasData(
                // Pedido 1 (Quinta 1) – 1 item
                new PedidoVinho { Id = 1, IdPedido = 1, IdVinho = 1, Quantidade = 5 },

                // Pedido 2 (Quinta 1) – 2 itens
                new PedidoVinho { Id = 2, IdPedido = 2, IdVinho = 2, Quantidade = 3 },
                new PedidoVinho { Id = 3, IdPedido = 2, IdVinho = 4, Quantidade = 2 },

                // Pedido 3 (Quinta 3) – 1 item
                new PedidoVinho { Id = 4, IdPedido = 3, IdVinho = 9, Quantidade = 10 },

                // Pedido 4 (Quinta 3) – 2 itens
                new PedidoVinho { Id = 5, IdPedido = 4, IdVinho = 10, Quantidade = 15 },
                new PedidoVinho { Id = 6, IdPedido = 4, IdVinho = 12, Quantidade = 5 },

                // Pedido 5 (vazio)
                // nenhum item

                // Pedido 6 (Quinta 4) – duplicado
                new PedidoVinho { Id = 7, IdPedido = 6, IdVinho = 13, Quantidade = 2 },
                new PedidoVinho { Id = 8, IdPedido = 6, IdVinho = 13, Quantidade = 3 },

                // Pedido 7 (Quinta 5) – 2 itens
                new PedidoVinho { Id = 9, IdPedido = 7, IdVinho = 17, Quantidade = 5 },
                new PedidoVinho { Id = 10, IdPedido = 7, IdVinho = 19, Quantidade = 4 },

                // Pedido 8 (Quinta 6) – 1 item
                new PedidoVinho { Id = 11, IdPedido = 8, IdVinho = 21, Quantidade = 6 },

                // Pedido 9 (Quinta 6) – 3 itens
                new PedidoVinho { Id = 12, IdPedido = 9, IdVinho = 22, Quantidade = 8 },
                new PedidoVinho { Id = 13, IdPedido = 9, IdVinho = 23, Quantidade = 2 },
                new PedidoVinho { Id = 14, IdPedido = 9, IdVinho = 24, Quantidade = 1 },

                // Pedido 10 (Quinta 2) – 3 itens
                new PedidoVinho { Id = 15, IdPedido = 10, IdVinho = 5, Quantidade = 7 },
                new PedidoVinho { Id = 16, IdPedido = 10, IdVinho = 6, Quantidade = 3 },
                new PedidoVinho { Id = 17, IdPedido = 10, IdVinho = 7, Quantidade = 1 },

                // Pedido 11 (Quinta 3) – 1 item com quantidade zero
                new PedidoVinho { Id = 18, IdPedido = 11, IdVinho = 12, Quantidade = 0 },

                // Pedido 12 (Quinta 1) – 2 itens
                new PedidoVinho { Id = 19, IdPedido = 12, IdVinho = 3, Quantidade = 10 },
                new PedidoVinho { Id = 20, IdPedido = 12, IdVinho = 1, Quantidade = 2 }
            );
        }
    }

    public class LogsCoposSeeder : IEntityTypeConfiguration<LogsCopos>
    {
        public void Configure(EntityTypeBuilder<LogsCopos> builder)
        {
            builder.HasData(
                new LogsCopos { Id = 1, QuantidadeVendida = 12, DataHoraVenda = new DateTime(2025, 7, 11, 10, 15, 0), Comprador = "Cliente X" },
                new LogsCopos { Id = 2, QuantidadeVendida = 8, DataHoraVenda = new DateTime(2025, 7, 11, 11, 0, 0), Comprador = null },
                new LogsCopos { Id = 3, QuantidadeVendida = 25, DataHoraVenda = new DateTime(2025, 7, 10, 18, 30, 0), Comprador = "Cliente Y" },
                new LogsCopos { Id = 4, QuantidadeVendida = 5, DataHoraVenda = new DateTime(2025, 7, 9, 20, 45, 0), Comprador = "Cliente Z" },
                new LogsCopos { Id = 5, QuantidadeVendida = 18, DataHoraVenda = new DateTime(2025, 7, 9, 19, 20, 0), Comprador = "Cliente W" },
                new LogsCopos { Id = 6, QuantidadeVendida = 0, DataHoraVenda = new DateTime(2025, 7, 8, 17, 10, 0), Comprador = "Sem Venda" },
                new LogsCopos { Id = 7, QuantidadeVendida = 50, DataHoraVenda = new DateTime(2025, 7, 7, 16, 5, 0), Comprador = null },
                new LogsCopos { Id = 8, QuantidadeVendida = 7, DataHoraVenda = new DateTime(2025, 7, 6, 21, 30, 0), Comprador = "Cliente K" }
            );
        }
    }

    public class UserAccountSeeder : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            var hasher = new PasswordHasher<UserAccount>();

            // 1) Cria cada utilizador e calcula o hash da password
            var admin = new UserAccount
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@vinhos.pt",
                NormalizedEmail = "ADMIN@VINHOS.PT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "Admin",
                MustChangePassword = false,
                QuintaId = null,
                CreatedAt = new DateTime(2025, 7, 11, 7, 0, 0, DateTimeKind.Utc)
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            var staff = new UserAccount
            {
                Id = 2,
                UserName = "staff01",
                NormalizedUserName = "STAFF01",
                Email = "staff01@vinhos.pt",
                NormalizedEmail = "STAFF01@VINHOS.PT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "Staff",
                MustChangePassword = false,
                QuintaId = null,
                CreatedAt = new DateTime(2025, 7, 11, 8, 0, 0, DateTimeKind.Utc)
            };
            staff.PasswordHash = hasher.HashPassword(staff, "Staff123!");

            var visitante = new UserAccount
            {
                Id = 3,
                UserName = "visitante1",
                NormalizedUserName = "VISITANTE1",
                Email = "visit1@vinhos.pt",
                NormalizedEmail = "VISIT1@VINHOS.PT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "User",
                MustChangePassword = true,
                QuintaId = 2,
                CreatedAt = new DateTime(2025, 7, 11, 9, 0, 0, DateTimeKind.Utc)
            };
            visitante.PasswordHash = hasher.HashPassword(visitante, "Guest123!");

            var user01 = new UserAccount
            {
                Id = 4,
                UserName = "user01",
                NormalizedUserName = "USER01",
                Email = "user01@vinhos.pt",
                NormalizedEmail = "USER01@VINHOS.PT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "User",
                MustChangePassword = false,
                QuintaId = 1,
                CreatedAt = new DateTime(2025, 7, 10, 14, 30, 0, DateTimeKind.Utc)
            };
            user01.PasswordHash = hasher.HashPassword(user01, "User01!");

            var convidado = new UserAccount
            {
                Id = 5,
                UserName = "convidado",
                NormalizedUserName = "CONVIDADO",
                Email = "convid@vinhos.pt",
                NormalizedEmail = "CONVID@VINHOS.PT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "User",
                MustChangePassword = false,
                QuintaId = 5,
                CreatedAt = new DateTime(2025, 7, 10, 16, 45, 0, DateTimeKind.Utc)
            };
            convidado.PasswordHash = hasher.HashPassword(convidado, "Conv123!");

            var guest2 = new UserAccount
            {
                Id = 6,
                UserName = "guest2",
                NormalizedUserName = "GUEST2",
                Email = "guest2@vinhos.pt",
                NormalizedEmail = "GUEST2@VINHOS.PT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = "User",
                MustChangePassword = true,
                QuintaId = 6,
                CreatedAt = new DateTime(2025, 7, 9, 12, 20, 0, DateTimeKind.Utc)
            };
            guest2.PasswordHash = hasher.HashPassword(guest2, "G2pass!");

            // 2) Regista todos de uma vez
            builder.HasData(admin, staff, visitante, user01, convidado, guest2);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppVinhosFinal.Models;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;

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
                new Vinhos { Id = 1, Nome = "Tinto Teste", Quantidade = 50, QuantidadeFria = 10, IdQuinta = 1, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 2, Nome = "Branco Oculto", Quantidade = 30, QuantidadeFria = 0, IdQuinta = 1, Estado = EstadoVinho.Hidden },
                new Vinhos { Id = 3, Nome = "Rosé Primavera", Quantidade = 100, QuantidadeFria = 60, IdQuinta = 3, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 4, Nome = "Espumante Real", Quantidade = 80, QuantidadeFria = 80, IdQuinta = 4, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 5, Nome = "Loureiro Verde", Quantidade = 120, QuantidadeFria = 70, IdQuinta = 5, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 6, Nome = "Alvarinho Clássico", Quantidade = 60, QuantidadeFria = 30, IdQuinta = 5, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 7, Nome = "Tinto Intenso", Quantidade = 40, QuantidadeFria = 20, IdQuinta = 2, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 8, Nome = "Branco Cítrico", Quantidade = 25, QuantidadeFria = 25, IdQuinta = 6, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 9, Nome = "Vinho de Mesa", Quantidade = 200, QuantidadeFria = 0, IdQuinta = 6, Estado = EstadoVinho.Hidden },
                new Vinhos { Id = 10, Nome = "Tinto Reserva", Quantidade = 75, QuantidadeFria = 15, IdQuinta = 3, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 11, Nome = "Branco Seco", Quantidade = 90, QuantidadeFria = 45, IdQuinta = 4, Estado = EstadoVinho.Visible },
                new Vinhos { Id = 12, Nome = "Vinha Velha", Quantidade = 30, QuantidadeFria = 5, IdQuinta = 1, Estado = EstadoVinho.Visible }
            );
        }
    }

    public class PedidosSeeder : IEntityTypeConfiguration<Pedidos>
    {
        public void Configure(EntityTypeBuilder<Pedidos> builder)
        {
            builder.HasData(
                new Pedidos { Id = 1, DataPedido = new DateTime(2025, 7, 11, 10, 0, 0, DateTimeKind.Utc), Estado = EstadoPedido.PorAprovar },
                new Pedidos { Id = 2, DataPedido = new DateTime(2025, 7, 10, 14, 30, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado },
                new Pedidos { Id = 3, DataPedido = new DateTime(2025, 7, 9, 16, 45, 0, DateTimeKind.Utc), Estado = EstadoPedido.Cancelado },
                new Pedidos { Id = 4, DataPedido = new DateTime(2025, 7, 9, 9, 15, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado },
                new Pedidos { Id = 5, DataPedido = new DateTime(2025, 7, 8, 11, 5, 0, DateTimeKind.Utc), Estado = EstadoPedido.PorAprovar },
                new Pedidos { Id = 6, DataPedido = new DateTime(2025, 7, 8, 18, 20, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado },
                new Pedidos { Id = 7, DataPedido = new DateTime(2025, 7, 7, 19, 30, 0, DateTimeKind.Utc), Estado = EstadoPedido.Cancelado },
                new Pedidos { Id = 8, DataPedido = new DateTime(2025, 7, 7, 13, 40, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado }
            );
        }
    }

    public class PedidoVinhoSeeder : IEntityTypeConfiguration<PedidoVinho>
    {
        public void Configure(EntityTypeBuilder<PedidoVinho> builder)
        {
            builder.HasData(
                // Pedido 1: 4 itens
                new PedidoVinho { Id = 1, IdPedido = 1, IdVinho = 1, Quantidade = 5 },
                new PedidoVinho { Id = 2, IdPedido = 1, IdVinho = 3, Quantidade = 2 },
                new PedidoVinho { Id = 3, IdPedido = 1, IdVinho = 5, Quantidade = 1 },
                new PedidoVinho { Id = 4, IdPedido = 1, IdVinho = 12, Quantidade = 1 },
                // Pedido 2: 2 itens
                new PedidoVinho { Id = 5, IdPedido = 2, IdVinho = 2, Quantidade = 10 },
                new PedidoVinho { Id = 6, IdPedido = 2, IdVinho = 4, Quantidade = 4 },
                // Pedido 3: 1 item
                new PedidoVinho { Id = 7, IdPedido = 3, IdVinho = 11, Quantidade = 20 },
                // Pedido 4: 3 itens
                new PedidoVinho { Id = 8, IdPedido = 4, IdVinho = 1, Quantidade = 3 },
                new PedidoVinho { Id = 9, IdPedido = 4, IdVinho = 6, Quantidade = 6 },
                new PedidoVinho { Id = 10, IdPedido = 4, IdVinho = 9, Quantidade = 2 },
                // Pedido 5: 2 itens
                new PedidoVinho { Id = 11, IdPedido = 5, IdVinho = 7, Quantidade = 8 },
                new PedidoVinho { Id = 12, IdPedido = 5, IdVinho = 8, Quantidade = 8 },
                // Pedido 6: 4 itens
                new PedidoVinho { Id = 13, IdPedido = 6, IdVinho = 10, Quantidade = 5 },
                new PedidoVinho { Id = 14, IdPedido = 6, IdVinho = 3, Quantidade = 10 },
                new PedidoVinho { Id = 15, IdPedido = 6, IdVinho = 12, Quantidade = 2 },
                new PedidoVinho { Id = 16, IdPedido = 6, IdVinho = 5, Quantidade = 3 },
                // Pedido 7: sem itens (testar vazio)
                // Pedido 8: 1 item duplicado
                new PedidoVinho { Id = 17, IdPedido = 8, IdVinho = 1, Quantidade = 3 },
                new PedidoVinho { Id = 18, IdPedido = 8, IdVinho = 1, Quantidade = 2 }
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
            builder.HasData(
                new UserAccount { Id = 1, UserName = "admin", Email = "admin@vinhos.pt", Password = "Admin123!", Role = "Admin", MustChangePassword = false, QuintaId = 1, CreatedAt = new DateTime(2025, 7, 11, 7, 0, 0) },
                new UserAccount { Id = 2, UserName = "staff01", Email = "staff01@vinhos.pt", Password = "Staff123!", Role = "Staff", MustChangePassword = false, QuintaId = 3, CreatedAt = new DateTime(2025, 7, 11, 8, 0, 0) },
                new UserAccount { Id = 3, UserName = "visitante1", Email = "visit1@vinhos.pt", Password = "Guest123!", Role = "User", MustChangePassword = true, QuintaId = 2, CreatedAt = new DateTime(2025, 7, 11, 9, 0, 0) },
                new UserAccount { Id = 4, UserName = "user01", Email = "user01@vinhos.pt", Password = "User01!", Role = "User", MustChangePassword = false, QuintaId = 1, CreatedAt = new DateTime(2025, 7, 10, 14, 30, 0) },
                new UserAccount { Id = 5, UserName = "convidado", Email = "convid@vinhos.pt", Password = "Conv123!", Role = "User", MustChangePassword = false, QuintaId = 5, CreatedAt = new DateTime(2025, 7, 10, 16, 45, 0) },
                new UserAccount { Id = 6, UserName = "guest2", Email = "guest2@vinhos.pt", Password = "G2pass!", Role = "User", MustChangePassword = true, QuintaId = 6, CreatedAt = new DateTime(2025, 7, 9, 12, 20, 0) }
            );
        }
    }
}

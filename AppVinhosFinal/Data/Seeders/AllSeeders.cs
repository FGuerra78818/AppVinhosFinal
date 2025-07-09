using System;
using System.Collections.Generic;
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
                new Quintas { Id = 4, Nome = "Vinhedo Real", NumeroMaxVinhoFrio = 30 }
            );
        }
    }

    public class VinhosSeeder : IEntityTypeConfiguration<Vinhos>
    {
        public void Configure(EntityTypeBuilder<Vinhos> builder)
        {
            builder.HasData(
                new Vinhos
                {
                    Id = 1,
                    Nome = "Tinto Teste",
                    Quantidade = 50,
                    QuantidadeFria = 10,
                    IdQuinta = 1,
                    Estado = EstadoVinho.Visible
                },
                new Vinhos
                {
                    Id = 2,
                    Nome = "Branco Oculto",
                    Quantidade = 30,
                    QuantidadeFria = 0,
                    IdQuinta = 1,
                    Estado = EstadoVinho.Hidden
                },
                new Vinhos
                {
                    Id = 3,
                    Nome = "Rosé Primavera",
                    Quantidade = 100,
                    QuantidadeFria = 60,
                    IdQuinta = 3,
                    Estado = EstadoVinho.Visible
                },
                new Vinhos
                {
                    Id = 4,
                    Nome = "Espumante Real",
                    Quantidade = 80,
                    QuantidadeFria = 80,
                    IdQuinta = 4,
                    Estado = EstadoVinho.Visible
                }
            );
        }
    }

    public class VinhosStockSeeder : IEntityTypeConfiguration<VinhosStock>
    {
        public void Configure(EntityTypeBuilder<VinhosStock> builder)
        {
            builder.HasData(
                // Vinho 1
                new VinhosStock { Id = 1, IdVinho = 1, Estado = EstadoVinhoStock.Frio, Quantidade = 10 },
                new VinhosStock { Id = 2, IdVinho = 1, Estado = EstadoVinhoStock.Quente, Quantidade = 40 },
                // Vinho 2
                new VinhosStock { Id = 3, IdVinho = 2, Estado = EstadoVinhoStock.Frio, Quantidade = 0 },
                new VinhosStock { Id = 4, IdVinho = 2, Estado = EstadoVinhoStock.Quente, Quantidade = 30 },
                // Vinho 3
                new VinhosStock { Id = 5, IdVinho = 3, Estado = EstadoVinhoStock.Frio, Quantidade = 60 },
                new VinhosStock { Id = 6, IdVinho = 3, Estado = EstadoVinhoStock.Quente, Quantidade = 40 },
                // Vinho 4
                new VinhosStock { Id = 7, IdVinho = 4, Estado = EstadoVinhoStock.Frio, Quantidade = 80 },
                new VinhosStock { Id = 8, IdVinho = 4, Estado = EstadoVinhoStock.Quente, Quantidade = 0 }
            );
        }
    }

    public class PedidosSeeder : IEntityTypeConfiguration<Pedidos>
    {
        public void Configure(EntityTypeBuilder<Pedidos> builder)
        {
            builder.HasData(
                new Pedidos { Id = 1, DataPedido = new DateTime(2025, 7, 9, 8, 0, 0, DateTimeKind.Utc), Estado = EstadoPedido.PorAprovar },
                new Pedidos { Id = 2, DataPedido = new DateTime(2025, 7, 8, 12, 30, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado },
                new Pedidos { Id = 3, DataPedido = new DateTime(2025, 7, 7, 15, 45, 0, DateTimeKind.Utc), Estado = EstadoPedido.Cancelado },
                new Pedidos { Id = 4, DataPedido = new DateTime(2025, 7, 6, 10, 15, 0, DateTimeKind.Utc), Estado = EstadoPedido.Aprovado }
            );
        }
    }

    public class PedidoVinhoSeeder : IEntityTypeConfiguration<PedidoVinho>
    {
        public void Configure(EntityTypeBuilder<PedidoVinho> builder)
        {
            builder.HasData(
                new PedidoVinho { IdPedido = 1, IdVinho = 1, Quantidade = 5 },
                new PedidoVinho { IdPedido = 2, IdVinho = 1, Quantidade = 10 },
                new PedidoVinho { IdPedido = 3, IdVinho = 2, Quantidade = 3 },
                new PedidoVinho { IdPedido = 4, IdVinho = 3, Quantidade = 20 }
            );
        }
    }

    public class LogsCoposSeeder : IEntityTypeConfiguration<LogsCopos>
    {
        public void Configure(EntityTypeBuilder<LogsCopos> builder)
        {
            builder.HasData(
                new LogsCopos
                {
                    Id = 1,
                    QuantidadeVendida = 12,
                    DataHoraVenda = new DateTime(2025, 7, 9, 10, 15, 0),
                    Comprador = "Cliente X"
                },
                new LogsCopos
                {
                    Id = 2,
                    QuantidadeVendida = 8,
                    DataHoraVenda = new DateTime(2025, 7, 9, 11, 0, 0),
                    Comprador = null
                },
                new LogsCopos
                {
                    Id = 3,
                    QuantidadeVendida = 25,
                    DataHoraVenda = new DateTime(2025, 7, 8, 18, 30, 0),
                    Comprador = "Cliente Y"
                },
                new LogsCopos
                {
                    Id = 4,
                    QuantidadeVendida = 5,
                    DataHoraVenda = new DateTime(2025, 7, 7, 20, 45, 0),
                    Comprador = "Cliente Z"
                }
            );
        }
    }

    public class UserAccountSeeder : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasData(
                // Admin inicial, não precisa trocar password
                new UserAccount
                {
                    Id = 1,
                    UserName = "admin",
                    Email = "admin@vinhos.pt",
                    Password = "Admin123!",
                    Role = "Admin",
                    CreatedAt = new DateTime(2025, 7, 9, 7, 0, 0),
                    MustChangePassword = false
                },
                // Staff inicial, não precisa trocar password
                new UserAccount
                {
                    Id = 2,
                    UserName = "staff01",
                    Email = "staff01@vinhos.pt",
                    Password = "Staff123!",
                    Role = "Staff",
                    CreatedAt = new DateTime(2025, 7, 9, 8, 0, 0),
                    MustChangePassword = false
                },
                // User criado via registo, precisa trocar password
                new UserAccount
                {
                    Id = 3,
                    UserName = "user03",
                    Email = "user03@vinhos.pt",
                    Password = "user03!",
                    Role = "User",
                    QuintaId = 3,
                    CreatedAt = new DateTime(2025, 7, 9, 9, 0, 0),
                    MustChangePassword = true
                },
                // Outro user já com password alterada
                new UserAccount
                {
                    Id = 4,
                    UserName = "user01",
                    Email = "user01@vinhos.pt",
                    Password = "User01!",
                    Role = "User",
                    QuintaId = 1,
                    CreatedAt = new DateTime(2025, 7, 8, 14, 30, 0),
                    MustChangePassword = false
                },
                new UserAccount
                {
                    Id = 4,
                    UserName = "user02",
                    Email = "user02@vinhos.pt",
                    Password = "User02!",
                    Role = "User",
                    QuintaId = 2,
                    CreatedAt = new DateTime(2025, 7, 8, 14, 30, 0),
                    MustChangePassword = false
                }
            );
        }
    }
}

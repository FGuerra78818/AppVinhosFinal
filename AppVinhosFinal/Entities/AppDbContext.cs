using AppVinhosFinal.Data.Seeders;
using AppVinhosFinal.Entities;
using AppVinhosFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppVinhosFinal.Entities
{
    public class AppDbContext
        : IdentityDbContext<
            UserAccount,               // o teu tipo de utilizador
            IdentityRole<int>,         // o tipo de role
            int,                       // tipo da chave primária
            IdentityUserClaim<int>,
            IdentityUserRole<int>,
            IdentityUserLogin<int>,
            IdentityRoleClaim<int>,
            IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets “não-Identity”
        public DbSet<LogsCopos> LogsCopos { get; set; }
        public DbSet<Vinhos> Vinhos { get; set; }
        public DbSet<Quintas> Quintas { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<PedidoVinho> PedidoVinhos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // *** remapeamento das tabelas Identity ***
            modelBuilder.Entity<UserAccount>(b => b.ToTable("UserAccounts"));
            modelBuilder.Entity<IdentityRole<int>>(b => b.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<int>>(b => b.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<int>>(b => b.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<int>>(b => b.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityRoleClaim<int>>(b => b.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserToken<int>>(b => b.ToTable("UserTokens"));

            base.OnModelCreating(modelBuilder);

            // só depois aplica os seeders
            modelBuilder.ApplyConfiguration(new QuintasSeeder());
            modelBuilder.ApplyConfiguration(new VinhosSeeder());
            modelBuilder.ApplyConfiguration(new PedidosSeeder());
            modelBuilder.ApplyConfiguration(new PedidoVinhoSeeder());
            modelBuilder.ApplyConfiguration(new LogsCoposSeeder());
            modelBuilder.ApplyConfiguration(new UserAccountSeeder());
        }
    }
}

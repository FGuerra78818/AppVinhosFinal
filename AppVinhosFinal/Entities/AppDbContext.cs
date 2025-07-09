using AppVinhosFinal.Data.Seeders;
using AppVinhosFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVinhosFinal.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<LogsCopos> LogsCopos { get; set; }
        public DbSet<Vinhos> Vinhos { get; set; }
        public DbSet<Quintas> Quintas { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<PedidoVinho> PedidoVinhos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuintasSeeder());
            modelBuilder.ApplyConfiguration(new VinhosSeeder());
            modelBuilder.ApplyConfiguration(new PedidosSeeder());
            modelBuilder.ApplyConfiguration(new PedidoVinhoSeeder());
            modelBuilder.ApplyConfiguration(new LogsCoposSeeder());
            modelBuilder.ApplyConfiguration(new UserAccountSeeder());

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM
{
    public class DefaultContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<Branch> Branches { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica as configurações de mapeamento das entidades (Fluent API)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configuração do relacionamento entre Sale e SaleProduct
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SaleProducts) // Sale tem muitos SaleProducts
                .WithOne(sp => sp.Sale) // SaleProduct tem uma Sale
                .HasForeignKey(sp => sp.SaleId); // Chave estrangeira em SaleProduct

            // Configuração do relacionamento entre SaleProduct e Product
            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product) // SaleProduct tem um Product
                .WithMany() // Product pode ter muitos SaleProducts
                .HasForeignKey(sp => sp.ProductId); // Chave estrangeira em SaleProduct

            base.OnModelCreating(modelBuilder);
        }
    }

    public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        public DefaultContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DefaultContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(
                   connectionString,
                   b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
            );

            return new DefaultContext(builder.Options);
        }
    }
}
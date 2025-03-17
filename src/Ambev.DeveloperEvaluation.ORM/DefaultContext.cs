using Ambev.DeveloperEvaluation.Common.Security;
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
            #region DatabaseTablesMigration
            // Aplica as configurações de mapeamento das entidades (Fluent API)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configuração do relacionamento entre Sale e SaleProduct
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SaleProducts) // Sale tem muitos SaleProducts
                .WithOne(sp => sp.Sale) // SaleProduct tem uma Sale
                .HasForeignKey(sp => sp.SaleId) // Chave estrangeira em SaleProduct
                .OnDelete(DeleteBehavior.Cascade); // Configura a exclusão em cascata

            // Configuração do relacionamento entre SaleProduct e Product
            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product) // SaleProduct tem um Product
                .WithMany() // Product pode ter muitos SaleProducts
                .HasForeignKey(sp => sp.ProductId); // Chave estrangeira em SaleProduct

            base.OnModelCreating(modelBuilder);
            #endregion

            #region DatabaseSeed
            // Dados iniciais para Branches (3 filiais)
            var branch1Id = new Guid("e49c076d-3046-4399-8d41-94cc9bb65dc0");
            var branch2Id = new Guid("c34d2387-e900-4704-9fe4-09bfaa50f0e1");
            var branch3Id = new Guid("f2495571-1390-411a-9bfd-669185afea64");

            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Id = branch1Id,
                    Name = "Filial São Paulo",
                    Address = "São Paulo - SP, Bairro Velha Guarda, nº 60",
                    CreatedAt = DateTime.UtcNow
                },
                new Branch
                {
                    Id = branch2Id,
                    Name = "Filial Florianópolis",
                    Address = "Florianópolis - SC, Trindade, nº 195",
                    CreatedAt = DateTime.UtcNow
                },
                new Branch
                {
                    Id = branch3Id,
                    Name = "Filial Curitiba",
                    Address = "Curitiba - PR, Centro, nº 300",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Dados iniciais para Users (3 usuários)
            var user1Id = new Guid("ffb140dc-53be-48d5-8c6e-5d3f93271bff");
            var user2Id = new Guid("bb6f91f2-1720-4191-9022-24254953fa18");
            var user3Id = new Guid("0af35800-836a-418a-8a44-f9d21b832fa2");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = user1Id,
                    Username = "joao.silva",
                    Email = "joao.silva@example.com",
                    Password = "$2a$11$FNkpDRmEjWrMDD2zZdX7BOZQ2z/nYo04bwW3rFxGb8u7/8YHb6cTG", // Password: Senha1234!
                    Phone = "11987654321",
                    Role = Domain.Enums.UserRole.Admin,
                    Status = Domain.Enums.UserStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = user2Id,
                    Username = "maria.souza",
                    Email = "maria.souza@example.com",
                    Password = "$2a$11$FNkpDRmEjWrMDD2zZdX7BOZQ2z/nYo04bwW3rFxGb8u7/8YHb6cTG", // Password: Senha1234!
                    Phone = "11912345678",
                    Role = Domain.Enums.UserRole.Manager,
                    Status = Domain.Enums.UserStatus.Inactive,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = user3Id,
                    Username = "carlos.oliveira",
                    Email = "carlos.oliveira@example.com",
                    Password = "$2a$11$FNkpDRmEjWrMDD2zZdX7BOZQ2z/nYo04bwW3rFxGb8u7/8YHb6cTG", // Password: Senha1234!
                    Phone = "11955556666",
                    Role = Domain.Enums.UserRole.Customer,
                    Status = Domain.Enums.UserStatus.Active,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Dados iniciais para Products (10 produtos)
            var product1Id = new Guid("2555343d-facf-4c33-b8a6-5fad76be36a3");
            var product2Id = new Guid("0f038ef4-2b67-4b7b-9454-801ab6581f54");
            var product3Id = new Guid("ccc9047b-23d4-4260-b29e-2b9c0229bf86");
            var product4Id = new Guid("4e662887-962a-46d8-9aed-aa6c0efb7e76");
            var product5Id = new Guid("184c3336-c66e-42e5-8738-11cbe96f527a");
            var product6Id = new Guid("5d0b27be-eaeb-4483-8960-489e65f2452d");
            var product7Id = new Guid("d7dae433-b37b-41c4-8e86-330fe3259c03");
            var product8Id = new Guid("955f7c88-fe46-483b-9411-19ee00271974");
            var product9Id = new Guid("1ce1afda-b45d-41ea-a2f4-3c4554154aa6");
            var product10Id = new Guid("eb938eb0-572a-4ae2-953a-2bd55e1709bc");

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = product1Id,
                    Name = "Arroz Integral",
                    Description = "Arroz integral orgânico, pacote de 5kg",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product2Id,
                    Name = "Feijão Preto",
                    Description = "Feijão preto, pacote de 1kg",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product3Id,
                    Name = "Azeite de Oliva",
                    Description = "Azeite extra virgem, garrafa de 500ml",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product4Id,
                    Name = "Macarrão Espaguete",
                    Description = "Macarrão espaguete, pacote de 500g",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product5Id,
                    Name = "Leite Integral",
                    Description = "Leite integral, caixa de 1L",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product6Id,
                    Name = "Café em Grãos",
                    Description = "Café em grãos torrado, pacote de 1kg",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product7Id,
                    Name = "Sabão em Pó",
                    Description = "Sabão em pó para roupas, pacote de 2kg",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product8Id,
                    Name = "Papel Higiênico",
                    Description = "Papel higiênico folha dupla, pacote com 12 rolos",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product9Id,
                    Name = "Desinfetante",
                    Description = "Desinfetante aroma pinho, frasco de 2L",
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = product10Id,
                    Name = "Detergente Líquido",
                    Description = "Detergente líquido neutro, frasco de 500ml",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Dados iniciais para SaleProducts (itens das compras)
            var saleProduct1 = new SaleProduct(
                id: new Guid("019e40fb-fd1a-4edb-a061-b036060c1a64"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product1Id,
                quantity: 5,
                unitPrice: 25.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct2 = new SaleProduct(
                id: new Guid("4b263c2b-d9ad-4149-9e41-0ca45977f7f2"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product2Id,
                quantity: 10,
                unitPrice: 8.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct3 = new SaleProduct(
                id: new Guid("60e1e8fd-8346-4210-a01e-8a1ee795435b"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product3Id,
                quantity: 2,
                unitPrice: 30.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct4 = new SaleProduct(
                id: new Guid("2edf25e5-a58d-4e3b-b93c-3d1957433b31"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product4Id,
                quantity: 20,
                unitPrice: 5.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct5 = new SaleProduct(
                id: new Guid("4dfbec1a-adbb-4148-ab8d-4b23d692f549"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product5Id,
                quantity: 14,
                unitPrice: 4.50m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct6 = new SaleProduct(
                id: new Guid("bc70cb2d-e0cd-4b3d-a610-1c101f846926"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product6Id,
                quantity: 11,
                unitPrice: 50.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct7 = new SaleProduct(
                id: new Guid("26fe8552-e4ca-4036-8470-9abdd06f48b6"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product7Id,
                quantity: 10,
                unitPrice: 15.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct8 = new SaleProduct(
                id: new Guid("4a93decd-8ce8-4a58-bc82-d159769f9965"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product8Id,
                quantity: 3,
                unitPrice: 20.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct9 = new SaleProduct(
                id: new Guid("1063acfd-e15c-4f6f-8825-ae7f9036e188"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product9Id,
                quantity: 5,
                unitPrice: 10.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            var saleProduct10 = new SaleProduct(
                id: new Guid("e22d970d-a3ce-43ba-85e1-14ced4c267fe"),
                saleId: Guid.Empty, // Será atualizado após a criação da venda
                productId: product10Id,
                quantity: 2,
                unitPrice: 5.00m,
                percentageDiscount: 0,
                fixedDiscount: 0,
                dateSold: DateTime.UtcNow,
                product: null
            );

            // Dados iniciais para Sales (5 compras)
            var sale1 = new Sale(
                clientId: user1Id,
                branchId: branch1Id,
                number: "SALE001",
                dateSold: DateTime.UtcNow,
                products: new List<SaleProduct> { saleProduct1, saleProduct2 }
            )
            {
                Id = new Guid("6fc34cef-afed-4edb-ab5f-44a1e2eea0a9")
            };

            var sale2 = new Sale(
                clientId: user2Id,
                branchId: branch2Id,
                number: "SALE002",
                dateSold: DateTime.UtcNow.AddDays(-1),
                products: new List<SaleProduct> { saleProduct3, saleProduct4 }
            )
            {
                Id = new Guid("e8765c98-ce78-4090-ae9a-903101d023c1")
            };

            var sale3 = new Sale(
                clientId: user3Id,
                branchId: branch3Id,
                number: "SALE003",
                dateSold: DateTime.UtcNow.AddDays(-2),
                products: new List<SaleProduct> { saleProduct5, saleProduct6 }
            )
            {
                Id = new Guid("a37f6397-3a09-435f-829d-a19c19728a9b")
            };

            var sale4 = new Sale(
                clientId: user1Id,
                branchId: branch1Id,
                number: "SALE004",
                dateSold: DateTime.UtcNow.AddDays(-3),
                products: new List<SaleProduct> { saleProduct7, saleProduct8 }
            )
            {
                Id = new Guid("acfeb46e-4383-48dc-af5a-9d1d64445042")
            };

            var sale5 = new Sale(
                clientId: user2Id,
                branchId: branch2Id,
                number: "SALE005",
                dateSold: DateTime.UtcNow.AddDays(-4),
                products: new List<SaleProduct> { saleProduct9, saleProduct10 }
            )
            {
                Id = new Guid("62bad870-cf24-4dee-8470-988fb0ee3361")
            };

            // Atualiza os SaleIds nos SaleProducts
            saleProduct1.SaleId = sale1.Id;
            saleProduct2.SaleId = sale1.Id;
            saleProduct3.SaleId = sale2.Id;
            saleProduct4.SaleId = sale2.Id;
            saleProduct5.SaleId = sale3.Id;
            saleProduct6.SaleId = sale3.Id;
            saleProduct7.SaleId = sale4.Id;
            saleProduct8.SaleId = sale4.Id;
            saleProduct9.SaleId = sale5.Id;
            saleProduct10.SaleId = sale5.Id;

            modelBuilder.Entity<SaleProduct>().HasData(
                saleProduct1, saleProduct2, saleProduct3, saleProduct4, saleProduct5,
                saleProduct6, saleProduct7, saleProduct8, saleProduct9, saleProduct10
            );

            // É necessário limpar pois o EFCore não permite inserção de relacionamentos, e precisou ser inserido para calcular desconto
            sale1.SaleProducts.Clear();
            sale2.SaleProducts.Clear();
            sale3.SaleProducts.Clear();
            sale4.SaleProducts.Clear();
            sale5.SaleProducts.Clear();

            // Adiciona as vendas e os produtos ao contexto
            modelBuilder.Entity<Sale>().HasData(sale1, sale2, sale3, sale4, sale5);
            #endregion
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
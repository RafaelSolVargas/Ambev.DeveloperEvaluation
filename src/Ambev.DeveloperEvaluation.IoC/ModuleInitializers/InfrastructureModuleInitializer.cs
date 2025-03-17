using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Rebus.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Routing.TypeBased;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBranchRepository, BranchRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        // Configuração do Rebus com RabbitMQ
        builder.Services.AddRebus(configure => configure
            .Transport(t => t.UseRabbitMq("amqp://user:password@localhost", "sale-events-queue"))
            .Routing(r => r.TypeBased()
                .Map<SaleCancelledEvent>("sale-events-queue")
                .Map<SaleCreatedEvent>("sale-events-queue")
                .Map<SaleModifiedEvent>("sale-events-queue")
                .Map<SaleUncancelledEvent>("sale-events-queue"))
            .Logging(l => l.Console()) // Habilita logs do Rebus no console
        );
    }
}
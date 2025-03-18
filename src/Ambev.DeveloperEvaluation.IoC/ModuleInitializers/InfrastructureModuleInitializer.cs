using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Rebus.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Routing.TypeBased;
using Ambev.DeveloperEvaluation.Domain.Events;
using StackExchange.Redis;
using Ambev.DeveloperEvaluation.Application.EventsHandlers;
using Rebus.Handlers;
using Ambev.DeveloperEvaluation.Application;

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

        // Configuração do Redis
        var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost";
        var redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD") ?? "ev@luAt10n";

        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { $"{redisHost}:6379" },
                Password = redisPassword,
                AbortOnConnectFail = false
            }));

        // Configuração do Rebus com RabbitMQ
        var rabbitMQHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";

        builder.Services.AddRebus(configure => configure
            .Transport(t => t.UseRabbitMq($"amqp://user:password@{rabbitMQHost}", "sale-events-queue"))
            .Routing(r => r.TypeBased()
                .Map<SaleCancelledEvent>("sale-events-queue")
                .Map<SaleCreatedEvent>("sale-events-queue")
                .Map<SaleModifiedEvent>("sale-events-queue")
                .Map<SaleUncancelledEvent>("sale-events-queue"))
            .Logging(l => l.Console()) // Habilita logs do Rebus no console
        );

        // Registro manual dos handlers
        builder.Services.AddScoped<IHandleMessages<SaleCancelledEvent>, SaleCancelledEventHandler>();
        builder.Services.AddScoped<IHandleMessages<SaleCreatedEvent>, SaleCreatedEventHandler>();
        builder.Services.AddScoped<IHandleMessages<SaleModifiedEvent>, SaleModifiedEventHandler>();
        builder.Services.AddScoped<IHandleMessages<SaleUncancelledEvent>, SaleUncancelledEventHandler>();
    }
}
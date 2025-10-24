using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using BankingSystem.Api.Middleware;
using BankingSystem.Application.Contracts.Repository;
using BankingSystem.Application.Features.Customers.Commands.CreateCustomer;
using BankingSystem.Application.Interfaces;
using BankingSystem.Infrastructure.Repositories;
using BankingSystem.Persistence.DatabaseContext;
using BankingSystem.Persistence.Repositories;
using BankingSystem.Persistence.Services;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Configuration
var checksumSecret = builder.Configuration["ChecksumSecret"] ?? "dev-secret";

// Add services to DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
    options.AddPolicy("AllowBlazorClient", builder => builder
        .WithOrigins("https://localhost:7247")
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

// EF Core
builder.Services.AddDbContext<BankingDbContext>(opts 
    => opts.UseSqlite(builder.Configuration.GetConnectionString("HrDatabaseConnectionString")));

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterAutoMapper(typeof(CreateCustomerCommand).Assembly);

    var configuration = MediatRConfigurationBuilder
    .Create("", typeof(CreateCustomerCommand).Assembly)
    .WithAllOpenGenericHandlerTypesRegistered()
    .Build();

    container.RegisterMediatR(configuration);

    // Repositories
    container.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

    // App services
    container.RegisterType<HmacChecksumService>().As<IChecksumService>().WithParameter("secret", checksumSecret).SingleInstance();

    // You may register other handlers here...
    container.RegisterType<CustomerRepository>().As<ICustomerRepository>().SingleInstance();
    container.RegisterType<AccountRepository>().As<IAccountRepository>().SingleInstance();
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Migrate/Ensure DB
using (var sc = app.Services.CreateScope())
{
    var db = sc.ServiceProvider.GetRequiredService<BankingDbContext>();
    db.Database.EnsureCreated();
}

// Enable CORS before MapControllers()
app.UseCors("AllowBlazorClient");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

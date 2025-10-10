using Autofac;
using Autofac.Extensions.DependencyInjection;
using BankingSystem.Persistence;
using BankingSystem.Persistence.Repositories;
using BankingSystem.Persistence.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Configuration
var conn = builder.Configuration.GetConnectionString("Default") ?? "Data Source=banking.db";
var checksumSecret = builder.Configuration["ChecksumSecret"] ?? "dev-secret";

// Add services to DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<BankingDbContext>(opts => opts.UseSqlite(conn));

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // Repositories
    container.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(BankingSystem.Application.Interfaces.IRepository<>)).InstancePerLifetimeScope();

    // App services
    container.RegisterType<HmacChecksumService>().As<BankingSystem.Application.Interfaces.IChecksumService>().WithParameter("secret", checksumSecret).SingleInstance();
    container.RegisterType<AccountService>().SingleInstance();

    // You may register other handlers here...
});

var app = builder.Build();

// Migrate/Ensure DB
using (var sc = app.Services.CreateScope())
{
    var db = sc.ServiceProvider.GetRequiredService<BankingDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

using Autofac;
using Autofac.Extensions.DependencyInjection;
using BankingSystem.Api.Middleware;
using BankingSystem.Application.DI;
using BankingSystem.Application.Interfaces;
using BankingSystem.Identity.DbContext;
using BankingSystem.Identity.DI;
using BankingSystem.Identity.Models;
using BankingSystem.Persistence.DatabaseContext;
using BankingSystem.Persistence.DI;
using BankingSystem.Persistence.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Use Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

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


builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule<ApplicationModule>();
    container.RegisterModule<PersistenceModule>();

    // App services
    container.RegisterType<HmacChecksumService>().As<IChecksumService>().WithParameter("secret", checksumSecret).SingleInstance();

});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// --- Ensure and/or migrate databases ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // --- Main app database ---
    var appDb = services.GetRequiredService<BankingDbContext>();
    appDb.Database.EnsureCreated();

    // --- Identity database ---
    var identityDb = services.GetRequiredService<BankingSystemDbContext>();

    if (app.Environment.IsEnvironment("CI"))
    {
        // Clean rebuild for CI (ensures seeded users/roles exist)
        identityDb.Database.EnsureDeleted();
        identityDb.Database.Migrate();
    }
    else
    {
        // Local/dev/staging
        identityDb.Database.Migrate();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorClient");

if (!app.Environment.IsEnvironment("CI"))
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok("Healthy"));

app.Run();


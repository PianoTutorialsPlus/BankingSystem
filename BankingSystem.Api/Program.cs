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
using Microsoft.AspNetCore.Identity;
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
    identityDb.Database.Migrate();
}

if (app.Environment.IsEnvironment("CI"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BankingSystemDbContext>();
    db.Database.Migrate(); // creates tables including Identity tables


    // Seed users/roles automatically
    var hasher = new PasswordHasher<ApplicationUser>();
    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new ApplicationUser
            {
                Id = "918043f8-d092-4b9d-be3e-63eae8307e2b",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new ApplicationUser
            {
                Id = "a2634141-eb89-4438-a70e-ad8f3ecbfe9b",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            });
    }

    if (!db.Roles.Any())
    {
        db.Roles.AddRange(
            new IdentityRole { Id = "2a95a9cd-c232-443a-a6d2-c613be45185b", Name = "Employee", NormalizedName = "EMPLOYEE" },
            new IdentityRole { Id = "f701d759-adf9-47cd-8f22-5b21e9c52ac9", Name = "Administrator", NormalizedName = "ADMIN" }
        );
    }

    db.SaveChanges();
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


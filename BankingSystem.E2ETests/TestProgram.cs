// File: TestProgram.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankingSystem.E2ETests
{
    public class TestProgram
    {
        public static IHost BuildHost(string[]? args = null)
        {
            var builder = WebApplication.CreateBuilder(args ?? Array.Empty<string>());

            // Register all the usual services of your API
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ⬅️ Add your custom API setup extension if you have one
            // builder.Services.AddMyApi();

            var app = builder.Build();
            app.MapControllers();

            return app;
        }
    }
}

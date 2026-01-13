using Infrastructure;
using Application;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddJwtAuthentication(builder.Services.GetJwtSettings(builder.Configuration));
        builder.Services.AddApplicationServices();

        var app = builder.Build();

        await app.Services.AddDatabaseInitializerAsync();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        //app.UseAuthorization();
        app.UseInfrastructure();
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.MapControllers();

        app.Run();
    }
}

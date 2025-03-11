using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();


            ConfigureServices(builder);

            var app = builder.Build();


            ConfigureMiddleware(app);

            ApplyMigrations(app);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.AddBasicHealthChecks();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DefaultContext>(options =>
     options.UseNpgsql(
         builder.Configuration.GetConnectionString("DefaultConnection"),
         b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
     )
 );


        builder.Services.AddJwtAuthentication(builder.Configuration);

        builder.RegisterDependencies();

        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
        builder.Services.AddScoped<SaleService>();
        builder.Services.AddScoped<SaleRepository>();

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        app.UseMiddleware<ValidationExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // Middleware de logging para requisi��es HTTP
        app.UseSerilogRequestLogging();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseBasicHealthChecks();

        app.MapControllers();
    }

    private static void ApplyMigrations(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        dbContext.Database.Migrate();
    }
}

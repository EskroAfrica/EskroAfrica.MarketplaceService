using EskroAfrica.MarketplaceService.API;
using EskroAfrica.MarketplaceService.Infrastructure.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;
/*
 - add serilog
 - add kafka
 - add hangfire
 - add redis
 - add exception handler
 */

// added comment
// added another comment
// one more comment

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;

bool writeToSeq = builder.Configuration.GetValue<bool>("AppSettings:LogSettings:WriteToSeq");
bool writeToFile = builder.Configuration.GetValue<bool>("AppSettings:LogSettings:WriteToFile");

if (writeToSeq)
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Seq(builder.Configuration.GetValue<string>("AppSettings:LogSettings:LogUrl"))
        .CreateLogger();
}else if (writeToFile)
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt",
            rollingInterval: RollingInterval.Day,
            rollOnFileSizeLimit: true)
        .CreateLogger();
}

Log.Information("Starting up MarketplaceService");

try
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddConfigurations(config);

    builder.Services.AddCors();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    if (builder.Configuration.GetValue<bool>("AppSettings:EnableMigration"))
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MarketplaceServiceDbContext>();
            dbContext.Database.Migrate();
        }
    }

    if (builder.Configuration.GetValue<bool>("AppSettings:EnableSeeding"))
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MarketplaceServiceDbContext>();
            dbContext.Database.Migrate();
            await Seeder.SeedAsync(dbContext);
        }
    }

    app.UseCors(options =>
    {
        options.AllowAnyOrigin();
    });
    app.UseHttpsRedirection();

    app.UseHangfireDashboard();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers().RequireAuthorization();

    app.Run();
}
catch (Exception e)
{
    Log.Fatal($"Failed to start MarketplaceService - {e.Message}");
}
finally
{
    Log.Information("Closing MarketplaceService");
    Log.CloseAndFlush();
}



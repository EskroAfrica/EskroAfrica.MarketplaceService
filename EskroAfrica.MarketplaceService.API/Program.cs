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

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration.GetValue<string>("AppSettings:LogSettings:LogUrl"))
    .CreateLogger();

Log.Information("Starting up MarketplaceService");

try
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddConfigurations(config);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<MarketplaceServiceDbContext>();
        dbContext.Database.Migrate();
        await Seeder.SeedAsync(dbContext);
    }


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



using EskroAfrica.MarketplaceService.API;
using Hangfire;
using Serilog;
/*
 - add serilog
 - add kafka
 - add hangfire
 - add redis
 - add exception handler
 */

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
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseHangfireDashboard();

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



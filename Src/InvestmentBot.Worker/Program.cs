using InvestmentBot.Application.DI;
using InvestmentBot.ExternalServices.SEC.DI;
using InvestmentBot.ExternalServices.Telegram.DI;
using Serilog;

namespace InvestmentBot.Worker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddEnvironmentVariables();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("/var/log/investment-bot/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        builder.Services
            .AddHostedService<Worker>()
            .AddTelegramModule(builder.Configuration)
            .AddSecModule(builder.Configuration)
            .AddApplicationModule(builder.Configuration);

        var host = builder.Build();
        host.Run();
    }
}
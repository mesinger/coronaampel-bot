using System;
using System.IO;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Data.Data;
using Mesi.Covid.Austria.CoronaAmpel.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Mesi.Covid.Austria.CoronaAmpel.TelegramBotSender
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json");
            
            _configuration = builder.Build();
        }

        public void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder => builder.AddSerilog());

            services.AddScoped<IGetCoronaStopLightDataForCommune, CoronaStopLightService>();
            services.AddScoped<ICoronaStopLightRepository, CoronaStopLightWeek2Repository>();

            services.AddHttpClient<ICoronaStopLightRepository, CoronaStopLightWeek2Repository>(client =>
            {
                client.BaseAddress = new Uri(_configuration["CoronaAmpelData:BaseUri"]);
            });

            services.Configure<CoronaAmpelOptions>(_configuration.GetSection("CoronaAmpelData"));

            return services.BuildServiceProvider();
        }
    }
}
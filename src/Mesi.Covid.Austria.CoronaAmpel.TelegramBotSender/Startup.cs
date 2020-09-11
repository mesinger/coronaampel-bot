using System;
using System.Collections.Generic;
using System.IO;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Data;
using Mesi.Covid.Austria.CoronaAmpel.Contract.Localization;
using Mesi.Covid.Austria.CoronaAmpel.Data.Data;
using Mesi.Covid.Austria.CoronaAmpel.Localization;
using Mesi.Covid.Austria.CoronaAmpel.Services;
using Mesi.Covid.Austria.CoronaAmpel.Telegram.Data.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Mesi.Covid.Austria.CoronaAmpel.TelegramBotSender
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json")
                .AddCommandLine(args);
            
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

            services.AddSingleton<IGetCoronaStopLightDataForCommune, CoronaStopLightService>();
            services.AddSingleton<ICoronaStopLightRepository, CoronaStopLightWeek2Repository>();
            services.AddSingleton<ICoronaStopLightMessageService, CoronaStopLightTelegramMessageService>();
            services.AddSingleton<ICoronaStopLightLocalizationService, CoronaStopLightLocalizationService>();

            services.AddHttpClient<ICoronaStopLightRepository, CoronaStopLightWeek2Repository>(client =>
            {
                client.BaseAddress = new Uri(_configuration["CoronaAmpelData:BaseUrl"]);
            });
            
            services.AddHttpClient<ICoronaStopLightMessageService, CoronaStopLightTelegramMessageService>();

            services.Configure<CoronaAmpelOptions>(_configuration.GetSection("CoronaAmpelData"));
            services.Configure<TelegramOptions>(_configuration.GetSection("Telegram"));
            services.Configure<LocalizationOptions>(_configuration.GetSection("Localization"));
            services.Configure<MessageOptions>(_configuration.GetSection("Message"));

            return services.BuildServiceProvider();
        }
    }
}
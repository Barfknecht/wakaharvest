using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WakaHarvest.Providers;
using WakaHarvest.Providers.Implementations;
using WakaHarvest.Services;
using WakaHarvest.Services.Implementations;
using WakaHarvest.Settings.Options;

[assembly: FunctionsStartup(typeof(WakaHarvest.Startup))]

namespace WakaHarvest
{
    public class Startup : FunctionsStartup
    {
        private static IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddTransient<IWakaTimeService, WakaTimeService>();
            builder.Services.AddTransient<IHarvestService, HarvestService>();
            builder.Services.AddSingleton<IRequestUrlProvider, RequestUrlProvider>();
            builder.Services.AddSingleton<IHttpClientProvider, HttpClientProvider>();
            AddOptions(builder);
        }

        private static void AddOptions(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<UserOptions>()
                .Configure<IOptions<ExecutionContextOptions>>(
                    (settings, context) =>
                    {
                        var configSection = ConfigureAppSettings(context.Value.AppDirectory)
                            .GetSection(nameof(UserOptions));
                        var projectOptions = configSection.GetSection(nameof(ProjectOptions)).Get<List<ProjectOptions>>();
                        var wakaOptions = configSection.GetSection(nameof(WakaTimeOptions)).Get<WakaTimeOptions>();
                        var harvestOptions = configSection.GetSection(nameof(HarvestOptions)).Get<HarvestOptions>();
                        settings.HarvestOptions = harvestOptions;
                        settings.WakaTimeOptions = wakaOptions;
                        settings.Projects = projectOptions;
                        configSection.Bind(settings);
                    });
        }

        private static IConfigurationRoot ConfigureAppSettings(string appDirectory) =>
            _functionConfig ??= new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(appDirectory, "local.settings.json"), optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
    }
}
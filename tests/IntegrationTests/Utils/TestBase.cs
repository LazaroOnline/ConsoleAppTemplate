namespace IntegrationTests.Utils;

public class TestBase
{
    public IHostBuilder GetHost(
        IEnumerable<KeyValuePair<string, string>> appSettingsPairs = null,
        Action<HostBuilderContext, IServiceCollection> configureServices = null
    )
    {
        IConfiguration configuration = null;
        var appSettingsResolved = ResolveShortHandParams(appSettingsPairs);
        var hostBuilder = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configuration = configApp
                .AddJsonFile(Program.APPSETTINGS_FILENAME, optional: true, reloadOnChange: false)
                .AddInMemoryCollection(appSettingsResolved)
                .Build();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions<AppSettings>().Bind(configuration).ValidateDataAnnotations();
                services.AddHostedService<App>();
                services.RegisterServicesForDependencyInjection();
                //services.AddTransient<Workers.MainWorker>();
                configureServices?.Invoke(hostContext, services);
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
            });

        return hostBuilder;
    }

    public IEnumerable<KeyValuePair<string, string>> ResolveShortHandParams(IEnumerable<KeyValuePair<string, string>> appSettingsPairs)
    {
        return appSettingsPairs.Select(p => new KeyValuePair<string, string>(ResolveShortHandParamKey(p.Key), p.Value));
    }

    public string ResolveShortHandParamKey(string appSettingsKey)
    {
        var hasMapping = Parameters.SwitchMappings.TryGetValue(appSettingsKey, out string mappingValue);
        return mappingValue ?? appSettingsKey;
    }
}

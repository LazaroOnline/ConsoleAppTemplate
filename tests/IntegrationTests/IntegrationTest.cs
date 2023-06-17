namespace IntegrationTests;

public class IntegrationTest
{
    public IHostBuilder GetHost(
        IEnumerable<KeyValuePair<string, string?>>? appSettingsDictionary = null,
        Action<HostBuilderContext, IServiceCollection>? configureServices = null
    )
    {
        IConfiguration configuration = null;
        var hostBuilder = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configuration = configApp
                .AddJsonFile(Program.APPSETTINGS_FILENAME, optional: true, reloadOnChange: false)
                .AddInMemoryCollection(appSettingsDictionary)
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
            .ConfigureLogging((hostingContext, logging) => {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
            });

        return hostBuilder;
    }

    [Fact]
	public async Task Integration_Test1()
	{
        var appSettings = new Dictionary<string, string>();
        var mainWorkerMock = Substitute.For<IMainWorker>();

        var hostBuilder = GetHost(appSettings, (hostContext, services) => {
            services.RemoveServices<IMainWorker>();
            services.AddScoped<IMainWorker>((services) => mainWorkerMock);
        });
        var host = await hostBuilder.StartAsync();
        // TODO: add some logic in your tests:

        mainWorkerMock.Received(1).Main();
    }

    [Fact]
    public async Task Integration_Test_NoSetupChanges()
    {
        var appSettings = new Dictionary<string, string>();
        var mainWorkerMock = Substitute.For<IMainWorker>();

        var hostBuilder = GetHost();
        var host = await hostBuilder.StartAsync();

        // TODO: add some logic in your tests:
        true.Should().Be(true);
    }

}

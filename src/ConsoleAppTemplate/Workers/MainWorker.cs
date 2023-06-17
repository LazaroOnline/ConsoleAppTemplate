namespace ConsoleAppTemplate.Workers;

public interface IMainWorker
{
	void Main();
}

public class MainWorker : IMainWorker
{
	private ILogger<MainWorker> _logger;
	private IOptions<AppSettings> _appSettings;
	
	public MainWorker(
		ILogger<MainWorker> logger,
		IOptions<AppSettings> appSettins
	) {
		_logger = logger;
		_appSettings = appSettins;
	}

	public void Main()
	{
		var appSettings = _appSettings.Value;
		_logger.LogInformation($"Starting {nameof(MainWorker)}...");
		_logger.LogInformation($"AppSettings Name: {appSettings.SomeConfigSection.SomeName}");
		_logger.LogInformation($"AppSettings URL: {appSettings.SomeConfigSection.SomeUrl}");
        // Do something here...

    }
}

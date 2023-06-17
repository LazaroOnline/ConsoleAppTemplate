namespace ConsoleAppTemplate.Workers;

public interface IMainWorker
{
	void Main();
}

public class MainWorker : IMainWorker
{
	private ILogger<MainWorker> _logger;
	private IOptions<AppSettings> _appSettins;
	
	public MainWorker(
		ILogger<MainWorker> logger,
		IOptions<AppSettings> appSettins
	) {
		_logger = logger;
		_appSettins = appSettins;
	}

	public void Main()
	{
		var appSettings = _appSettins.Value;
		_logger.LogInformation($"Starting {nameof(MainWorker)}...");
		_logger.LogInformation($"AppSettings Name: {appSettings.SomeConfigSection.SomeName}");
		_logger.LogInformation($"AppSettings URL: {appSettings.SomeConfigSection.SomeUrl}");
        // Do something here...

    }
}

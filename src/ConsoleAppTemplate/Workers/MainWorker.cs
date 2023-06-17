namespace ConsoleAppTemplate.Workers;

public class MainWorker
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
		// Do something here...

	}
}

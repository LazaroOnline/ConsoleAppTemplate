namespace ConsoleAppTemplate;

public class App : IHostedService
{
	private readonly IHostApplicationLifetime _applicationLifetime;
	private ILogger<App> _logger;
	private IOptions<AppSettings> _appSettins;
	private Workers.IMainWorker _mainWorker;
	
	public App(
		IHostApplicationLifetime applicationLifetime,
		ILogger<App> logger,
		IOptions<AppSettings> appSettins,
		Workers.IMainWorker mainWorker
	) {
		_applicationLifetime = applicationLifetime;
		_logger = logger;
		_appSettins = appSettins;
		_mainWorker = mainWorker;
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		try
			{
			Execute();
			System.Environment.ExitCode = 0;
		}
		catch (Exception exception)
		{
			_logger.LogCritical(exception, "Unexpected error exception!");
			System.Environment.ExitCode = 1;
		}
		_applicationLifetime.StopApplication();

		return Task.CompletedTask;
	}
	
	public void Execute()
	{
		_logger.LogDebug($"Starting app...");
		_mainWorker.Main();
		_logger.LogDebug($"App finished running.");
        //Console.ReadLine();
    }

    public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}
}

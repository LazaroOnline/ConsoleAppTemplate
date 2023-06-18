namespace ConsoleAppTemplate;

public class App : IHostedService
{
	private readonly IHostApplicationLifetime _applicationLifetime;
	private readonly ILogger<App> _logger;
	private readonly IOptions<AppSettings> _appSettings;
	private readonly Workers.IMainWorker _mainWorker;
    private readonly AppArguments _appArguments;

    public App(
		IHostApplicationLifetime applicationLifetime,
		ILogger<App> logger,
		IOptions<AppSettings> appSettings,
		Workers.IMainWorker mainWorker,
        AppArguments appArguments
	)
    {
        _applicationLifetime = applicationLifetime;
		_logger = logger;
		_appSettings = appSettings;
		_mainWorker = mainWorker;
        _appArguments = appArguments;
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
        var appSettings = _appSettings.Value;
        _logger.LogInformation($"Starting ConsoleAppTemplate v{Parameters.Version}...");
        LogConfig(appSettings);

        if (_appArguments.Args.Any(arg => Parameters.IsCommandArgument(arg, Parameters.AppCommand.Help)))
        {
            Console.WriteLine(Parameters.GetHelpMessage());
            return;
        }

        if (_appArguments.Args.Any(arg => Parameters.IsCommandArgument(arg, Parameters.AppCommand.Version)))
        {
            Console.WriteLine(Parameters.VersionMessage);
            return;
        }

        _mainWorker.Main();
		_logger.LogDebug($"App finished running.");
        //Console.ReadLine();
    }

    public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
    }

    public void LogConfig(AppSettings appSettings, LogLevel logLevel = LogLevel.Information)
    {
        var appSettingsText = appSettings?.ToString();
        Console.WriteLine(appSettingsText);
        // appSettingsText?.Split("\r\n").ToList().ForEach(line => _logger?.Log(logLevel, line));
    }

}

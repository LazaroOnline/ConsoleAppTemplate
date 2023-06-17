namespace ConsoleAppTemplate;

public class Program
{
	private IConfiguration Configuration;
	
	private static Dictionary<string, string> CommandSwitchMapDictionary = new Dictionary<string, string>
	{
        // Example:
        // { "-D",   "Directory" },
        // { "-Dir", "Directory" },
    };

	static async Task Main(string[] args)
	{
		var program = new Program();
		await program.CreateHostBuilder(args).Build().RunAsync();
	}

    public const string APPSETTINGS_FILENAME = "AppSettings.json";

    private IHostBuilder CreateHostBuilder(string[] args) =>
		new HostBuilder()
			.ConfigureAppConfiguration((hostContext, configApp) =>
			{
				Configuration = configApp
					.AddJsonFile(APPSETTINGS_FILENAME, true, true)
					.AddCommandLine(args, CommandSwitchMapDictionary)
					.Build();
			})
			.ConfigureLogging((hostContext, logging) =>
			{
				logging
					.AddConfiguration(hostContext.Configuration.GetSection("Logging"))
					//.SetMinimumLevel(LogLevel.Trace)
					//.AddConsole()
                    .AddDebug()
                    .AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = false;
                        options.SingleLine = true;
                        options.TimestampFormat = "HH:mm:ss ";
                    })
				;
			})
			.ConfigureServices((hostContext, services) =>
			{
				services.AddOptions<AppSettings>().Bind(Configuration).ValidateDataAnnotations();
				services.AddHostedService<App>();
				//services.AddTransient<Workers.MainWorker>();
				services.RegisterServicesForDependencyInjection();
			})
			.UseConsoleLifetime();
}

public static class Extensions
{
	public static IServiceCollection RegisterServicesForDependencyInjection(this IServiceCollection services)
	{
		return services
			.RegisterServicesForDependencyInjection<Program>()
		//	.RegisterServicesForDependencyInjection<OtherClassFromOtherAssembly>()
		;
	}

	
	public static IServiceCollection RegisterServicesForDependencyInjection<T>(this IServiceCollection services)
	{
		// Using Scrutor library:
		// https://andrewlock.net/using-scrutor-to-automatically-register-your-services-with-the-asp-net-core-di-container/
		return services.Scan(scan => scan
			//.FromCallingAssembly()
			//.FromAssemblyDependencies()
			//.FromApplicationDependencies()
			.FromAssemblyOf<T>()
				.AddClasses(publicOnly: true)
				//.AddClasses(classes => classes.InNamespaces(nameof(ConsoleAppTemplate)).Where(type => !(type is SomeSpecificClass)))
				.UsingRegistrationStrategy(RegistrationStrategy.Skip)
					//.AsSelf()
					//.AsMatchingInterface()
					.AsSelfWithInterfaces()
					.WithTransientLifetime());
	}
}

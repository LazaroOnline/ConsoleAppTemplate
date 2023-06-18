using NLog.Extensions.Logging;

namespace ConsoleAppTemplate;

public class Program
{
	public const string APPSETTINGS_FILENAME = "AppSettings.json";

	static async Task Main(string[] args)
    {
        try
        {
            var program = new Program();
            await program.CreateHostBuilder(args).Build().RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("FATAL ERROR:");
            Console.WriteLine(ex.ToString());
        }
	}

	private IHostBuilder CreateHostBuilder(string[] args) {
		IConfiguration configuration = null;
		return new HostBuilder()
			.ConfigureAppConfiguration((hostContext, configApp) =>
			{
				configuration = configApp
					.AddJsonFile(APPSETTINGS_FILENAME, true, true)
					.AddCommandLine(args, Parameters.SwitchMappings)
					.Build();
			})
			.ConfigureLogging((hostContext, logging) =>
			{
				logging
					.AddConfiguration(hostContext.Configuration.GetSection("Logging"))
					//.SetMinimumLevel(LogLevel.Trace)
					//.AddConsole()
					.AddDebug()
					.AddNLog(configuration)
					/*
					.AddSimpleConsole(options =>
					{
						options.IncludeScopes = false;
						options.SingleLine = true;
						options.TimestampFormat = "HH:mm:ss ";
					})
					*/
				;
			})
			.ConfigureServices((hostContext, services) =>
			{
				services.AddOptions<AppSettings>().Bind(configuration).ValidateDataAnnotations();
				services.AddHostedService<App>();
                services.AddTransient<AppArguments>(services => new AppArguments(args));
                services.RegisterServicesForDependencyInjection();
			})
			.UseConsoleLifetime();
	}
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

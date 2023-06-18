namespace IntegrationTests;

public class IntegrationTest : TestBase
{
    [Fact]
	public async Task Integration_Test1()
    {
        var appSettings = new Dictionary<string, string>() {
            { "-Url", "SomeValueE2E" },
            { $"{nameof(AppSettings.SomeConfigSection)}:{nameof(AppSettings.SomeConfigSection.SomeName)}", "SomeNameE2E" },
        };
        var mainWorkerMock = Substitute.For<IMainWorker>();

        var hostBuilder = GetHost(appSettings, (hostContext, services) => {
            services.RemoveServices<IMainWorker>();
            services.AddScoped<IMainWorker>((services) => mainWorkerMock);
        });
        var host = await hostBuilder.StartAsync();
        // TODO: add some logic in your tests:

        mainWorkerMock.Received(1).Main();
        Environment.ExitCode.Should().Be(App.ExitCodes.Success_0);
    }

    [Fact]
    public async Task Integration_Test_NoSetupChanges()
    {
        var appSettings = new Dictionary<string, string> {
            { "", "" },
        };
        var hostBuilder = GetHost(appSettings);
        var host = await hostBuilder.StartAsync();

        // TODO: add some logic in your tests:
        Environment.ExitCode.Should().Be(App.ExitCodes.Success_0);
    }

}

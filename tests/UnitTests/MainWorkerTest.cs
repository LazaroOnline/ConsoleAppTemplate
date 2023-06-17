using ConsoleAppTemplate;

namespace UnitTests;

public class MainWorkerTest
{
    [Fact]
    public void Test1()
    {
        // TODO: Change this test example to do something useful in the real project:
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization() { ConfigureMembers = true });

        // Mock example:
        // var dependencyMock = Substitute.For<IDependency>();
        // dependencyMock.SomeMethod(Arg.Any<int>(), Arg.Any<string>()).Returns(false);
        // fixture.Inject(dependencyMock);

        var sut = fixture.Create<MainWorker>();
        sut.Main();
        Assert.True(true);

        // Example assertion on mock calls:
        // dependencyMock.Received().SomeMethod(Arg.Any<int>(), Arg.Any<string>());
        // dependencyMock.Received(1).SomeMethod(Arg.Is<int>(x => x < 100), "some-string");
    }

    [Theory]
    [InlineData("something", "something")]
    public void Test2(string input, string expectedResult)
    {
        // TODO: Change this test example to do something useful in the real project:
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization() { ConfigureMembers = true });

        var appSettings = Substitute.For<AppSettings>();
        appSettings.SomeConfigSection = new SomeExampleConfigSection()
        {
            SomeInt = 2,
            SomeName = "Peter",
            SomeUrl = "https://some-test-url.com"
        };
        fixture.Inject(appSettings);

        var sut = fixture.Create<MainWorker>();
        sut.Main();

        // TODO: assert something useful in the real project:
        input.Should().Be(expectedResult);
    }
}

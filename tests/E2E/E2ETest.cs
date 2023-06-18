namespace E2ETests;

public class E2ETest : TestBase
{
    [Fact]
    public void E2E_Test_1()
    {
        var args = "-U some-e2e-test-url.com";
        var process = RunApp(args);
        try
        {
            // Add assertions here...
            Assert.True(true);
        }
        finally
        {
            process.Kill();
        }
    }

}

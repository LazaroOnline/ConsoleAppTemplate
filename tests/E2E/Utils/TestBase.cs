using System.Diagnostics;

namespace E2ETests.Utils;

public class TestBase
{
    public Process RunApp(string args = "")
    {
        var process = new Process();
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.FileName = "ConsoleAppTemplate.exe";
        process.StartInfo.Arguments = args;
        process.StartInfo.CreateNoWindow = false;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        process.StartInfo.UseShellExecute = true; // Set to false if you don't want to show the app console window.
        process.Start();

        return process;
    }
}

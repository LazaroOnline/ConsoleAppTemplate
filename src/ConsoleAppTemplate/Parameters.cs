using ConsoleAppTemplate;

namespace ConsoleAppTemplate;

public static class Parameters
{
    public static readonly string Version = GetAppVersion();
    public static readonly string VersionMessage = "ConsoleAppTemplate Version: " + Version;
    public static string GetHelpMessage() => $@"
ConsoleAppTemplate help:
Parameter list:
{string.Join("\r\n", Enum.GetValues(typeof(AppCommand)).Cast<AppCommand>().Select(e => " - " + e))}
{string.Join("\r\n", SwitchMappings.Select(p => $" -{p.Key}, --{p.Value}"))}
";

    public enum AppCommand
    {
        Help,
        Version,
    }

    public static readonly Dictionary<string, string> SwitchMappings = new()
    {
        // Parameter names are case in-sensitive:
        { "-N",   $"{nameof(AppSettings.SomeConfigSection)}:{nameof(AppSettings.SomeConfigSection.SomeName)}" },
        { "-U",   $"{nameof(AppSettings.SomeConfigSection)}:{nameof(AppSettings.SomeConfigSection.SomeUrl)}" },
        { "-Url", $"{nameof(AppSettings.SomeConfigSection)}:{nameof(AppSettings.SomeConfigSection.SomeUrl)}" },
    };

    public static string GetAppVersion()
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var assemblyVersion = assembly?.GetName()?.Version?.ToString();
        return assemblyVersion ?? "";
    }

    public static bool IsCommandArgument(string arg, AppCommand command)
    {
        var commandName = command.ToString().ToLower();
        var argName = arg.ToLower().TrimStart('-', '/', '\\').Trim();
        return argName == commandName;
    }

}

public class AppArguments
{
    public string[] Args { get; } = new string[0];
    public AppArguments(string[]? args = null)
    {
        Args = args ?? Args;
    }
}

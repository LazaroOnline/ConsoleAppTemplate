
# <img src="Icon.png" width="45"/> ConsoleAppTemplate


## Description

This is a template for console applications.


## How to use this template
- Clone this repository.
- Delete the `.git` folder from your local copy (it is a hidden folder in the root of the repo).
- Rename `ConsoleAppTemplate` with the new project name in all files, you can use `Replace in All files` editor command.
- Add logic to the `\Workers\MainWorker.cs` file in the `Main` method.
- Rewrite this `README.md` file content and remove this section.
- Edit the documentation in `/docs/` folder.

## Features
- Dependency injection.
- Asynchronous and object oriented (testable) `Program.cs` class.
- Logging with `Microsoft.Extensions.Logging` and NLog.
- Configuration in `AppSettings.json` using `Microsoft.Extensions.Configuration`.
- Configuration validation with `DataAnnotations`.
- Documentation template in `/docs` folder and `README.md`.
- Unit Test project template with `XUnit`, `FluentAssertions`, `AutoFixture` and `NSubstitute` (can be replaced by `Moq` if preferred). 

________________________________________________________________________________________________

# User's Guide

## Requirements
- [dotnet 7 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).


## How to install
- Download the latest binaries from the [releases section on GitHub](/releases).
- Or alternatively compile the project yourself by cloning this repo, optionally use the `/scripts/Build.ps1`.


## Configuration

Can be done in 2 ways (in order of preference):
- From the `command-line` parameters during the app invocation.
- Editing `AppSettings.json` file before running the tool.

The available parameters are:
- `-U`, `-Url`  `--SomeConfigSection:SomeUrl`: Optional. Some URL.  
- `--SomeConfigSection:SomeName`: Optional. Some name.  
...


### Command line
Config parameters can be set from the command line arguments as supported by 
[dotnet's Microsoft logging]( https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.commandlineconfigurationextensions.addcommandline?view=dotnet-plat-ext-7.0 ).  
Examples for the PowerShell console:
```ps1
# Example with full param names:
.\ConsoleAppTemplate.exe -SomeConfigSection:SomeUrl "someurl.com" --SomeConfigSection:SomeName "Some Name" 

# Example with short param names:
.\ConsoleAppTemplate.exe -U "someurl.com" -N "Some Name" 

# Example with full-path and start-process (opens in a new cmd window):
Start "C:\ConsoleAppTemplate.exe" -ArgumentList '-U "someurl.com" --N "Some Name" ' 

```


### AppSettings.json
Edit the optional `AppSettings.json` file located in the same folder as the app's executable, before running the app.  
These settings will be taken as the default values when a parameter is not specified.  
Example:
```json
{
  "SomeConfigSection": {
	"SomeUrl": "https://some-example.com",
	"SomeName": "Peter",
  }
}
```


## Extra Documentation
- [Developer's Guide](/docs/DevelopersGuide.md)
- [Functional Document](/docs/FunctionalDoc.md)


cd .\..\src
$publishFolder = ".\bin\Publish\"
# https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-publish
dotnet publish "ConsoleAppTemplate.csproj" -c "Release" -o $publishFolder /p:DebugType=None
# -r "win-x64"  -p:PublishSingleFile=true  --self-contained false

$version = (Get-Item "$publishFolder\ConsoleAppTemplate.exe").VersionInfo.FileVersion
# https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.archive/compress-archive?view=powershell-7.3
Compress-Archive -Path "$publishFolder\*" -DestinationPath ".\bin\ConsoleAppTemplate-v$version.zip" -Force

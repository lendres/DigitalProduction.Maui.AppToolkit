# Validation Native AOT smoke test

This Windows x64 application references the actual AppToolkit project, publishes with full
trimming and Native AOT, and runs MAUI-control checks on WinUI's application thread. It exits
automatically and writes `validation-smoke.txt` beside its executable, or to the path supplied
by `VALIDATION_SMOKE_REPORT`. A failure sets exit code 1 and records the exception.

From the repository root, with the .NET 10 MAUI workload and Visual Studio C++ Native AOT tools:

```powershell
dotnet publish src/ValidationSmoke/ValidationSmoke.csproj -c Release
$output = Join-Path $pwd 'src/ValidationSmoke/bin/Release/net10.0-windows10.0.19041.0/win-x64/publish'
$process = Start-Process -FilePath (Join-Path $output 'ValidationSmoke.exe') -WindowStyle Hidden -PassThru -Wait
$process.ExitCode
Get-Content (Join-Path $output 'validation-smoke.txt')
```

Expected: exit code 0 and a PASS report. Checks cover valid/invalid files, directories and
filenames; attachment to Entry, Editor, SearchBar and Label; automatic text changes; valid and
invalid styles; disabling automatic validation and forcing validation; detachment; and a
compiled explicit-source binding. It does not exercise native text input or focus interaction.

The behaviors and shared Shell foreground style produce no trimming/AOT diagnostics. Publishing
still reports warnings from the external `akgul.Maui.DataGrid` 4.0.6 dependency. These are not
suppressed; investigate them if they cause an application bug. This smoke test does not establish
whole-library AOT safety.
Apple and Android runtime tests must be run separately on supported hosts/devices.

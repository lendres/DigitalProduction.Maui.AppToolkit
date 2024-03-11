/*
Test runner application to run tests for specific platforms.
The instructions are located at:
https://github.com/shinyorg/xunit-maui
*/
using Microsoft.Extensions.Logging;
using Xunit.Runners.Maui;

namespace TestRunnerApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp() => MauiApp
		.CreateBuilder()
		.ConfigureTests(new TestOptions
		{
			Assemblies =
			{
				typeof(MauiProgram).Assembly
			}
		})
		.UseVisualRunner()
		.Build();
}
using CommunityToolkit.Maui.Behaviors;
using DigitalProduction.Maui.Behaviors;
using Microsoft.Maui.Controls;
using Microsoft.UI.Dispatching;

namespace ValidationSmoke;

/// <summary>Runs real MAUI-control checks after WinUI has initialized; exits with a report.</summary>
static class Program
{
	[STAThread]
	static void Main()
	{
		WinRT.ComWrappersSupport.InitializeComWrappers();
		Microsoft.UI.Xaml.Application.Start(parameters =>
		{
			SynchronizationContext.SetSynchronizationContext(
				new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread()));
			_ = new SmokeApplication();
		});
	}
}

sealed partial class SmokeApplication : Microsoft.UI.Xaml.Application
{
	protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
	{
		string report = Environment.GetEnvironmentVariable("VALIDATION_SMOKE_REPORT")
			?? System.IO.Path.Combine(AppContext.BaseDirectory, "validation-smoke.txt");
		try
		{
			await RunChecks();
			File.WriteAllText(report, "PASS: file, directory, filename, Entry/Editor/SearchBar/Label attachment, text changes, styles, flags, detachment, explicit Value binding.");
		}
		catch (Exception ex)
		{
			File.WriteAllText(report, "FAIL: " + ex);
			Environment.ExitCode = 1;
		}
		finally { Exit(); }
	}

	static async Task RunChecks()
	{
		string directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "validation-smoke-" + Guid.NewGuid());
		Directory.CreateDirectory(directory);
		string file = System.IO.Path.Combine(directory, "exists.txt");
		File.WriteAllText(file, "test");
		try
		{
			using var fileBehavior = new FileExistsValidationBehavior { Value = file };
			await fileBehavior.ForceValidate();
			Check(fileBehavior.IsValid && fileBehavior.Error == PathErrorType.None, "Existing file");
			fileBehavior.Value = file + ".missing";
			await fileBehavior.ForceValidate();
			Check(fileBehavior.IsNotValid && fileBehavior.Error == PathErrorType.FileNotFound, "Missing file");

			using var nameBehavior = new FileNameValidationBehavior { Value = file };
			await nameBehavior.ForceValidate();
			Check(nameBehavior.IsValid, "Valid filename");
			nameBehavior.Value = "";
			await nameBehavior.ForceValidate();
			Check(nameBehavior.IsNotValid, "Empty filename");

			VisualElement[] views = [new Entry(), new Editor(), new SearchBar(), new Label()];
			foreach (var view in views)
			{
				var text = view is InputView ? InputView.TextProperty : Label.TextProperty;
				view.SetValue(text, directory);
				var validStyle = new Style(view.GetType());
				var invalidStyle = new Style(view.GetType());
				using var behavior = new DirectoryExistsValidationBehavior
				{
					Flags = ValidationFlags.ValidateOnAttaching | ValidationFlags.ValidateOnValueChanged,
					ValidStyle = validStyle,
					InvalidStyle = invalidStyle
				};
				view.Behaviors.Add(behavior);
				Check(behavior.IsValid && behavior.Error == PathErrorType.None && view.Style == validStyle, "Attach " + view.GetType().Name);
				view.SetValue(text, directory + ".missing");
				Check(behavior.IsNotValid && behavior.Error == PathErrorType.DirectoryNotFound && view.Style == invalidStyle, "Change text");
				behavior.Flags = ValidationFlags.None;
				view.SetValue(text, directory);
				Check(behavior.IsNotValid, "Disabled automatic validation");
				await behavior.ForceValidate();
				Check(behavior.IsValid, "Forced validation");
				view.Behaviors.Remove(behavior);
				view.SetValue(text, directory + ".missing");
				Check(behavior.Value == directory, "Detach unsubscribes");
			}

			var entry = new Entry { Text = directory };
			using var explicitBehavior = new DirectoryExistsValidationBehavior { ValuePropertyName = "" };
			explicitBehavior.SetBinding(TrimSafeValidationBehavior<string>.ValueProperty,
				static (Entry source) => source.Text, source: entry);
			entry.Behaviors.Add(explicitBehavior);
			await explicitBehavior.ForceValidate();
			Check(explicitBehavior.IsValid, "Compiled binding");
			entry.Text = directory + ".missing";
			await explicitBehavior.ForceValidate();
			Check(explicitBehavior.IsNotValid, "Compiled binding updates");
			entry.Behaviors.Remove(explicitBehavior);
		}
		finally
		{
			File.Delete(file);
			Directory.Delete(directory);
		}
	}

	static void Check(bool condition, string scenario)
	{
		if (!condition)
			throw new InvalidOperationException(scenario);
	}
}

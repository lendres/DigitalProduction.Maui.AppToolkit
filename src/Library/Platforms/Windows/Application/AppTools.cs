using Microsoft.UI.Windowing;

namespace DigitalProduction.Maui.UI;

/// <summary>
/// 
/// </summary>
public static class AppTools
{
	#region Fields
	#endregion

	#region Properties
	/// <summary>Disable persistence during automated runs while still allowing restoration.</summary>
	public static bool SaveWindowPlacement { get; set; } = true;

	#endregion

	#region Methods

	public static AppWindow? GetAppWindow(MauiWinUIWindow window)
	{
		if (window == null)
		{
			return null;
		}
		var handle    = WinRT.Interop.WindowNative.GetWindowHandle(window);
		var id        = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
		var appWindow = AppWindow.GetFromWindowId(id);
		return appWindow;
	}

	public static void SaveWindowState(OverlappedPresenterState state, string name)
	{
		SavePreference(name+".Position.State", (int)state);
	}

	public static OverlappedPresenterState GetWindowState(string name)
	{
		int state = GetPreference(name+".Position.State", (int)OverlappedPresenterState.Maximized);
		return (OverlappedPresenterState)state;
	}

	public static void SaveWindowPosition(Window window, string name)
	{
		SavePreference(name+".Position.X", window.X);
		SavePreference(name+".Position.Y", window.Y);
	}

	public static void SaveWindowSize(Window window, string name)
	{
		SavePreference(name+".Position.Width", window.Width);
		SavePreference(name+".Position.Height", window.Height);
	}

	public static void RestoreWindowPosition(Window window, string name, bool ensureOnScreen)
	{
		window.X = GetPreference(name+".Position.X", window.X);
		window.Y = GetPreference(name+".Position.Y", window.Y);

		if (ensureOnScreen && (window.X < 0 || window.Y < 0))
		{
			window.X = 20;
			window.Y = 20;
		}

		window.Width	= GetPreference(name+".Position.Width", window.Width);
		window.Height	= GetPreference(name+".Position.Height", window.Height);
	}

	private static void SavePreference<T>(string key, T value)
	{
		if (!SaveWindowPlacement)
		{
			return;
		}
		try
		{
			PreferenceStorage.Default.Set(key, value);
		}
		catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or System.Text.Json.JsonException or System.Runtime.InteropServices.COMException)
		{
			System.Diagnostics.Trace.TraceWarning("Could not save window placement: {0}", exception);
		}
	}

	private static T GetPreference<T>(string key, T defaultValue)
	{
		try
		{
			return PreferenceStorage.Default.Get(key, defaultValue);
		}
		catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or System.Text.Json.JsonException or FormatException or System.Runtime.InteropServices.COMException)
		{
			System.Diagnostics.Trace.TraceWarning("Could not restore window placement: {0}", exception);
			return defaultValue;
		}
	}
	#endregion

} // End class.

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
		Preferences.Default.Set(name+".Position.State", (int)state);
	}

	public static OverlappedPresenterState GetWindowState(string name)
	{
		int state = Preferences.Default.Get(name+".Position.State", (int)OverlappedPresenterState.Maximized);
		return (OverlappedPresenterState)state;
	}

	public static void SaveWindowPosition(Window window, string name)
	{
		Preferences.Default.Set(name+".Position.X", window.X);
		Preferences.Default.Set(name+".Position.Y", window.Y);
	}

	public static void SaveWindowSize(Window window, string name)
	{
		Preferences.Default.Set(name+".Position.Width", window.Width);
		Preferences.Default.Set(name+".Position.Height", window.Height);
	}

	public static void RestoreWindowPosition(Window window, string name, bool ensureOnScreen = true)
	{
		window.X        = Preferences.Default.Get(name+".Position.X", window.X);
		window.Y        = Preferences.Default.Get(name+".Position.Y", window.Y);

		if (ensureOnScreen && window.X < 0)
		{
			window.X = 90;
		}

		if (ensureOnScreen && window.Y < 0)
		{
			window.Y = 40;
		}

		window.Width    = Preferences.Default.Get(name+".Position.Width", window.Width);
		window.Height   = Preferences.Default.Get(name+".Position.Height", window.Height);
	}

	#endregion

} // End class.
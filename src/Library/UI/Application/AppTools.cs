using DigitalProduction.XML.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
#if WINDOWS
using Microsoft.UI.Windowing;
#endif

namespace DigitalProduction.UI;

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


#if WINDOWS
	public static AppWindow GetAppWindow(MauiWinUIWindow window)
	{
		if (window == null)
		{
			return null;
		}
		var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
		var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
		var appWindow = AppWindow.GetFromWindowId(id);
		return appWindow;
	}

	public static OverlappedPresenterState GetWindowState(string name)
	{
		int state = Preferences.Default.Get(name+".Position.State", (int)OverlappedPresenterState.Maximized);
		return (OverlappedPresenterState)state;
	}

	public static void RestoreWindowPosition(Window window, string name)
	{
		window.X        = Preferences.Default.Get(name+".Position.X", window.X);
		window.Y        = Preferences.Default.Get(name+".Position.Y", window.Y);
		window.Width    = Preferences.Default.Get(name+".Position.Width", window.Width);
		window.Height   = Preferences.Default.Get(name+".Position.Height", window.Height);
	}

#endif



	#endregion

} // End class.
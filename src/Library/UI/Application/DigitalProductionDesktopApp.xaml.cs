//#define WINDOWS

using DigitalProduction.XML.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace DigitalProduction.Desktop;

/// <summary>
/// 
/// </summary>
public partial class DigitalProductionDesktopApp : Application
{
	#region Fields

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public DigitalProductionDesktopApp()
	{
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	protected override Window CreateWindow(Microsoft.Maui.IActivationState activationState)
	{
		Window window   = base.CreateWindow(activationState);

		//RestoreWindowPosition(window, "MainWindow");
		//window.SizeChanged += WindowSizeChanged;

		return window;
	}

	private void WindowSizeChanged(object sender, EventArgs eventArgs)
	{
		Window window	= (Window)sender;
		SaveWindowPosition(window, "MainWindow");
	}

	private static void RestoreWindowPosition(Window window, string name)
	{
		window.X        = Preferences.Default.Get(name+".Position.X", window.X);
		window.Y        = Preferences.Default.Get(name+".Position.Y", window.Y);
		window.Width    = Preferences.Default.Get(name+".Position.Width", window.Width);
		window.Height   = Preferences.Default.Get(name+".Position.Height", window.Height);
	}

	private static void SaveWindowPosition(Window window, string name)
	{
		Preferences.Default.Set(name+".Position.X", window.X);
		Preferences.Default.Set(name+".Position.Y", window.Y);
		Preferences.Default.Set(name+".Position.Width", window.Width);
		Preferences.Default.Set(name+".Position.Height", window.Height);
	}

#if WINDOWS
    protected Microsoft.UI.Windowing.AppWindow GetAppWindow(MauiWinUIWindow mauiWinUIWindow)
    {
		//mauiWinUIWindow.GetWindow();
        var handle = WinRT.Interop.WindowNative.GetWindowHandle(mauiWinUIWindow);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
    }
#endif

	private void GetRestore(Window window)
	{
#if WINDOWS
    //    Microsoft.UI.Windowing.AppWindow appWindow = GetAppWindow(window);

    //    switch (appWindow.Presenter)
    //    {
    //        case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
    //            if (overlappedPresenter.State == Microsoft.UI.Windowing.OverlappedPresenterState.Maximized)
				//{
				//	int breakhere = 1;
				//}
    //            //{
    //            //    overlappedPresenter.SetBorderAndTitleBar(true, true);
    //            //    overlappedPresenter.Restore();
    //            //}
    //            //else
    //            //{
    //            //    overlappedPresenter.SetBorderAndTitleBar(false, false);
    //            //    overlappedPresenter.Maximize();
    //            //}
    //            break;
    //    }
#endif
	}

	#endregion

} // End class.
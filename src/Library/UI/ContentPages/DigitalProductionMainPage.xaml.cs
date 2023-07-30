namespace DigitalProduction.Controls;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
#if WINDOWS
using Microsoft.UI.Windowing;
#endif

public partial class DigitalProductionMainPage : ContentPage
{
	#region Fields

	#endregion

	#region Construction

	public DigitalProductionMainPage()
	{
		InitializeComponent();

		this.Loaded += this.OnLoaded;
		
	}

	private void OnLoaded(object sender, EventArgs eventArgs)
	{
#if WINDOWS
		AppWindow appWindow = this.GetAppWindow();
		if (appWindow == null)
		{
			return;
		}

		switch (appWindow.Presenter)
		{
			case OverlappedPresenter overLappedPresenter:
				Window parentWindow = GetParentWindow();
				RestoreWindowPosition(parentWindow, "MainWindow");

				OverlappedPresenterState state = GetWindowState("MainWindow");
				if (state == OverlappedPresenterState.Maximized)
				{
					overLappedPresenter.Maximize();
				}
				else
				{
					overLappedPresenter.Restore();
				}

				// Add the event handler only after setting the initial position.  Otherwise, it will overwrite the
				// saved values and we will not get restoration of the correct position.
				parentWindow.SizeChanged += this.OnSizeChanged;
				break;

		}
#endif
	}

	private void OnSizeChanged(object sender, EventArgs eventArgs)
	{
#if WINDOWS
		AppWindow appWindow = this.GetAppWindow();
		if (appWindow == null)
		{
			return;
		}

		switch (appWindow.Presenter)
		{
			case OverlappedPresenter overLappedPresenter:
				OverlappedPresenterState state = overLappedPresenter.State;
				SaveWindowState(GetParentWindow(), "MainWindow", state);
				break;
		}
#endif
	}

#if WINDOWS
	private void RestoreWindowPosition(Window window, string name)
	{
		window.X        = Preferences.Default.Get(name+".Position.X", window.X);
		window.Y        = Preferences.Default.Get(name+".Position.Y", window.Y);
		window.Width    = Preferences.Default.Get(name+".Position.Width", window.Width);
		window.Height   = Preferences.Default.Get(name+".Position.Height", window.Height);
	}

	private void SaveWindowState(Window window, string name, OverlappedPresenterState state)
	{
		Preferences.Default.Set(name+".Position.State", (int)state);

		if (state == OverlappedPresenterState.Restored)
		{
			// Only save the postion and size in the restored state.  Otherwise we are just save and restoring the maximized
			// size which is not what we want.
			Preferences.Default.Set(name+".Position.X", window.X);
			Preferences.Default.Set(name+".Position.Y", window.Y);
			Preferences.Default.Set(name+".Position.Width", window.Width);
			Preferences.Default.Set(name+".Position.Height", window.Height);
		}
	}

	private OverlappedPresenterState GetWindowState(string name)
	{
		int state = Preferences.Default.Get(name+".Position.State", (int)OverlappedPresenterState.Maximized);
		return (OverlappedPresenterState)state;
	}

    protected AppWindow GetAppWindow()
    {
		MauiWinUIWindow mauiWinUIWindow = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;
		if (mauiWinUIWindow == null)
		{
			return null;
		}
        var handle = WinRT.Interop.WindowNative.GetWindowHandle(mauiWinUIWindow);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        var appWindow = AppWindow.GetFromWindowId(id);
        return appWindow;
    }
#endif

	#endregion
}
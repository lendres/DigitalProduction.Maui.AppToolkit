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
		AppWindow appWindow = GetAppWindow();
		//if (appWindow == null)
		//{
		//	return;
		//}

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

    protected AppWindow GetAppWindow()
    {
		MauiWinUIWindow mauiWinUIWindow = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;
		return DigitalProduction.UI.AppTools.GetAppWindow(mauiWinUIWindow);
    }
#endif

	#endregion
}
using Microsoft.UI.Windowing;

namespace DigitalProduction.Controls;

public partial class DigitalProductionMainPage
{
	#region Properties

	#endregion

	#region Methods

	partial void InstallOnLoaded(object? sender, EventArgs eventArgs)
	{
		AppWindow? appWindow = this.GetAppWindow();
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
	}

	private void OnSizeChanged(object? sender, EventArgs eventArgs)
	{
		AppWindow? appWindow = GetAppWindow();

		switch (appWindow?.Presenter)
		{
			case OverlappedPresenter overLappedPresenter:
				DigitalProduction.UI.AppTools.SaveWindowState(overLappedPresenter.State, "MainWindow");

				if (overLappedPresenter.State == OverlappedPresenterState.Restored)
				{
					// Only save the postion and size in the restored state.  Otherwise we are just save and restoring the maximized
					// size which is not what we want.
					DigitalProduction.UI.AppTools.SaveWindowPosition(GetParentWindow(), "MainWindow");
				}
				break;
		}
	}

	protected AppWindow? GetAppWindow()
	{
		if (GetParentWindow().Handler.PlatformView is MauiWinUIWindow mauiWinUIWindow)
		{
			return DigitalProduction.UI.AppTools.GetAppWindow(mauiWinUIWindow);
		}
		else
		{
			return null;
		}
	}

	#endregion
}
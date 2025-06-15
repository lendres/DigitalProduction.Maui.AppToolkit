using Microsoft.UI.Windowing;

namespace DigitalProduction.Maui.Controls;

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
				parentWindow.SizeChanged		+= OnSizeChanged;
				parentWindow.PropertyChanged	+= OnPropertyChanged;
				break;
		}
	}

	private void OnSizeChanged(object? sender, EventArgs eventArgs)
	{
		if (IsRestoredPresenter())
		{ 
			// Only save the postion and size in the restored state.  Otherwise we are just save and restoring the maximized
			// size which is not what we want.
			DigitalProduction.Maui.UI.AppTools.SaveWindowSize(GetParentWindow(), "MainWindow");
		}
	}

	private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs eventArgs)
	{
		if ((eventArgs.PropertyName == "X" || eventArgs.PropertyName == "Y") && IsRestoredPresenter())
		{
			// Only save the postion and size in the restored state.  Otherwise we are just save and restoring the maximized
			// size which is not what we want.
			DigitalProduction.Maui.UI.AppTools.SaveWindowPosition(GetParentWindow(), "MainWindow");
		}
	}

	private bool IsRestoredPresenter()
	{
		AppWindow? appWindow = GetAppWindow();

		switch (appWindow?.Presenter)
		{
			case OverlappedPresenter overLappedPresenter:
				if (overLappedPresenter.State == OverlappedPresenterState.Restored)
				{
					return true;
				}
				break;
		}

		return false;
	}

	protected AppWindow? GetAppWindow()
	{
		if (GetParentWindow()?.Handler?.PlatformView is MauiWinUIWindow mauiWinUIWindow)
		{
			return DigitalProduction.Maui.UI.AppTools.GetAppWindow(mauiWinUIWindow);
		}
		else
		{
			return null;
		}
	}

	#endregion
}
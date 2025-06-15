using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using Microsoft.UI;
using Microsoft.UI.Windowing;

namespace DigitalProduction.Maui.UI;

public static partial class LifecycleEventsInstaller
{
	static partial void PlatformConfigureLifecycleEvents(MauiAppBuilder builder, LifecycleOptions? lifecycleOptions)
	{
		// If no options were provided, we default them.
		if (lifecycleOptions == null)
		{
			lifecycleOptions = new LifecycleOptions();
		}

		builder.ConfigureLifecycleEvents(events =>
		{
			// We want to set the restored position, size, and state (restored/maximized) here before the window is created.
			// If we do it in MainPage, it will be displayed in one location, then get its size and position set, which
			// will move the window.

			events.AddWindows(windowsLifecycleBuilder =>
			{
				windowsLifecycleBuilder.OnWindowCreated(window =>
				{
					// Microsoft.UI.Xaml.Window window.
					window.ExtendsContentIntoTitleBar = false;

					AppWindow? appWindow = DigitalProduction.Maui.UI.AppTools.GetAppWindow((MauiWinUIWindow)window);

					SetupPositionSavingAndRestoration(lifecycleOptions, window, appWindow);
					SetWindowTitle(lifecycleOptions, window);
					SetDisableMaximizedWindow(lifecycleOptions, appWindow);
				});
			});
		});
	}

	private static void SetupPositionSavingAndRestoration(LifecycleOptions lifecycleOptions, Microsoft.UI.Xaml.Window window, AppWindow? appWindow)
	{
		switch (appWindow?.Presenter)
		{
			case Microsoft.UI.Windowing.OverlappedPresenter overLappedPresenter:
				MauiWinUIWindow winUIWindow = (MauiWinUIWindow)window;

				if (winUIWindow.GetWindow() is Window mauiWindow)
				{
					// Set the restored position.
					DigitalProduction.Maui.UI.AppTools.RestoreWindowPosition(mauiWindow, "MainWindow", lifecycleOptions.EnsureOnScreen);

					OverlappedPresenterState state = DigitalProduction.Maui.UI.AppTools.GetWindowState("MainWindow");
					if (state == OverlappedPresenterState.Maximized)
					{
						overLappedPresenter.Maximize();
					}
					else
					{
						overLappedPresenter.Restore();
					}
				}
				break;
		}
	}

	private static void SetWindowTitle(LifecycleOptions lifecycleOptions, Microsoft.UI.Xaml.Window window)
	{
		if (lifecycleOptions.WindowTitle != string.Empty)
		{
			window.Title = lifecycleOptions.WindowTitle;
		}
	}

	private static void SetDisableMaximizedWindow(LifecycleOptions lifecycleOptions, AppWindow? appWindow)
	{
		if (lifecycleOptions.DisableMaximizeButton)
		{
			switch (appWindow?.Presenter)
			{
				case OverlappedPresenter overlappedPresenter:
					overlappedPresenter.IsMaximizable = false;
					break;
			}
		}
	}
}
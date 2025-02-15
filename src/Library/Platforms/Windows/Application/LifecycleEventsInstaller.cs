using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;

namespace DigitalProduction.Maui.UI;

public static partial class LifecycleEventsInstaller
{
	static partial void PlatformConfigureLifecycleEvents(MauiAppBuilder builder, bool ensureOnScreen = true)
	{
		builder.ConfigureLifecycleEvents(events =>
		{
			// We want to set the restored position, size, and state (restored/maximized) here before the window is created.
			// If we do it in MainPage, it will be displayed in one location, then get its size and position set, which
			// will move the window.

			events.AddWindows(windowsLifecycleBuilder =>
			{
				windowsLifecycleBuilder.OnWindowCreated(window =>
				{
					// Microsoft.UI.Xaml.Window window
					window.ExtendsContentIntoTitleBar = false;

					AppWindow? appWindow = DigitalProduction.Maui.UI.AppTools.GetAppWindow((MauiWinUIWindow)window);

					switch (appWindow?.Presenter)
					{
						case Microsoft.UI.Windowing.OverlappedPresenter overLappedPresenter:
							MauiWinUIWindow winUIWindow	= (MauiWinUIWindow)window;

							if (winUIWindow.GetWindow() is Window mauiWindow)
							{
								// Set the restored position.
								DigitalProduction.Maui.UI.AppTools.RestoreWindowPosition(mauiWindow, "MainWindow", ensureOnScreen);

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
				});
			});
		});
	}
}
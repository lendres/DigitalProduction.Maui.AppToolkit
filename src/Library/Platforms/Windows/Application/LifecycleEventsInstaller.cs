using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;

namespace DigitalProduction.Maui.UI;

public static partial class LifecycleEventsInstaller
{
	static partial void PlatformConfigureLifecycleEvents(MauiAppBuilder builder, LifecycleOptions? lifecycleOptions)
	{
		// If no options were provided, we default them.
		lifecycleOptions ??= new LifecycleOptions();

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
					SetupPromptToSave(lifecycleOptions, window, appWindow);
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

	private static void SetupPromptToSave(LifecycleOptions lifecycleOptions, Microsoft.UI.Xaml.Window window, AppWindow? appWindow)
	{
		if (!lifecycleOptions.PromptToSaveBeforeClose || appWindow is null)
		{
			return;
		}

		bool isProgrammaticClose = false;

		// AppWindow.Closing supports canceling the close
		appWindow!.Closing += async (sender, eventArgs) =>
		{
            if (isProgrammaticClose)
            {
                return;
            }

            eventArgs.Cancel = true;

            CloseChoice closeChoice = await ShowCloseDialogAsync(window);

            switch (closeChoice)
            {
                case CloseChoice.SaveAndExit:
                    bool saveSucceeded = await SaveBeforeExitAsync();
                    if (!saveSucceeded)
                    {
                        return;
                    }

                    isProgrammaticClose = true;
                    window.Close();
                    break;

                case CloseChoice.ExitWithoutSaving:
                    isProgrammaticClose = true;
                    window.Close();
                    break;

                case CloseChoice.Cancel:
                default:
                    break;
            }
		};
	}

    private static async Task<CloseChoice> ShowCloseDialogAsync(Microsoft.UI.Xaml.Window window)
    {
        Microsoft.UI.Xaml.FrameworkElement rootElement = (Microsoft.UI.Xaml.FrameworkElement)window.Content;

        Microsoft.UI.Xaml.Controls.ContentDialog dialog = new()
        {
            Title = "Unsaved changes",
            Content = "What would you like to do before closing the application?",
            PrimaryButtonText = "Save and Exit",
            SecondaryButtonText = "Exit without Saving",
            CloseButtonText = "Cancel",
            DefaultButton = Microsoft.UI.Xaml.Controls.ContentDialogButton.Primary,
            XamlRoot = rootElement.XamlRoot
        };

        Microsoft.UI.Xaml.Controls.ContentDialogResult result = await dialog.ShowAsync();

        return result switch
        {
            Microsoft.UI.Xaml.Controls.ContentDialogResult.Primary		=> CloseChoice.SaveAndExit,
            Microsoft.UI.Xaml.Controls.ContentDialogResult.Secondary	=> CloseChoice.ExitWithoutSaving,
            _															=> CloseChoice.Cancel
        };
    }

    private static async Task<bool> SaveBeforeExitAsync()
    {
        try
        {
            // Put your real save logic here.
            // Example:
            // await SomeService.Current.SaveAllAsync();

            await Task.CompletedTask;
            return true;
        }
        catch (Exception)
        {
            // Optional: log the exception or show another dialog.
            return false;
        }
    }

    private enum CloseChoice
    {
        SaveAndExit,
        ExitWithoutSaving,
        Cancel
    }
}
using DigitalProduction.Maui.Services;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;

namespace DigitalProduction.Maui.UI;

public static partial class LifecycleEventsInstaller
{
	#region Main Installer

	/// <summary>
	/// Configures platform-specific application lifecycle events for the Windows platform using the specified builder and
	/// lifecycle options.
	/// </summary>
	/// <remarks>This method sets up event handlers for window creation, including restoring window position, size,
	/// and state, as well as configuring additional window behaviors. It should be called during application startup to
	/// ensure correct window lifecycle management.</remarks>
	/// <param name="builder">The application builder used to configure lifecycle events.</param>
	/// <param name="lifecycleOptions">The options that control lifecycle event behavior. If null, default options are used.</param>
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

					// Installers for each feature that needs to hook into window events..
					SetupPositionSavingAndRestoration(lifecycleOptions, window, appWindow);
					SetWindowTitle(lifecycleOptions, window);
					SetDisableMaximizedWindow(lifecycleOptions, appWindow);
					SetupPromptToSave(lifecycleOptions, window, appWindow);
				});
			});
		});
	}

	#endregion

	#region Window

	/// <summary>
	/// Configures the saving and restoration of window position and state for the specified window using the provided
	/// lifecycle options.
	/// </summary>
	/// <remarks>This method is intended to be called during window initialization to ensure that the window's
	/// position and state are preserved across application sessions. Only windows with an OverlappedPresenter are
	/// supported.</remarks>
	/// <param name="lifecycleOptions">The lifecycle options that determine how window position and state should be managed, including whether the window
	/// should be ensured to appear on screen.</param>
	/// <param name="window">The WinUI window instance whose position and state will be saved and restored.</param>
	/// <param name="appWindow">The optional AppWindow associated with the window. If null, position and state restoration is not performed.</param>
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

	/// <summary>
	/// Sets the title of the specified window based on the provided lifecycle options.
	/// </summary>
	/// <remarks>If the WindowTitle property in lifecycleOptions is an empty string, the window's title remains
	/// unchanged.</remarks>
	/// <param name="lifecycleOptions">The lifecycle options containing the window title to apply. The WindowTitle property is used to determine the new
	/// title.</param>
	/// <param name="window">The window whose title is to be set.</param>
	private static void SetWindowTitle(LifecycleOptions lifecycleOptions, Microsoft.UI.Xaml.Window window)
	{
		if (lifecycleOptions.WindowTitle != string.Empty)
		{
			window.Title = lifecycleOptions.WindowTitle;
		}
	}

	/// <summary>
	/// Disables the maximize button for the specified window if the lifecycle options indicate that maximizing should be
	/// disabled.
	/// </summary>
	/// <remarks>This method only affects windows with an overlapped presenter. Other window types are not
	/// modified.</remarks>
	/// <param name="lifecycleOptions">The lifecycle options that determine whether the maximize button should be disabled. The maximize button is
	/// disabled if <paramref name="lifecycleOptions.DisableMaximizeButton"/> is set to <see langword="true"/>.</param>
	/// <param name="appWindow">The window whose maximize button will be disabled if applicable. If <paramref name="appWindow"/> is <see
	/// langword="null"/>, no action is taken.</param>
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

	#endregion

	#region Prompt to Save Before Close

	private static void SetupPromptToSave(LifecycleOptions lifecycleOptions, Microsoft.UI.Xaml.Window window, AppWindow? appWindow)
	{
		if (!lifecycleOptions.PromptToSaveBeforeClose || appWindow is null)
		{
			return;
		}

		bool isProgrammaticClose	= false;
		ISaveService saveService	= IPlatformApplication.Current!.Services.GetRequiredService<ISaveService>();

		// AppWindow.Closing supports canceling the close
		appWindow!.Closing += async (sender, eventArgs) =>
		{
            if (isProgrammaticClose)
            {
                return;
            }

            eventArgs.Cancel = true;

            SaveChoice closeChoice = await saveService.PromptSaveChangesAsync();

			switch (closeChoice)
            {
                case SaveChoice.ContinueWithoutSaving:
                case SaveChoice.SaveAndContinue:
                case SaveChoice.SavingNotRequired:
                    isProgrammaticClose = true;
                    window.Close();
                    break;

                case SaveChoice.Cancel:
                default:
                    break;
            }
		};
	}

	#endregion
}
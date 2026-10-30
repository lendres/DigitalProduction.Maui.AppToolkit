using CommunityToolkit.Maui.Views;

namespace DigitalProduction.Maui.Views;

public partial class PopupView : Popup<bool>
{
	#region Construction

	public PopupView()
	{
		// Override the Toolkit's local defaults here because they take precedence over the implicit Popup style. This is a bug
		// reported to the Toolkit team, but it has not been fixed yet. The workaround is to clear the local values for these
		// properties so that the implicit style can take effect.
		// CommunityToolkit.Maui issue:
		// https://github.com/CommunityToolkit/Maui/issues/2747
		// DigitalProduction.Maui.AppToolkit issue:
		// https://github.com/lendres/DigitalProduction.Maui.AppToolkit/issues/133
		// Once fixed, the two lines below can be removed and the implicit style will take effect.
		ClearValue(BackgroundColorProperty);
		ClearValue(PaddingProperty);
	}

	#endregion

	#region Events

	protected void OnLoadedSetFocus(object? sender, EventArgs eventArgs)
	{
		if (sender is VisualElement control)
		{
			control.Focus();
		}
	}

	async protected virtual void OnSaveButtonClicked(object? sender, EventArgs eventArgs)
	{
		CancellationTokenSource cancelationTokenSource = new(TimeSpan.FromSeconds(5));
		await CloseAsync(true, cancelationTokenSource.Token);
	}

	async protected void OnCancelButtonClicked(object? sender, EventArgs eventArgs)
	{
		CancellationTokenSource cancelationTokenSource = new(TimeSpan.FromSeconds(5));
		await CloseAsync(false, cancelationTokenSource.Token);
	}

	#endregion
}
using CommunityToolkit.Maui.Views;

namespace DigitalProduction.Maui.Views;

public partial class PopupView : Popup<bool>
{
	#region Construction

	public PopupView()
	{
		// The content supplies its own border and padding. Override the Toolkit's local
		// defaults here because they take precedence over the implicit Popup style.
		BackgroundColor	= Colors.Transparent;
		Padding			= 0;
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
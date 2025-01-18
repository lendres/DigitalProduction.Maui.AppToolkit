using CommunityToolkit.Maui.Views;

namespace DigitalProduction.Maui.Views;

public partial class PopupView : Popup
{
	#region Construction

	public PopupView()
	{
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
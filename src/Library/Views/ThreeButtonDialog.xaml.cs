using CommunityToolkit.Maui.Views;
using DigitalProduction.Maui.Services;

namespace DigitalProduction.Maui.Views;

public partial class ThreeButtonDialog : Popup
{
	public ThreeButtonDialog()
	{
		InitializeComponent();
	}

	private void OnSaveClicked(object? sender, EventArgs e)
	{
		Close(SaveChoice.SaveAndContinue);
	}

	private void OnDontSaveClicked(object? sender, EventArgs e)
	{
		Close(SaveChoice.ContinueWithoutSaving);
	}

	private void OnCancelClicked(object? sender, EventArgs e)
	{
		Close(SaveChoice.Cancel);
	}
}
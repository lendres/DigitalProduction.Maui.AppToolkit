using CommunityToolkit.Maui.Views;
using DigitalProduction.Maui.Services;

namespace DigitalProduction.Maui.Views;

public partial class ThreeButtonView : Popup
{
	#region Construction

	public ThreeButtonView()
	{
		InitializeComponent();
	}

	#endregion

	#region Properties

	public string Title
	{
		get => _titleLabel.Text;
		set => _titleLabel.Text = value;
	}

	public SaveChoice Button1Option { get; set; } = SaveChoice.SaveAndContinue;

	public SaveChoice Button2Option { get; set; } = SaveChoice.ContinueWithoutSaving;

	public SaveChoice Button3Option { get; set; } = SaveChoice.Cancel;

	#endregion

	#region Button Click Handlers

	private void OnButton1Clicked(object? sender, EventArgs e)
	{
		Close(Button1Option);
	}

	private void OnButton2Clicked(object? sender, EventArgs e)
	{
		Close(Button2Option);
	}

	private void OnButton3Clicked(object? sender, EventArgs e)
	{
		Close(Button3Option);
	}

	#endregion
}
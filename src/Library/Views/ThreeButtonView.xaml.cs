using CommunityToolkit.Maui.Views;

namespace DigitalProduction.Maui.Views;

public partial class ThreeButtonView : Popup
{
	#region Fields
	#endregion

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

	public string Button1Text { get; set; } = "Button 1";

	public string Button2Text { get; set; } = "Button 2";

	public string Button3Text { get; set; } = "Button 3";

	public object Button1Value { get; set; } = ButtonChoice.Button1;

	public object Button2Value { get; set; } = ButtonChoice.Button2;

	public object Button3Value { get; set; } = ButtonChoice.Button3;
	#endregion

	#region Button Click Handlers

	private void OnButton1Clicked(object? sender, EventArgs e)
	{
		Close(Button1Value);
	}

	private void OnButton2Clicked(object? sender, EventArgs e)
	{
		Close(Button2Value);
	}

	private void OnButton3Clicked(object? sender, EventArgs e)
	{
		Close(Button3Value);
	}

	#endregion

	//private void SetButtonText(T value, Button button)
	//{
	//	button.Text = DigitalProduction.Reflection.Attributes.GetAttribute<ControlTextAttribute>(value)?.Text;
	//}
}
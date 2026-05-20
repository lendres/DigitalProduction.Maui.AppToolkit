using CommunityToolkit.Maui.Views;
using DigitalProduction.Maui.Attributes;

namespace DigitalProduction.Maui.Views;

public class ThreeButtonView<T> : Popup where T : Enum
{
	#region Fields

	public T _button1Value = default!;
	public T _button2Value = default!;
	public T _button3Value = default!;

	private readonly Label _titleLabel = new()
	{
		Text				= "Select an Option",
		StyleClass			= ["Title"],
		HorizontalOptions	= LayoutOptions.Start
	};

	private readonly Button _button1 = CreateButton("Button 1");
	private readonly Button _button2 = CreateButton("Button 2");
	private readonly Button _button3 = CreateButton("Button 3");

	#endregion

	#region Construction

	public ThreeButtonView()
	{
		CanBeDismissedByTappingOutsideOfPopup = false;

		Content = new Border
		{
			Padding			= 20,
			StrokeThickness	= 1,
			BackgroundColor	= Application.Current?.Resources.TryGetValue("BackgroundColor", out object backgroundColor) == true ? (Color)backgroundColor : Colors.Transparent,

			Content = new VerticalStackLayout
			{
				Spacing = 16,
				HorizontalOptions = LayoutOptions.Fill,

				Children =
				{
					_titleLabel,
					new Label {	Text = "Do you want to save the changes?" },

					new VerticalStackLayout
					{
						Spacing = 10,
						HorizontalOptions = LayoutOptions.Fill,

						Children = { _button1, _button2, _button3 }
					}
				}
			}
		};

		_button1.Clicked += OnButton1Clicked;
		_button2.Clicked += OnButton2Clicked;
		_button3.Clicked += OnButton3Clicked;
	}

	#endregion

	public string Title
	{
		get => _titleLabel.Text;
		set => _titleLabel.Text = value;
	}

	public string Message { get; set; } = "Do you want to save the changes?";

	public T Button1Value
	{
		get => _button1Value;
		set
		{
			_button1Value = value;
			SetButtonText(value, _button1);
		}
	}

	public T Button2Value
	{
		get => _button2Value;
		set
		{
			_button2Value = value;
			SetButtonText(value, _button2);
		}
	}

	public T Button3Value
	{
		get => _button3Value;
		set
		{
			_button3Value = value;
			SetButtonText(value, _button3);
		}
	}

	private static Button CreateButton(string defaultText)
	{
		return new Button
		{
			Text				= defaultText,
			StyleClass			= ["SelectorButtonStyle"],
			HorizontalOptions	= LayoutOptions.Fill
		};
	}

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

	private static void SetButtonText(T value, Button button)
	{
		button.Text = DigitalProduction.Reflection.Attributes.GetAttribute<ControlTextAttribute>(value)?.Text ?? value.ToString();
	}
}
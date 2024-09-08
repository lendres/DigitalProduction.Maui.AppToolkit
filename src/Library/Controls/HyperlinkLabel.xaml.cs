namespace DigitalProduction.Controls;

public partial class HyperlinkLabel : ContentView
{
	public static readonly BindableProperty TextProperty = BindableProperty.Create(
		nameof(Text),
		typeof(string),
		typeof(HyperlinkLabel),
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			HyperlinkLabel hyperlinkLabel = (HyperlinkLabel)bindable;
			hyperlinkLabel.Link.Text = newValue as string;
		}
	);

	public static readonly BindableProperty UrlProperty = BindableProperty.Create(
		nameof(Url),
		typeof(string),
		typeof(HyperlinkLabel),
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			HyperlinkLabel hyperlinkLabel = (HyperlinkLabel)bindable;
			hyperlinkLabel.Link.Url = newValue as string;
		}
	);

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public string Url
	{
		get => (string)GetValue(UrlProperty);
		set => SetValue(UrlProperty, value);
	}

	public HyperlinkLabel()
	{
		InitializeComponent();
	}
}
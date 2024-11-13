namespace DigitalProduction.Controls;

public partial class HyperlinkLabel : ContentView
{
	public HyperlinkLabel()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty TextProperty = BindableProperty.Create(
		nameof(Text),
		typeof(string),
		typeof(HyperlinkLabel),
		"",
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			HyperlinkLabel hyperlinkLabel = (HyperlinkLabel)bindable;
			hyperlinkLabel._hyperlinkSpan.Text = newValue as string;
		}
	);

	public static readonly BindableProperty UrlProperty = BindableProperty.Create(
		nameof(Url),
		typeof(string),
		typeof(HyperlinkLabel),
		"",
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			HyperlinkLabel hyperlinkLabel = (HyperlinkLabel)bindable;
			hyperlinkLabel._hyperlinkSpan.Url = (string)newValue;
		}
	);

	//public static readonly BindableProperty SpanProperty = BindableProperty.Create(
	//	nameof(Span),
	//	typeof(HyperlinkSpan),
	//	typeof(HyperlinkLabel),
	//	propertyChanged: (bindable, oldValue, newValue) =>
	//	{
	//		HyperlinkLabel hyperlinkLabel = (HyperlinkLabel)bindable;
	//		hyperlinkLabel._hyperlinkSpan = (HyperlinkSpan)newValue;
	//	}
	//);

	public static readonly BindableProperty StyleClassProperty = BindableProperty.Create(
		nameof(StyleClass),
		typeof(string),
		typeof(HyperlinkLabel),
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			HyperlinkLabel hyperlinkLabel = (HyperlinkLabel)bindable;
			bool hasValue = Application.Current!.Resources.TryGetValue((string)newValue, out object foundObject);
            if (hasValue && foundObject is Style style)
            {
                hyperlinkLabel._hyperlinkSpan.Style = style;
            }
			
		}
	);

	public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
		nameof(TextColor),
		typeof(Color),
		typeof(HyperlinkLabel),
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			if (bindable is HyperlinkLabel hyperlinkLabel)
			{
				hyperlinkLabel._hyperlinkSpan.TextColor = (Color)newValue;
			}
		}
	);

	public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
		nameof(FontFamily),
		typeof(string),
		typeof(HyperlinkLabel),
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			if (bindable is HyperlinkLabel hyperlinkLabel)
			{
				hyperlinkLabel._hyperlinkSpan.FontFamily = (string)newValue;
			}
		}
	);

	public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
		nameof(FontSize),
		typeof(double),
		typeof(HyperlinkLabel),
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			if (bindable is HyperlinkLabel hyperlinkLabel)
			{
				hyperlinkLabel._hyperlinkSpan.FontSize = (double)newValue;
			}
		}
	);

	public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
		nameof(FontAttributes),
		typeof(FontAttributes),
		typeof(HyperlinkLabel),
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			if (bindable is HyperlinkLabel hyperlinkLabel)
			{
				hyperlinkLabel._hyperlinkSpan.FontAttributes = (FontAttributes)newValue;
			}
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

	//public HyperlinkSpan Span
	//{
	//	get => (HyperlinkSpan)GetValue(SpanProperty);
	//	set => SetValue(SpanProperty, value);
	//}
	public HyperlinkSpan Span
	{
		get => _hyperlinkSpan;
		//set => _hyperlinkSpan = value;
	}

	public new string StyleClass
	{
		get => (string)GetValue(StyleProperty);
		set => SetValue(StyleProperty, value);
	}

	public Color TextColor
	{
		get => (Color)GetValue(StyleProperty);
		set => SetValue(StyleProperty, value);
	}

	public string FontFamily
	{
		get => (string)GetValue(StyleProperty);
		set => SetValue(StyleProperty, value);
	}

	public double FontSize
	{
		get => (double)GetValue(StyleProperty);
		set => SetValue(StyleProperty, value);
	}

	public FontAttributes FontAttributes
	{
		get => (FontAttributes)GetValue(StyleProperty);
		set => SetValue(StyleProperty, value);
	}
}
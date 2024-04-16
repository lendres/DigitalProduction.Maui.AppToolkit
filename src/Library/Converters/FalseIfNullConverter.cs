using System.Globalization;

namespace DigitalProduction.Converters;

public class FalseIfNullConverter : Microsoft.Maui.Controls.IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return value is null ? false : true;
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}

using System.Globalization;

namespace DigitalProduction.Converters;

/// <summary>
/// Returns true if debugging, false otherwise.  Useful to have debugging controls, but have them hidden in production code.
/// 
/// E.g.: IsVisible="{Binding ., Converter={StaticResource AreDebuggingConverter}}"
/// </summary>
public class EmptyIfZeroConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is not null)
		{
			if (value is int || value is double || value is float)
			{
				if ((int)value == 0)
				{
					return "";
				}
				else
				{
					return value;
				}
			}
		}

		return "";
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
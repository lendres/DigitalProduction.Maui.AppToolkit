using System.Globalization;

namespace DigitalProduction.Maui.Converters;

/// <summary>
/// Returns an empty string if a number is zero.  Otherwise, it returns the value of the number.
/// 
/// Useful to avoid displaying anything if a number is zero.
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
					return string.Empty;
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
using System.Globalization;

namespace DigitalProduction.Converters;

/// <summary>
/// Returns true if debugging, false otherwise.  Useful to have debugging controls, but have them hidden in production code.
/// 
/// E.g.: IsVisible="{Binding ., Converter={StaticResource AreDebuggingConverter}}"
/// </summary>
public class AreDebuggingConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		#if DEBUG
			return true;
		#else
			return false;
		#endif
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
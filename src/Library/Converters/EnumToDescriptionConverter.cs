using CommunityToolkit.Maui.Converters;
using DigitalProduction.Exceptions;
using System.Globalization;

namespace DigitalProduction.Converters;

public class EnumToDescriptionConverter<TEnum> : IValueConverter where TEnum : struct
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is null)
		{
			return default(TEnum);
		}

		InvalidTypeException.ThrowNotType(typeof(TEnum), value);
		string result = DigitalProduction.Reflection.Attributes.GetDescription((TEnum)value);
		StringIsNullOrEmptyException.ThrowIfNullOrEmpty("The enumeration must have a Description attribute.", result);
		return result;
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is null)
		{
			return "";
		}

		InvalidTypeException.ThrowNotType(typeof(string), value);
		return DigitalProduction.Reflection.Enumerations.GetInstanceFromDescription<TEnum>((string)value);
	}
}
using CommunityToolkit.Maui.Converters;
using System.ComponentModel;
using System.Globalization;

namespace DigitalProduction.Converters;

/// <summary>
///     Converts an <see cref="Enum" /> to its underlying <see cref="int" /> value.
/// </summary>
public class EnumToStringConverter<TEnum> : IValueConverter where TEnum : struct
{
	/// <inheritdoc/>
	public string DefaultConvertReturnValue { get; set; } = "";

	/// <inheritdoc/>
	public Enum DefaultConvertBackReturnValue { get; set; } = DefaultEnum.Value;

	enum DefaultEnum { Value }

	/// <summary>
	/// Convert a default <see cref="Enum"/> (i.e., extending <see cref="int"/>) to corresponding underlying <see cref="int"/>
	/// </summary>
	/// <param name="value"><see cref="Enum"/> value to convert</param>
	/// <param name="parameter"></param>
	/// <param name="culture">Unused: Culture to use in the converter</param>
	/// <returns>The underlying <see cref="int"/> value of the passed enum value</returns>
	/// <exception cref="ArgumentException">If value is not an enumeration type</exception>
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		ArgumentNullException.ThrowIfNull(value);

		return ((TEnum)value).ToString() ?? DefaultConvertReturnValue;
	}

	/// <summary>
	/// Returns the <see cref="Enum"/> associated with the specified <see cref="int"/> value defined in the targetType
	/// </summary>
	/// <param name="value"><see cref="Enum"/> value to convert</param>
	/// <param name="parameter"></param>
	/// <param name="culture">Unused: Culture to use in the converter</param>
	/// <returns>The underlying <see cref="Enum"/> of the associated targetType</returns>
	/// <exception cref="ArgumentException">If value is not a valid value in the targetType enum</exception>
	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		ArgumentNullException.ThrowIfNull(value);
		ArgumentNullException.ThrowIfNull(targetType);

		if (!Enum.IsDefined(targetType, value))
		{
			throw new InvalidEnumArgumentException($"{value} is not valid for {targetType.Name}.");
		}

		return (Enum)Enum.Parse(targetType, (string)value);
	}
}
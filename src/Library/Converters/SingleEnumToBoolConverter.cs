using System.Globalization;
using System.Reflection;

namespace DigitalProduction.Converters;

/// <summary>
///     Convert an <see cref="Enum"/> to corresponding <see cref="bool"/>
/// </summary>
public class SingleEnumToBoolConverter<TEnum> : IValueConverter where TEnum : Enum
{
	/// <inheritdoc/>
	public bool DefaultConvertReturnValue { get; set; } = false;

	public Enum DefaultConvertBackReturnValue { get; set; } = DefaultEnum.Value;

	public enum DefaultEnum { Value }

	/// <summary>
	///     Enum values, that converts to <c>true</c> (optional)
	/// </summary>
	public TEnum? TrueValue { get; set; } = default;

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

		if (TrueValue is null)
		{
			ArgumentNullException.ThrowIfNull(parameter);
			return CompareTwoEnums((Enum)value, (TEnum)parameter);
		}
		else
		{
			return CompareTwoEnums((Enum)value, (TEnum)TrueValue);
		}

		static bool CompareTwoEnums(in Enum valueToCheck, in TEnum? referenceEnumValue)
		{
			if (referenceEnumValue is null)
			{
				return false;
			}

			var valueToCheckType = valueToCheck.GetType();
			if (valueToCheckType != referenceEnumValue.GetType())
			{
				return false;
			}

			if (valueToCheckType.GetTypeInfo().GetCustomAttribute<FlagsAttribute>() != null)
			{
				return referenceEnumValue.HasFlag(valueToCheck);
			}

			return Equals(valueToCheck, referenceEnumValue);
		}
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
		throw new NotImplementedException();
	}
}
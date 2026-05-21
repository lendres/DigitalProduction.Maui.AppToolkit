using System.Globalization;
using System.Reflection;

namespace DigitalProduction.Maui.Converters;

/// <summary>
///     Convert an <see cref="System.Enum"/> to corresponding <see cref="bool"/>
/// </summary>
public class SingleEnumToBoolConverter<TEnum> : IValueConverter where TEnum : struct, Enum
{
	/// <inheritdoc/>
	public bool DefaultConvertReturnValue { get; set; } = false;

	public enum DefaultEnum { Value }

	/// <summary>
	/// Enum value to compare against (optional).
	/// </summary>
	public TEnum? Enum { get; set; } = null;

	/// <summary>
	/// The result to return when the value and the provided Enum property or the passed parameter are equal. If
	/// false, the return value will be inverted.
	/// </summary>
	public bool ResultIfEqual { get; set; } = true;

	/// <summary>
	/// Converts an <see cref="System.Enum"/> field to a <see cref="bool"/> value by comparing the passed enum value
	/// with the provided Enum property or the passed parameter.
	/// </summary>
	/// <param name="value"><see cref="System.Enum"/> value to compare.</param>
	/// <param name="parameter"><see cref="System.Enum"/>Enum value to compare against (optional).</param>
	/// <param name="culture">Unused: Culture to use in the converter</param>
	/// <returns>
	/// A <see cref="bool"/> value that is true if the values are equal, false otherwise. If ResultIfEqual
	/// is false, the return value will be inverted.
	/// </returns>
	/// <exception cref="ArgumentException">If value is not an enumeration type</exception>
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		ArgumentNullException.ThrowIfNull(value);

		bool areEqual = DefaultConvertReturnValue;
		if (Enum is null)
		{
			ArgumentNullException.ThrowIfNull(parameter);
			areEqual = CompareTwoEnums((Enum)value, (TEnum)parameter);
		}
		else
		{
			areEqual = CompareTwoEnums((Enum)value, (TEnum)Enum);
		}

		return ResultIfEqual ? areEqual : !areEqual;
	}

	private static bool CompareTwoEnums(in Enum valueToCheck, in TEnum? referenceEnumValue)
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
			return referenceEnumValue.Value.HasFlag(valueToCheck);
		}

		return Equals(valueToCheck, referenceEnumValue);
	}

	/// <summary>
	/// Returns the <see cref="System.Enum"/> associated with the specified <see cref="int"/> value defined in the targetType
	/// </summary>
	/// <param name="value"><see cref="System.Enum"/> value to convert</param>
	/// <param name="parameter"></param>
	/// <param name="culture">Unused: Culture to use in the converter</param>
	/// <returns>The underlying <see cref="System.Enum"/> of the associated targetType</returns>
	/// <exception cref="ArgumentException">If value is not a valid value in the targetType enum</exception>
	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
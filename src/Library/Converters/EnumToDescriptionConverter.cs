using CommunityToolkit.Maui.Converters;
using DigitalProduction.Exceptions;
using System.Globalization;

namespace DigitalProduction.Converters;

public class EnumToDescriptionConverter<TEnum> : BaseConverter<TEnum, string> where TEnum : struct
{
	/// <inheritdoc/>
	public override string DefaultConvertReturnValue { get; set; } = "";

	/// <inheritdoc/>
	public override TEnum DefaultConvertBackReturnValue { get; set; } = default;

	/// <summary>
	/// Converts <see cref="string"/> to an enumeration.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	/// <param name="culture">The culture to use in the converter.  This is not implemented.</param>
	/// <returns>The enumeration whose DescriptionAttribute matches the specified string.</returns>
	public override string ConvertFrom(TEnum value, CultureInfo? culture = null)
	{
		string result = Reflection.Attributes.GetDescription(value);
		StringIsNullOrEmptyException.ThrowIfNullOrEmpty("The enumeration must have a Description attribute.", result);
		return result;
	}

	/// <summary>
	/// Converts back object to <see cref="string"/>.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	/// <param name="culture">The culture to use in the converter.  This is not implemented.</param>
	/// <returns>The text in the DescriptionAttribute of the enumeration.</returns>
	public override TEnum ConvertBackTo(string value, CultureInfo? culture = null)
	{
		TEnum result = Reflection.Enumerations.GetInstanceFromDescription<TEnum>(value);
		return result;
	}
}
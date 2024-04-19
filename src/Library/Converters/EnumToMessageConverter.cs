using CommunityToolkit.Maui.Converters;
using DigitalProduction.Reflection;
using System.Globalization;

namespace DigitalProduction.Maui.Converters;

/// <summary>
///     Converts an <see cref="Enum" /> to its underlying <see cref="int" /> value.
/// </summary>
public class EnumToMessageConverter : BaseConverterOneWay<Enum, string, Type>
{
	/// <inheritdoc/>
	public override string DefaultConvertReturnValue { get; set; } = "";

	enum DefaultEnum { Value }

	/// <summary>
	/// Convert a default <see cref="Enum"/> (i.e., extending <see cref="int"/>) to corresponding underlying <see cref="int"/>
	/// </summary>
	/// <param name="value"><see cref="Enum"/> value to convert</param>
	/// <param name="parameter"></param>
	/// <param name="culture">Unused: Culture to use in the converter</param>
	/// <returns>The underlying <see cref="int"/> value of the passed enum value</returns>
	/// <exception cref="ArgumentException">If value is not an enumeration type</exception>
	public override string ConvertFrom(Enum value, Type? parameter = null, CultureInfo? culture = null)
	{
		ArgumentNullException.ThrowIfNull(value);

		string description = "";

		MessageAttribute? attribute = DigitalProduction.Reflection.Attributes.GetAttribute<MessageAttribute>(value);
		if (attribute != null)
		{
			description = attribute.Message;
		}
		return description;
	}
}
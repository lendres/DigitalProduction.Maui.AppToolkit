using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace DigitalProduction.Converters;

/// <summary>
/// Gets the first string out of an enumerable.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class FirstValidationErrorConverter : BaseConverterOneWay<IEnumerable<string>, string>
{
	public override string DefaultConvertReturnValue { get; set; } = string.Empty;

	public override string ConvertFrom(IEnumerable<string> value, CultureInfo? culture)
	{
		return value?.FirstOrDefault() ?? DefaultConvertReturnValue;
	}
}
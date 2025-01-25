using DigitalProduction.Maui.Validation;
using System.Globalization;

namespace DigitalProduction.Maui.Validation;

/// <summary>
/// Is not null or empty validation rule for string.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class IsNumericRule : ValidationRuleBase<string>
{
	public double	MinimumValue { get; set; }			= double.MinValue;

	public double	MaximumValue { get; set; }			= double.MaxValue;

	public double	MinimumDecimalPlaces { get; set; }	= 0;

	public double	MaximumDecimalPlaces { get; set; }	= double.MaxValue;

	public override bool Check(string? value)
	{
		if (value is null)
		{
			return false;
		}

		if (!(double.TryParse(value, out double numeric) && numeric >= MinimumValue && numeric <= MaximumValue))
		{
			return false;
		}

		int	decimalDelimiterIndex	= value.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
		bool hasDecimalDelimiter	= decimalDelimiterIndex >= 0;

		// If MaximumDecimalPlaces equals zero, ".5" or "14." should be considered as invalid inputs.
		if (hasDecimalDelimiter && MaximumDecimalPlaces == 0)
		{
			return false;
		}

		int decimalPlaces = hasDecimalDelimiter ? value.Substring(decimalDelimiterIndex + 1, value.Length - decimalDelimiterIndex - 1).Length : 0;

		return decimalPlaces >= MinimumDecimalPlaces && decimalPlaces <= MaximumDecimalPlaces;
	}
}
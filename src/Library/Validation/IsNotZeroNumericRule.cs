using DigitalProduction.Maui.Validation;
using System.Globalization;

namespace DigitalProduction.Maui.Validation;

/// <summary>
/// Checks that the value is a number and is not zero.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class IsNotZeroNumericRule : IsNumericRule
{
	public override bool Check(string? value)
	{
		if (base.Check(value))
		{
			double.TryParse(value, out double numeric);
			if (numeric != 0)
			{
				return true;
			}
		}
		return false;
	}
}
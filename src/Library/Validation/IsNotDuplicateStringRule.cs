using DigitalProduction.Validation;
using DigitalProduction.Exceptions;

namespace Data.Translation.Validation;

/// <summary>
/// Is not null or empty validation rule for string.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class IsNotDuplicateStringRule : ValidationRuleBase<string>
{
	public string? ExcludeValue { get; set; } = null;

	public List<string>? Values { get; set; }

	public override bool Check(string? value)
	{

		if (value is null)
		{
			return false;
		}

		if (Values is null)
		{
			return false;
		}

		if (value == ExcludeValue)
		{
			// The "new" (entered) value is the same as it was (unchanged).  This is ok.
			return true;
		}

		bool exists = Values.Contains(value);
		return !exists;
	}
}
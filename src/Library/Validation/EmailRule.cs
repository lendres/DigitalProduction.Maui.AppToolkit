namespace DigitalProduction.Validation;

/// <summary>
/// Email validation rule.
/// </summary>
/// <typeparam name="T">Type of object to validate.</typeparam>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class EmailRule<T> : ValidationRuleBase<T>
{
	private readonly System.Text.RegularExpressions.Regex _regex = new(@"^([w.-]+)@([w-]+)((.(w){2,3})+)$");

	public override bool Check(T? value) =>
		value is string str &&
		_regex.IsMatch(str);
}
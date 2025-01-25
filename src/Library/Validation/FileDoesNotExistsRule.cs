namespace DigitalProduction.Maui.Validation;

/// <summary>
/// File must exist validation rule.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class FileDoesNotExistsRule : ValidationRuleBase<string>
{
	public override bool Check(string? value) =>
		!string.IsNullOrWhiteSpace(value) &&
		!File.Exists(value);
}
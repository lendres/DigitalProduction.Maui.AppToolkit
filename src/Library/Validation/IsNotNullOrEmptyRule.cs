namespace DigitalProduction.Validation;

/// <summary>
/// Is not null or empty validation rule for string.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class IsNotNullOrEmptyRule : ValidationRuleBase<string>
{
    public override bool Check(string? value) =>
        value is not null &&
        !string.IsNullOrWhiteSpace(value);
}
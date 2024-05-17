namespace DigitalProduction.Validation;

/// <summary>
/// Is not null or empty validation rule for string.
/// </summary>
/// <typeparam name="T">Type of object to validate.</typeparam>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class IsNotNullOrEmptyRule<T> : ValidationRuleBase<T>
{
    public override bool Check(T? value) =>
        value is string str &&
        !string.IsNullOrWhiteSpace(str);
}
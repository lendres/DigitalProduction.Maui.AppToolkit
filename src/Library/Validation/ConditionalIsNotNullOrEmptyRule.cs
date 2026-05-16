namespace DigitalProduction.Maui.Validation;

/// <summary>
/// Is not null or empty validation rule for string.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class ConditionalIsNotNullOrEmptyRule : ValidationRuleBase<string>, IConditionalRule
{
    public Func<bool> IsRequired { get; set; } = () => true;

    public override bool Check(string? value) => IsRequired() ? !string.IsNullOrWhiteSpace(value) : true;
}
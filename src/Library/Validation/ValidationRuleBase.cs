namespace DigitalProduction.Validation;

/// <summary>
/// Validation rule base class.
/// </summary>
/// <typeparam name="T">Type of object to validate.</typeparam>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public abstract class ValidationRuleBase<T> : IValidationRule<T>
{
	public string ValidationMessage { get; set; } = "";

	public abstract bool Check(T? value);
}
namespace DigitalProduction.Validation;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Type of object to validate.</typeparam>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public interface IValidationRule<T>
{
    string ValidationMessage { get; set; }

    bool Check(T? value);
}
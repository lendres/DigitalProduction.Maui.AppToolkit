namespace DigitalProduction.Maui.Validation;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public interface IValidity
{
    bool IsValid { get; }
}
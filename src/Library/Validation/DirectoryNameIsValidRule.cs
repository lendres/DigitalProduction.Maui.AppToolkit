﻿using DigitalProduction.IO;

namespace DigitalProduction.Maui.Validation;

/// <summary>
/// File must exist validation rule.
/// </summary>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class DirectoryNameIsValidRule : ValidationRuleBase<string>
{
	public override bool Check(string? value) =>
		DigitalProduction.IO.Path.IsValidDirectory(value) == PathValidationResult.Valid;
}
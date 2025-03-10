﻿using CommunityToolkit.Maui.Behaviors;
using System.Diagnostics.CodeAnalysis;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// The <see cref="FileExistsValidationBehavior"/> is a behavior that allows the user to determine if a file exists.  Additional properties handling validation are inherited from <see cref="ValidationBehavior"/>.
/// </summary>
[RequiresUnreferencedCode($"{nameof(TextValidationBehavior)} is not trim safe because it uses bindings with string paths.")]
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public partial class FileExistsValidationBehavior : ErrorReportableValidationBehavior<string, PathErrorType>
{
	/// <inheritdoc/>
	protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(value);

		bool passed = File.Exists(value);
		if (passed)
		{
			Error = PathErrorType.None;
		}
		else
		{
			Error = PathErrorType.FileNotFound;
		}

		return new ValueTask<bool>(passed);
	}
}
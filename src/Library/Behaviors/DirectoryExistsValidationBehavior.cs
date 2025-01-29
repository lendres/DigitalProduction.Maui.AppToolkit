using CommunityToolkit.Maui.Behaviors;
using System.Diagnostics.CodeAnalysis;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// The <see cref="DirectoryExistsValidationBehavior"/> is a behavior that allows the user to determine if a directory exists. Additional properties handling validation are inherited from <see cref="ValidationBehavior"/>.
/// </summary>
[RequiresUnreferencedCode($"{nameof(TextValidationBehavior)} is not trim safe because it uses bindings with string paths.")]
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public partial class DirectoryExistsValidationBehavior : ErrorReportableValidationBehavior<string, PathErrorType>
{
	/// <inheritdoc/>
	protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(value);

		bool passed = Directory.Exists(value);
		if (passed)
		{
			Error = PathErrorType.None;
		}
		else
		{
			Error = PathErrorType.DirectoryNotFound;
		}

		return new ValueTask<bool>(passed);
	}
}
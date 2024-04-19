using CommunityToolkit.Maui.Behaviors;
using DigitalProduction.Behaviors;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// The <see cref="FileExistsValidationBehavior"/> is a behavior that allows the user to determine if a file exists.  Additional properties handling validation are inherited from <see cref="ValidationBehavior"/>.
/// </summary>
public class FileExistsValidationBehavior : ErrorReportableValidationBehavior<string, FileErrorType>
{
	/// <inheritdoc/>
	protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(value);

		bool passed = File.Exists(value);
		if (passed)
		{
			Error = FileErrorType.None;
		}
		else
		{
			Error = FileErrorType.FileNotFound;
		}

		return new ValueTask<bool>(passed);
	}
}
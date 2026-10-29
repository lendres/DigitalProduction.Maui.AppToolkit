
namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// The <see cref="FileExistsValidationBehavior"/> is a behavior that allows the user to determine if a file exists.  Additional properties handling validation are provided by the trim-safe validation base.
/// </summary>
public partial class FileExistsValidationBehavior : ErrorReportableValidationBehavior<string, PathErrorType>
{
	/// <inheritdoc/>
	protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
	{
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
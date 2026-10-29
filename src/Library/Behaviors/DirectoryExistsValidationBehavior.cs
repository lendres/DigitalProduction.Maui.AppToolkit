
namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// The <see cref="DirectoryExistsValidationBehavior"/> is a behavior that allows the user to determine if a directory exists. Additional properties handling validation are provided by the trim-safe validation base.
/// </summary>
public partial class DirectoryExistsValidationBehavior : ErrorReportableValidationBehavior<string, PathErrorType>
{
	/// <inheritdoc/>
	protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
	{
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
using DigitalProduction.IO;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// Validates the file name in a full path and optionally requires the file to exist.
/// Additional properties handling validation are provided by the trim-safe validation base.
/// </summary>
/// <remarks>
/// The value is expected to be a fully qualified file path, including the directory and file name.
/// Relative paths and bare file names are outside the intended input contract; callers should resolve
/// them to a full path before validation. This behavior does not enforce or normalize that requirement.
/// </remarks>
public partial class FileNameValidationBehavior : ErrorReportableValidationBehavior<string, PathValidationResult>
{
	/// <summary>
	/// Backing BindableProperty for the <see cref="RequireFileToExist"/> property.
	/// </summary>
	public static readonly BindableProperty RequireFileToExistProperty =
		BindableProperty.Create(nameof(RequireFileToExist), typeof(bool), typeof(FileNameValidationBehavior), default(bool), propertyChanged: OnValidationPropertyChanged);

	/// <summary>
	/// Specifies whether the full path must identify an existing file. When false, only file name
	/// validity is checked; the file and its parent directory do not have to exist. This is a bindable property.
	/// </summary>
	public bool RequireFileToExist
	{
		get => (bool)GetValue(RequireFileToExistProperty);
		set => SetValue(RequireFileToExistProperty, value);
	}

	/// <inheritdoc/>
	protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
	{
		if (RequireFileToExist)
		{
			Error = DigitalProduction.IO.Path.IsValidFileName(value, new PathValidationOptions() { RequirePathToExist = true });
			if (Error == PathValidationResult.Valid)
			{
				if (!System.IO.File.Exists(value))
				{
					Error = PathValidationResult.PathDoesNotExist;
				}
			}
		}
		else
		{
			Error = DigitalProduction.IO.Path.IsValidFileName(value);
		}

		return new ValueTask<bool>(Error == PathValidationResult.Valid);
	}
}

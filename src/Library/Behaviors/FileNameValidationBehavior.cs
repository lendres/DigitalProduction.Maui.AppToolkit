using CommunityToolkit.Maui.Behaviors;
using DigitalProduction.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitalProduction.Behaviors;

/// <summary>
/// The <see cref="FileNameValidationBehavior"/> is a behavior that allows the user to determine if a file exists.  Additional properties handling validation are inherited from <see cref="ValidationBehavior"/>.
/// </summary>
public class FileNameValidationBehavior : ErrorReportableValidationBehavior<string, PathValidationResult>
{
	/// <summary>
	/// Backing BindableProperty for the <see cref="RequireFileToExist"/> property.
	/// </summary>
	public static readonly BindableProperty RequireFileToExistProperty =
		BindableProperty.Create(nameof(RequireFileToExist), typeof(bool), typeof(DirectoryExistsValidationBehavior), default(bool), propertyChanged: OnValidationPropertyChanged);

	/// <summary>
	/// The associated error found during the validation. This is a bindable property.  It is recommended that the TError type include a "None" error code.
	/// </summary>
	public bool RequireFileToExist
	{
		get => (bool)GetValue(RequireFileToExistProperty);
		set => SetValue(RequireFileToExistProperty, value);
	}

	/// <inheritdoc/>
	protected override ValueTask<bool> ValidateAsync(string? value, CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(value);

		if (RequireFileToExist)
		{
			Error = DigitalProduction.IO.Path.IsValidFileName(value, new PathValidationOptions() { RequirePathToExist = true });
		}
		else
		{
			Error = DigitalProduction.IO.Path.IsValidFileName(value);
		}

		return new ValueTask<bool>(Error == PathValidationResult.Valid);
	}
}
using System.Globalization;
using CommunityToolkit.Maui.Behaviors;
using DigitalProduction.Behaviors;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// The <see cref="FileExistsValidationBehavior"/> is a behavior that allows the user to determine if a file exists. For example, an <see cref="Entry"/> control can be styled differently depending on whether a valid or an invalid numeric input is provided. Additional properties handling validation are inherited from <see cref="ValidationBehavior"/>.
/// </summary>
public class FileExistsValidationBehavior : ValidationBehavior<string>
{
	/// <summary>
	/// Backing BindableProperty for the <see cref="FileErroMessageConverter"/> property.
	/// </summary>
	public static readonly BindableProperty ErrorMessageProperty =
		BindableProperty.Create(nameof(Error), typeof(FileErrorType), typeof(FileExistsValidationBehavior), FileErrorType.None, propertyChanged: OnValidationPropertyChanged);

	/// <summary>
	/// The minimum numeric value that will be allowed. This is a bindable property.
	/// </summary>
	public FileErrorType Error
	{
		get => (FileErrorType)GetValue(ErrorMessageProperty);
		set => SetValue(ErrorMessageProperty, value);
	}

	/// <inheritdoc/>
	protected override string? Decorate(string? value)
		=> base.Decorate(value)?.Trim();

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
using CommunityToolkit.Maui.Behaviors;
//using System.Diagnostics.CodeAnalysis;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// The <see cref="ErrorReportableValidationBehavior{T, TError}"/> is a behavior that allows the user to determine get an error code. Additional properties handling validation are inherited from <see cref="ValidationBehavior"/>.
/// </summary>
//public abstract class ErrorReportableValidationBehavior<T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]TError> : ValidationBehavior<T>
public abstract class ErrorReportableValidationBehavior<T, TError> : ValidationBehavior<T>
{
	/// <summary>
	/// Backing BindableProperty for the <see cref="Error"/> property.
	/// </summary>
	public static readonly BindableProperty ErrorMessageProperty =
		BindableProperty.Create(nameof(Error), typeof(TError), typeof(DirectoryExistsValidationBehavior), default(TError), propertyChanged: OnValidationPropertyChanged);

	/// <summary>
	/// The associated error found during the validation. This is a bindable property.  It is recommended that the TError type include a "None" error code.
	/// </summary>
	public TError Error
	{
		get => (TError)GetValue(ErrorMessageProperty);
		set => SetValue(ErrorMessageProperty, value);
	}
}
using System.Diagnostics.CodeAnalysis;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// Reports an error code in addition to the validity state provided by the trim-safe validation base.
/// </summary>
public abstract class ErrorReportableValidationBehavior<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)]TError> : TrimSafeValidationBehavior<T>
{
	/// <summary>
	/// Backing BindableProperty for the <see cref="Error"/> property.
	/// </summary>
	public static readonly BindableProperty ErrorMessageProperty =
		BindableProperty.Create(nameof(Error), typeof(TError), typeof(ErrorReportableValidationBehavior<T, TError>), default(TError));

	/// <summary>
	/// The associated error found during the validation. This is a bindable property.  It is recommended that the TError type include a "None" error code.
	/// </summary>
	public TError Error
	{
		get => (TError)GetValue(ErrorMessageProperty);
		set => SetValue(ErrorMessageProperty, value);
	}
}

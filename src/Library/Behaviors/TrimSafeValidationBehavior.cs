using CommunityToolkit.Maui.Behaviors;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// Validates a value without creating reflection-based bindings to the attached control.
/// </summary>
/// <remarks>
/// Supports Text on Entry, Editor, SearchBar and Label. Override <see cref="GetValueProperty"/>
/// for another control or set ValuePropertyName to an empty string and bind Value with a compiled binding.
/// This is independent of Toolkit ValidationBehavior and cannot be nested in MultiValidationBehavior.
/// </remarks>
public abstract class TrimSafeValidationBehavior<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T> : Behavior<VisualElement>, IDisposable
{
	#region Fields

	public const string					ValidVisualState			= "Valid";
	public const string					InvalidVisualState			= "Invalid";

	private ValidationFlags				_currentStatus;
	private CancellationTokenSource?	_validationCancellation;
	private bool						_isAttaching;
	private bool						_isDisposed;

	#endregion

	#region Bindable Properties

	/// <summary>
	/// Backing BindableProperty for the <see cref="Value"/> property.
	/// </summary>
	public static readonly BindableProperty ValueProperty =
		BindableProperty.Create(nameof(Value), typeof(T), typeof(TrimSafeValidationBehavior<T>), default(T),
		propertyChanged: OnValueChanged);

	/// <summary>
	/// The value to validate. This is a bindable property.
	/// </summary>
	public T? Value
	{
		get => (T?)GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="IsValid"/> property.
	/// </summary>
	public static readonly BindableProperty IsValidProperty =
		BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(TrimSafeValidationBehavior<T>), true, BindingMode.OneWayToSource,
		propertyChanged: (b, _, value) => ((TrimSafeValidationBehavior<T>)b).IsNotValid = !(bool)value);

	/// <summary>
	/// Indicates whether the current value is valid. This is a bindable property.
	/// </summary>
	public bool IsValid
	{
		get => (bool)GetValue(IsValidProperty);
		set => SetValue(IsValidProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="IsNotValid"/> property.
	/// </summary>
	public static readonly BindableProperty IsNotValidProperty =
		BindableProperty.Create(nameof(IsNotValid), typeof(bool), typeof(TrimSafeValidationBehavior<T>), false, BindingMode.OneWayToSource);

	/// <summary>
	/// Indicates whether the current value is invalid. This is a bindable property.
	/// </summary>
	public bool IsNotValid
	{
		get => (bool)GetValue(IsNotValidProperty);
		set => SetValue(IsNotValidProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="IsRunning"/> property.
	/// </summary>
	public static readonly BindableProperty IsRunningProperty =
		BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(TrimSafeValidationBehavior<T>), false, BindingMode.OneWayToSource);

	/// <summary>
	/// Indicates whether validation is in progress. This is a bindable property.
	/// </summary>
	public bool IsRunning
	{
		get => (bool)GetValue(IsRunningProperty);
		set => SetValue(IsRunningProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="ValidStyle"/> property.
	/// </summary>
	public static readonly BindableProperty ValidStyleProperty =
		BindableProperty.Create(nameof(ValidStyle), typeof(Style), typeof(TrimSafeValidationBehavior<T>), propertyChanged: OnValidationPropertyChanged);

	/// <summary>
	/// The style applied after successful validation. This is a bindable property.
	/// </summary>
	public Style? ValidStyle
	{
		get => (Style?)GetValue(ValidStyleProperty);
		set => SetValue(ValidStyleProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="InvalidStyle"/> property.
	/// </summary>
	public static readonly BindableProperty InvalidStyleProperty =
		BindableProperty.Create(nameof(InvalidStyle), typeof(Style), typeof(TrimSafeValidationBehavior<T>), propertyChanged: OnValidationPropertyChanged);

	/// <summary>
	/// The style applied after failed validation. This is a bindable property.
	/// </summary>
	public Style? InvalidStyle
	{
		get => (Style?)GetValue(InvalidStyleProperty);
		set => SetValue(InvalidStyleProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="Flags"/> property.
	/// </summary>
	public static readonly BindableProperty FlagsProperty =
		BindableProperty.Create(nameof(Flags), typeof(ValidationFlags), typeof(TrimSafeValidationBehavior<T>),
		ValidationFlags.ValidateOnUnfocused | ValidationFlags.ForceMakeValidWhenFocused,
		propertyChanged: OnValidationPropertyChanged);

	/// <summary>
	/// Specifies when validation runs automatically. This is a bindable property.
	/// </summary>
	public ValidationFlags Flags
	{
		get => (ValidationFlags)GetValue(FlagsProperty);
		set => SetValue(FlagsProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="ValuePropertyName"/> property.
	/// </summary>
	public static readonly BindableProperty ValuePropertyNameProperty =
		BindableProperty.Create(nameof(ValuePropertyName), typeof(string), typeof(TrimSafeValidationBehavior<T>), "Text",
		propertyChanged: (b, _, _) => ((TrimSafeValidationBehavior<T>)b).ReadAttachedValue());

	/// <summary>
	/// The attached control property to read. Use an empty string for an explicit Value binding.
	/// </summary>
	public string ValuePropertyName
	{
		get => (string)GetValue(ValuePropertyNameProperty);
		set => SetValue(ValuePropertyNameProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="ForceValidateCommand"/> property.
	/// </summary>
	public static readonly BindableProperty ForceValidateCommandProperty =
		BindableProperty.Create(nameof(ForceValidateCommand), typeof(ICommand), typeof(TrimSafeValidationBehavior<T>),
		defaultBindingMode: BindingMode.OneWayToSource,
		defaultValueCreator: b => new Command<CancellationToken>(async token => await ((TrimSafeValidationBehavior<T>)b).ForceValidate(token)));

	/// <summary>
	/// The command that forces validation, accepting a CancellationToken parameter.
	/// </summary>
	public ICommand ForceValidateCommand
	{
		get => (ICommand)GetValue(ForceValidateCommandProperty);
		set => SetValue(ForceValidateCommandProperty, value);
	}

	#endregion

	#region Properties

	/// <summary>
	/// The control currently attached to this behavior.
	/// </summary>
	protected VisualElement? View { get; private set; }

	#endregion

	#region Methods

	/// <summary>
	/// Returns the source property directly, without reflection or a string-path binding.
	/// </summary>
	protected virtual BindableProperty GetValueProperty(VisualElement view) => (view, ValuePropertyName) switch
	{
		(InputView, "Text") => InputView.TextProperty,
		(Label, "Text") => Label.TextProperty,
		_ => throw new NotSupportedException("Use a compiled Value binding or override GetValueProperty for this control/property.")
	};

	/// <summary>
	/// Allows derived behaviors to transform the value before validation.
	/// </summary>
	protected virtual T? Decorate(T? value) => value;

	/// <summary>
	/// Validates the value and returns whether it is valid.
	/// </summary>
	protected abstract ValueTask<bool> ValidateAsync(T? value, CancellationToken token);

	/// <summary>
	/// Forces a validation pass using the current value and optional cancellation token.
	/// </summary>
	public ValueTask ForceValidate(CancellationToken token = default) => UpdateStateAsync(true, token);

	/// <inheritdoc/>
	protected override void OnAttachedTo(VisualElement bindable)
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);
		base.OnAttachedTo(bindable);
		View			= bindable;
		_isAttaching	= true;

		try
		{
			ReadAttachedValue();
		}
		catch
		{
			View = null;
			throw;
		}
		finally
		{
			_isAttaching = false;
		}

		bindable.PropertyChanged	+= OnViewPropertyChanged;
		_currentStatus			= ValidationFlags.ValidateOnAttaching;
		OnValidationPropertyChanged(this, null!, null!);
	}

	/// <inheritdoc/>
	protected override void OnDetachingFrom(VisualElement bindable)
	{
		bindable.PropertyChanged -= OnViewPropertyChanged;
		CancelValidation();

		View			= null;
		_currentStatus	= ValidationFlags.None;
		base.OnDetachingFrom(bindable);
	}

	private void ReadAttachedValue()
	{
		if (View is not { } view || string.IsNullOrEmpty(ValuePropertyName))
		{
			return;
		}

		Value = (T?)view.GetValue(GetValueProperty(view));
	}

	private async ValueTask UpdateStateAsync(bool forced = false, CancellationToken token = default)
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);
		token.ThrowIfCancellationRequested();

		if (View?.IsFocused == true && Flags.HasFlag(ValidationFlags.ForceMakeValidWhenFocused))
		{
			CancelValidation();
			IsValid = true;
		}
		else if (forced || (_currentStatus != ValidationFlags.None && Flags.HasFlag(_currentStatus)))
		{
			CancelValidation();
			using CancellationTokenSource cancellation = CancellationTokenSource.CreateLinkedTokenSource(token);
			_validationCancellation	= cancellation;
			IsRunning				= true;

			try
			{
				bool valid = await ValidateAsync(Decorate(Value), cancellation.Token);
				if (!cancellation.IsCancellationRequested)
				{
					IsValid = valid;
				}
			}
			catch (OperationCanceledException) when (cancellation.IsCancellationRequested)
			{
			}
			finally
			{
				if (_validationCancellation == cancellation)
				{
					_validationCancellation	= null;
					IsRunning				= false;
				}
			}
		}

		if (View is { } view)
		{
			VisualStateManager.GoToState(view, IsValid ? ValidVisualState : InvalidVisualState);
			view.Style = (IsValid ? ValidStyle : InvalidStyle) ?? view.Style;
		}
	}

	private void CancelValidation()
	{
		_validationCancellation?.Cancel();
		_validationCancellation	= null;
		IsRunning				= false;
	}

	/// <summary>
	/// Cancels pending validation and removes the control's property-change subscription.
	/// </summary>
	public void Dispose()
	{
		if (_isDisposed)
		{
			return;
		}

		if (View is { } view)
		{
			view.PropertyChanged -= OnViewPropertyChanged;
		}
		CancelValidation();

		View		= null;
		_isDisposed	= true;
		GC.SuppressFinalize(this);
	}

	#endregion

	#region Events

	private async void OnViewPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (View is not { } view || _isDisposed)
		{
			return;
		}

		if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
		{
			_currentStatus = view.IsFocused ? ValidationFlags.ValidateOnFocused : ValidationFlags.ValidateOnUnfocused;
			await UpdateStateAsync();
		}
		else if (!string.IsNullOrEmpty(ValuePropertyName) && e.PropertyName == GetValueProperty(view).PropertyName)
		{
			ReadAttachedValue();
		}
	}

	private static async void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
	{
		TrimSafeValidationBehavior<T> behavior = (TrimSafeValidationBehavior<T>)bindable;
		if (behavior._isAttaching || behavior._isDisposed)
		{
			return;
		}

		behavior._currentStatus = ValidationFlags.ValidateOnValueChanged;
		await behavior.UpdateStateAsync();
	}

	/// <summary>
	/// Re-evaluates the validation state when a validation option changes.
	/// </summary>
	protected static async void OnValidationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	{
		TrimSafeValidationBehavior<T> behavior = (TrimSafeValidationBehavior<T>)bindable;
		if (!behavior._isDisposed)
		{
			await behavior.UpdateStateAsync();
		}
	}

	#endregion
}

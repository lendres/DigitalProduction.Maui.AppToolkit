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
	public const string ValidVisualState = "Valid";
	public const string InvalidVisualState = "Invalid";

	public static readonly BindableProperty ValueProperty = BindableProperty.Create(
		nameof(Value), typeof(T), typeof(TrimSafeValidationBehavior<T>), default(T),
		propertyChanged: OnValueChanged);
	public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
		nameof(IsValid), typeof(bool), typeof(TrimSafeValidationBehavior<T>), true, BindingMode.OneWayToSource,
		propertyChanged: (b, _, value) => ((TrimSafeValidationBehavior<T>)b).IsNotValid = !(bool)value);
	public static readonly BindableProperty IsNotValidProperty = BindableProperty.Create(
		nameof(IsNotValid), typeof(bool), typeof(TrimSafeValidationBehavior<T>), false, BindingMode.OneWayToSource);
	public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(
		nameof(IsRunning), typeof(bool), typeof(TrimSafeValidationBehavior<T>), false, BindingMode.OneWayToSource);
	public static readonly BindableProperty ValidStyleProperty = BindableProperty.Create(
		nameof(ValidStyle), typeof(Style), typeof(TrimSafeValidationBehavior<T>), propertyChanged: OnValidationPropertyChanged);
	public static readonly BindableProperty InvalidStyleProperty = BindableProperty.Create(
		nameof(InvalidStyle), typeof(Style), typeof(TrimSafeValidationBehavior<T>), propertyChanged: OnValidationPropertyChanged);
	public static readonly BindableProperty FlagsProperty = BindableProperty.Create(
		nameof(Flags), typeof(ValidationFlags), typeof(TrimSafeValidationBehavior<T>),
		ValidationFlags.ValidateOnUnfocused | ValidationFlags.ForceMakeValidWhenFocused,
		propertyChanged: OnValidationPropertyChanged);
	public static readonly BindableProperty ValuePropertyNameProperty = BindableProperty.Create(
		nameof(ValuePropertyName), typeof(string), typeof(TrimSafeValidationBehavior<T>), "Text",
		propertyChanged: (b, _, _) => ((TrimSafeValidationBehavior<T>)b).ReadAttachedValue());
	public static readonly BindableProperty ForceValidateCommandProperty = BindableProperty.Create(
		nameof(ForceValidateCommand), typeof(ICommand), typeof(TrimSafeValidationBehavior<T>),
		defaultBindingMode: BindingMode.OneWayToSource,
		defaultValueCreator: b => new Command<CancellationToken>(async token => await ((TrimSafeValidationBehavior<T>)b).ForceValidate(token)));

	ValidationFlags currentStatus;
	CancellationTokenSource? validationCancellation;
	bool isAttaching;
	bool isDisposed;

	protected VisualElement? View { get; private set; }
	public T? Value { get => (T?)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
	public bool IsValid { get => (bool)GetValue(IsValidProperty); set => SetValue(IsValidProperty, value); }
	public bool IsNotValid { get => (bool)GetValue(IsNotValidProperty); set => SetValue(IsNotValidProperty, value); }
	public bool IsRunning { get => (bool)GetValue(IsRunningProperty); set => SetValue(IsRunningProperty, value); }
	public Style? ValidStyle { get => (Style?)GetValue(ValidStyleProperty); set => SetValue(ValidStyleProperty, value); }
	public Style? InvalidStyle { get => (Style?)GetValue(InvalidStyleProperty); set => SetValue(InvalidStyleProperty, value); }
	public ValidationFlags Flags { get => (ValidationFlags)GetValue(FlagsProperty); set => SetValue(FlagsProperty, value); }
	public string ValuePropertyName { get => (string)GetValue(ValuePropertyNameProperty); set => SetValue(ValuePropertyNameProperty, value); }
	public ICommand ForceValidateCommand { get => (ICommand)GetValue(ForceValidateCommandProperty); set => SetValue(ForceValidateCommandProperty, value); }

	/// <summary>Returns the source property directly, without reflection or a string-path binding.</summary>
	protected virtual BindableProperty GetValueProperty(VisualElement view) => (view, ValuePropertyName) switch
	{
		(InputView, "Text") => InputView.TextProperty,
		(Label, "Text") => Label.TextProperty,
		_ => throw new NotSupportedException("Use a compiled Value binding or override GetValueProperty for this control/property.")
	};

	protected virtual T? Decorate(T? value) => value;
	protected abstract ValueTask<bool> ValidateAsync(T? value, CancellationToken token);

	public ValueTask ForceValidate(CancellationToken token = default) => UpdateStateAsync(true, token);

	protected override void OnAttachedTo(VisualElement bindable)
	{
		ObjectDisposedException.ThrowIf(isDisposed, this);
		base.OnAttachedTo(bindable);
		View = bindable;
		isAttaching = true;
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
			isAttaching = false;
		}
		bindable.PropertyChanged += OnViewPropertyChanged;
		currentStatus = ValidationFlags.ValidateOnAttaching;
		OnValidationPropertyChanged(this, null!, null!);
	}

	protected override void OnDetachingFrom(VisualElement bindable)
	{
		bindable.PropertyChanged -= OnViewPropertyChanged;
		CancelValidation();
		View = null;
		currentStatus = ValidationFlags.None;
		base.OnDetachingFrom(bindable);
	}

	async void OnViewPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (View is not { } view || isDisposed)
			return;
		if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
		{
			currentStatus = view.IsFocused ? ValidationFlags.ValidateOnFocused : ValidationFlags.ValidateOnUnfocused;
			await UpdateStateAsync();
		}
		else if (!string.IsNullOrEmpty(ValuePropertyName) && e.PropertyName == GetValueProperty(view).PropertyName)
		{
			ReadAttachedValue();
		}
	}

	void ReadAttachedValue()
	{
		if (View is not { } view || string.IsNullOrEmpty(ValuePropertyName))
			return;
		Value = (T?)view.GetValue(GetValueProperty(view));
	}

	static async void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var behavior = (TrimSafeValidationBehavior<T>)bindable;
		if (behavior.isAttaching || behavior.isDisposed)
			return;
		behavior.currentStatus = ValidationFlags.ValidateOnValueChanged;
		await behavior.UpdateStateAsync();
	}

	protected static async void OnValidationPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var behavior = (TrimSafeValidationBehavior<T>)bindable;
		if (!behavior.isDisposed)
			await behavior.UpdateStateAsync();
	}

	async ValueTask UpdateStateAsync(bool forced = false, CancellationToken token = default)
	{
		ObjectDisposedException.ThrowIf(isDisposed, this);
		token.ThrowIfCancellationRequested();
		if (View?.IsFocused == true && Flags.HasFlag(ValidationFlags.ForceMakeValidWhenFocused))
		{
			CancelValidation();
			IsValid = true;
		}
		else if (forced || (currentStatus != ValidationFlags.None && Flags.HasFlag(currentStatus)))
		{
			CancelValidation();
			using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(token);
			validationCancellation = cancellation;
			IsRunning = true;
			try
			{
				var valid = await ValidateAsync(Decorate(Value), cancellation.Token);
				if (!cancellation.IsCancellationRequested)
					IsValid = valid;
			}
			catch (OperationCanceledException) when (cancellation.IsCancellationRequested) { }
			finally
			{
				if (validationCancellation == cancellation)
				{
					validationCancellation = null;
					IsRunning = false;
				}
			}
		}
		if (View is { } view)
		{
			VisualStateManager.GoToState(view, IsValid ? ValidVisualState : InvalidVisualState);
			view.Style = (IsValid ? ValidStyle : InvalidStyle) ?? view.Style;
		}
	}

	void CancelValidation()
	{
		validationCancellation?.Cancel();
		validationCancellation = null;
		IsRunning = false;
	}

	public void Dispose()
	{
		if (isDisposed)
			return;
		if (View is { } view)
			view.PropertyChanged -= OnViewPropertyChanged;
		CancelValidation();
		View = null;
		isDisposed = true;
		GC.SuppressFinalize(this);
	}
}

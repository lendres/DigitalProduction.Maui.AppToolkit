using CommunityToolkit.Mvvm.ComponentModel;

namespace DigitalProduction.Validation;

/// <summary>
/// Validatable object template.
/// </summary>
/// <typeparam name="T">Type of object to validate.</typeparam>
/// <remarks>
/// Based on:
/// https://github.com/dotnet-architecture/eshop-mobile-client
/// Copyright (c) 2020 .NET Application Architecture - Reference Apps
/// </remarks>
public class ValidatableObject<T> : ObservableObject, IValidity
{
	#region Fields

	private     IEnumerable<string>     _errors			= Enumerable.Empty<string>();
    private		bool					_isValid		= true;
    private		T?						_value;

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public ValidatableObject()
	{
	}

	#endregion

	#region Properties

	public List<IValidationRule<T>> Validations { get; } = new();

    public IEnumerable<string> Errors
    {
        get => _errors;
        private set => SetProperty(ref _errors, value);
    }

    public bool IsValid
    {
        get => _isValid;
        private set => SetProperty(ref _isValid, value);
    }

    public T? Value
    {
        get => _value;
        set => SetProperty(ref _value, value);
    }

	#endregion

	#region Methods

	public bool Validate()
    {
        Errors = Validations
            ?.Where(v => !v.Check(Value))
            ?.Select(v => v.ValidationMessage)
            ?.ToArray()
            ?? Enumerable.Empty<string>();

        IsValid = !Errors.Any();

        return IsValid;
    }

	#endregion
}
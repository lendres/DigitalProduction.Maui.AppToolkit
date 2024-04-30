using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DigitalProduction.Exceptions;

/// <summary>
/// The exception that is thrown a field is <see langword="null"/> reference (<see langword="Nothing"/> in Visual Basic).
/// </summary>
[Serializable]
[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
public class StringIsNullOrEmptyException : NullReferenceException
{
	// Creates a new FieldNullException with its message
	// string set to a default message explaining an argument was null.
	public StringIsNullOrEmptyException()
	{
	}

	public StringIsNullOrEmptyException(string? message) :
		base(message)
	{
	}

	public StringIsNullOrEmptyException(string? message, Exception? innerException) :
		base(message, innerException)
	{
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The reference type argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (string.IsNullOrEmpty(argument))
		{
			Throw(paramName);
		}
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The reference type argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowIfNullOrEmpty(string message, [NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (string.IsNullOrEmpty(argument))
		{
			Throw(paramName, message);
		}
	}

	[DoesNotReturn]
	internal static void Throw(string? paramName) =>
		throw new FieldNullException(paramName);

	[DoesNotReturn]
	internal static void Throw(string? paramName, string message) =>
		throw new FieldNullException(message + Environment.NewLine + "Parameter: " + paramName);
}
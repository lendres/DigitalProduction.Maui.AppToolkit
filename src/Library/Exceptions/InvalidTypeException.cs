using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DigitalProduction.Exceptions;

/// <summary>
/// The exception that is thrown a field is <see langword="null"/> reference (<see langword="Nothing"/> in Visual Basic).
/// </summary>
[Serializable]
[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
public class InvalidTypeException : Exception
{
	// Creates a new FieldNullException with its message
	// string set to a default message explaining an argument was null.
	public InvalidTypeException()
	{
	}

	public InvalidTypeException(string? message) :
		base(message)
	{
	}

	public InvalidTypeException(string? message, Exception? innerException) :
		base(message, innerException)
	{
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The reference type argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowNotType(Type type, [NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (argument?.GetType() != type)
		{
			Throw(paramName);
		}
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The reference type argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowIfNull(string message, Type type, [NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (argument?.GetType() != type)
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
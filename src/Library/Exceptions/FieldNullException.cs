using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DigitalProduction.Exceptions;

/// <summary>
/// The exception that is thrown a field is <see langword="null"/> reference (<see langword="Nothing"/> in Visual Basic).
/// </summary>
[Serializable]
[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
public class FieldNullException : NullReferenceException
{
	// Creates a new FieldNullException with its message
	// string set to a default message explaining an argument was null.
	public FieldNullException()
	{
	}

	public FieldNullException(string? message) :
		base(message)
	{
	}

	public FieldNullException(string? message, Exception? innerException) :
		base(message, innerException)
	{
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The reference type argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (argument is null)
		{
			Throw(paramName);
		}
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The reference type argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowIfNull(string message, [NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (argument is null)
		{
			Throw(paramName, message);
		}
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The pointer argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static unsafe void ThrowIfNull([NotNull] void* argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (argument is null)
		{
			Throw(paramName);
		}
	}

	/// <summary>Throws an <see cref="FieldNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The pointer argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	internal static unsafe void ThrowIfNull(IntPtr argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (argument == IntPtr.Zero)
		{
			Throw(paramName);
		}
	}

	[DoesNotReturn]
	internal static void Throw(string? paramName) =>
		throw new FieldNullException(paramName);

	[DoesNotReturn]
	internal static void Throw(string? paramName, string message) =>
		throw new FieldNullException(message + Environment.NewLine + "Parameter: " + paramName);
}
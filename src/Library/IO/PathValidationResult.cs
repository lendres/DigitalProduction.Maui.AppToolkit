using System.ComponentModel;
using DigitalProduction.Reflection;

namespace DigitalProduction.IO;

/// <summary>
/// Enumeration that specifies the result of checking a file name for validity.
/// 
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// 
/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
/// </summary>
public enum PathValidationResult
{
	/// <summary>File name is valid.</summary>
	[Message("")]
	[Description("Valid file name.")]
	Valid,

	/// <summary>A string of zero length (or all spaces) was specified.</summary>
	[Message("A file name was not specified.")]
	[Description("A file name was not specified.")]
	ZeroLength,

	/// <summary>File name is too long.</summary>
	[Message("The file name exceeds the allowed length.")]
	[Description("The file name exceeds the allowed length.")]
	TooLong,

	/// <summary>Characters not allowed by the system were used.</summary>
	[Message("Some of characters are not valid.")]
	[Description("Some of characters are not valid.")]
	InvalidCharacters,

	/// <summary>Path does not exist.</summary>
	[Message("The path does not exist.")]
	[Description("The path does not exist.")]
	PathDoesNotExist,

	/// <summary>File name was not provided / file name starts with a "dot."</summary>
	[Message("File name was not provided / file name starts with a \"dot.\"")]
	[Description("File name was not provided / file name starts with a \"dot.\"")]
	FileNameNotProvided,
			
	/// <summary>The first few characters must not match any known device names.</summary>
	[Message("The file name begins with a \".\" or a device name.  E.g. AUX, LPT1")]
	[Description("The file name begins with a \".\" or a device name.  E.g. AUX, LPT1")]
	DeviceName

} // End enum.
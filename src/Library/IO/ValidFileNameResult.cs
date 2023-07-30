using System.ComponentModel;

namespace DigitalProduction.IO
{
	/// <summary>
	/// Enumeration that specifies the result of checking a file name for validity.
    /// 
	/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
	/// 
	/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
	/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
	/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
	/// </summary>
	public enum ValidFileNameResult
	{
		/// <summary>File name is valid.</summary>
		[Description("Valid file name.")]
		Valid,

		/// <summary>A string of zero length (or all spaces) was specified.</summary>
		[Description("A file name was not specified.")]
		ZeroLength,

		/// <summary>File name is too long.</summary>
		[Description("The file name exceeds the allowed length.")]
		TooLong,

		/// <summary>Characters not allowed by the system were used.</summary>
		[Description("Some of characters are not valid.")]
		InvalidCharacters,

		/// <summary>Path does not exist.</summary>
		[Description("The path does not exist.")]
		PathDoesNotExist,

		/// <summary>File name was not provided / file name starts with a "dot."</summary>
		[Description("File name was not provided / file name starts with a \"dot.\"")]
		FileNameNotProvided,
			
		/// <summary>The first few characters must not match any known device names.</summary>
		[Description("The file name begins with a \".\" or a device name.  E.g. AUX, LPT1")]
		DeviceName

	} // End enum.
} // End namespace.
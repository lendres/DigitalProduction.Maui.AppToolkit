using System.ComponentModel;
using DigitalProduction.Reflection;

namespace DigitalProduction.Maui.Behaviors;

/// <summary>
/// Add summary here.
/// 
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// 
/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
/// </summary>
public enum PathErrorType
{
	/// <summary>File not found error.</summary>
	[Message("The directory was not found.")]
	[Description("Directory Not Found")]
	DirectoryNotFound,

	/// <summary>File not found error.</summary>
	[Message("The specified file was not found.")]
	[Description("File Not Found")]
	FileNotFound,

	/// <summary>File not found error.</summary>
	[Message("The specified file is not valid.")]
	[Description("Invalid File")]
	InvalidFile,

	/// <summary>No errors.</summary>
	[Message("")]
	[Description("None")]
	None,

	/// <summary>The number of types/items in the enumeration.</summary>
	[Description("Length")]
	Length

} // End enum.
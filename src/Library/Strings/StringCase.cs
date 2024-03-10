using System.ComponentModel;

namespace DigitalProduction.Strings;

/// <summary>
/// Used to specify the case of string for things such as conversion, comparison, et cetera.
/// 
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// 
/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
/// </summary>
public enum StringCase
{
	/// <summary>None specified / do not convert.</summary>
	[Description("None")]
	None,

	/// <summary>Lower case.</summary>
	[Description("Lower Case")]
	LowerCase,

	/// <summary>Title case.</summary>
	[Description("Title Case")]
	TitleCase,

	/// <summary>Upper case.</summary>
	[Description("Upper Case")]
	UpperCase,

	/// <summary>The number of types/items in the enumeration.</summary>
	[Description("Length")]
	Length

} // End enum.
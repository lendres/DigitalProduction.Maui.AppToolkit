using System.ComponentModel;

namespace DigitalProduction.Enumerations;

/// <summary>
/// Add summary here.
/// 
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// 
/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
/// </summary>
public enum ObjectSource
{
	/// <summary>An existing object.</summary>
	[Description("Existing")]
	Existing,

	/// <summary>A new object.</summary>
	[Description("New")]
	New,

	/// <summary>The number of types/items in the enumeration.</summary>
	[Description("Length")]
	Length

} // End enum.
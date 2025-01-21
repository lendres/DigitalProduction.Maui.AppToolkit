using System.ComponentModel;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Add summary here.
/// 
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// 
/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
/// </summary>
public enum TestingType
{
	/// <summary>Type 1.</summary>
	[Description("Type 1")]
	Type1,

	/// <summary>Type 2.</summary>
	[Description("Type 2")]
	Type2,

	/// <summary>Type 3.</summary>
	[Description("Type 3")]
	Type3

} // End enum.

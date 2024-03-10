using System.ComponentModel;
using DigitalProduction.Reflection;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// Add summary here.
	///
	/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
	///
	/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
	/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
	/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
	/// </summary>
	public enum AirPlaneType
	{
		[Description("An European short- to medium-range twin-engine narrow-body airliner.")]
		[AlternateNames("320", "Airbus 320")]
		Airbus320,

		[Description("An European ")]
		[AlternateNames("380", "Airbus 380")]
		Airbus380,

		[Description("An American short- to medium-range twin-engine narrow-body airliner.")]
		[AlternateNames("737", "Boeing 737")]
		Boeing737,

		[Description("An American wide-body commercial jet airliner and cargo aircraft. ")]
		[AlternateNames("747", "Boeing 747")]
		Boeing747,

		/// <summary>The number of types/items in the enumeration.</summary>
		[Description("Length")]
		Length

	} // End enum.
} // End namespace.
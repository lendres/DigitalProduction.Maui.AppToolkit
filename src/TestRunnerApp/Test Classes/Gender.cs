using System.ComponentModel;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// Gender of a person.
	/// </summary>
	public enum Gender
	{
		/// <summary>A woman.</summary>
		[Description("Woman")]
		Female,

		/// <summary>A man.</summary>
		[Description("Man")]
		Male,

		/// <summary>The number of types/items in the enumeration.</summary>
		[Description("Length")]
		Length

	} // End enum.
} // End namespace.
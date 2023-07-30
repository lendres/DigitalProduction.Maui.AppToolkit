using System.ComponentModel;

namespace DigitalProduction.Mathmatics
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
	public enum NormalizationType
	{
		/// <summary>Euclidian.</summary>
		[Description("Euclidean")]
		Euclidean,

		/// <summary>The maximum value is specified as input.</summary>
		[Description("MaxValueIsHalfPi")]
		MaxValueIsHalfPi,

		/// <summary>All numbers are divided by the maximum value.</summary>
		[Description("MaxValueIsOne")]
		MaxValueIsOne,

		/// <summary>All numbers are divided by the maximum absolute value.</summary>
		[Description("MaxAbsoluteValueIsOne")]
		MaxAbsoluteValueIsOne

	} // End enum.
} // End namespace.
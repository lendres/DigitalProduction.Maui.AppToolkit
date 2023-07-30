using System.ComponentModel;

namespace DigitalProduction.Reflection
{
	/// <summary>
	/// Types of alternate names.
	///
	/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
	///
	/// We don't use the "Length" enumeration in this case because this enumeration type can be used in the control designer
	/// and we don't want it appearing, therefore allowing someone to select it even though it is not a valid design option.
	/// </summary>
	public enum AlternateNameType
	{
		/// <summary>Long name.</summary>
		[Description("Long Name")]
		LongName,

		/// <summary>Short name.</summary>
		[Description("Short Name")]
		ShortName,

	} // End enum.
} // End namespace.
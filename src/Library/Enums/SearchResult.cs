using System.ComponentModel;

namespace DigitalProduction.Maui.Enums;

/// <summary>
/// Specifies the user's choice when prompted to save changes before continuing. This enumeration is used to 
/// represent the different options available to the user, such as saving changes and continuing, continuing without
/// saving, or canceling the operation.
///
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// </summary>
public enum SearchResult
{
	/// <summary>No items found.</summary>
	[Description("No Items Found")]
    NoItemsFound,

	/// <summary>Next item found.</summary>
	[Description("Next Item Found")]
	NextItemFound,

	/// <summary>No more found items.</summary>
	[Description("No More Found Items")]
	NoMoreFoundItems
}
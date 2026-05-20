using System.ComponentModel;
using DigitalProduction.Maui.Attributes;

namespace DigitalProduction.Maui.Views;

/// <summary>
/// Specifies the user's choice when prompted to save changes before continuing. This enumeration is used to 
/// represent the different options available to the user, such as saving changes and continuing, continuing without
/// saving, or canceling the operation.
///
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// </summary>
public enum ButtonChoice
{
	/// <summary>Button 1.</summary>
	[Description("Button 1")]
    Button1,

	/// <summary>Button 2.</summary>
	[Description("Button 2")]
	Button2,

	/// <summary>Button 3.</summary>
	[Description("Button 3")]
    Button3,
}
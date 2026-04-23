using System.ComponentModel;

namespace DigitalProduction.Maui.UI;

/// <summary>
/// Specifies the user's choice when prompted to save changes before closing an application or document. This enumeration
/// is used to represent the different options available to the user, such as saving changes and exiting, exiting without
/// saving, or canceling the close operation.
///
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// </summary>
internal enum CloseChoice
{
	/// <summary>Save changes and exit the application.</summary>
	[Description("Save and Exit")]
    SaveAndExit,

	/// <summary>Exit the application without saving changes.</summary>
	[Description("Exit without Saving")]
	ExitWithoutSaving,

	/// <summary>Cancel the close operation.</summary>
	[Description("Cancel")]
    Cancel
}
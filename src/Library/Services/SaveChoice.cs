using System.ComponentModel;

namespace DigitalProduction.Maui.Services;

/// <summary>
/// Specifies the user's choice when prompted to save changes before continuing. This enumeration is used to 
/// represent the different options available to the user, such as saving changes and continuing, continuing without
/// saving, or canceling the operation.
///
/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
/// </summary>
public enum SaveChoice
{
	/// <summary>Cancel the close operation.</summary>
	[Description("Cancel")]
    Cancel,

	/// <summary>Exit the application without saving changes.</summary>
	[Description("Continue without Saving")]
	ContinueWithoutSaving,

	/// <summary>Save changes and exit the application.</summary>
	[Description("Save and Continue")]
    SaveAndContinue,

	/// <summary>Save changes and exit the application.</summary>
	[Description("Saving not Requried")]
    SavingNotRequired
}
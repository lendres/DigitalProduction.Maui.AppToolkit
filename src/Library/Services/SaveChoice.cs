using System.ComponentModel;
using DigitalProduction.Maui.Attributes;

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
	/// <summary>Cancel the operation.</summary>
	[ControlText("Cancel")]
	[Description("Cancel")]
    Cancel,

	/// <summary>Continue without saving changes.</summary>
	[ControlText("Continue without Saving")]
	[Description("Continue without Saving")]
	ContinueWithoutSaving,

	/// <summary>Save changes and continue.</summary>
	[ControlText("Save and Continue")]
	[Description("Save and Continue")]
    SaveAndContinue,

	/// <summary>Save is not required.</summary>
	[ControlText("Save not Required")]
	[Description("Save not Required")]
    SaveNotRequired
}
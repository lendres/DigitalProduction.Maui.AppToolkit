namespace DigitalProduction.Maui.Attributes;

/// <summary>
/// An attribute to add additional names to a class, structure, or enumeration.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class ControlTextAttribute : Attribute
{
	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public ControlTextAttribute()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public ControlTextAttribute(string text)
	{
		Text = text;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Short name.
	/// </summary>
	public string Text { get; private set; } = "";

	#endregion

} // End class.

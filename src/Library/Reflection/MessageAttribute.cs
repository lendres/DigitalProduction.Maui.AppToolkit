namespace DigitalProduction.Reflection;

/// <summary>
/// An attribute to add additional names to a class, structure, or enumeration.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
public class MessageAttribute : Attribute
{
	#region Fields
	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public MessageAttribute()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public MessageAttribute(string message)
	{
		Message = message;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Short name.
	/// </summary>
	public string Message { get; private set; } = "";
	
	#endregion

	#region Methods
	#endregion

} // End class.
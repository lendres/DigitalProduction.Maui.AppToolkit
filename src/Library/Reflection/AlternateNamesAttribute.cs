namespace DigitalProduction.Reflection;

/// <summary>
/// An attribute to add additional names to a class, structure, or enumeration.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
public class AlternateNamesAttribute : Attribute
{
	#region Fields

	private readonly string[]          _names					= new string[2];

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public AlternateNamesAttribute()
	{
	}

	/// <summary>
	/// Default constructor.
	/// </summary>
	public AlternateNamesAttribute(string shortName, string longName)
	{
		_names[(int)AlternateNameType.ShortName]	= shortName;
		_names[(int)AlternateNameType.LongName]		= longName;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Short name.
	/// </summary>
	public string ShortName
	{
		get
		{
			return _names[(int)AlternateNameType.ShortName];
		}

		set
		{
			_names[(int)AlternateNameType.ShortName] = value;
		}
	}

	/// <summary>
	/// Long name.
	/// </summary>
	public string LongName
	{
		get
		{
			return _names[(int)AlternateNameType.LongName];
		}

		set
		{
			_names[(int)AlternateNameType.LongName] = value;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Get a name by using the enumeration.
	/// </summary>
	/// <param name="nameType">Name type to get.</param>
	public string GetName(AlternateNameType nameType)
	{
		return _names[(int)nameType];
	}

	#endregion

} // End class.
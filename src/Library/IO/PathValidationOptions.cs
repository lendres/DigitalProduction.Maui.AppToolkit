namespace DigitalProduction.IO;

/// <summary>
/// Options for controlling what determines if a file name is valid or not.
/// </summary>
public class PathValidationOptions
{
	#region Fields

	/// <summary>Specifies if the path is required to exist for the file name to be valid.  The default is false.</summary>
	public bool		RequirePathToExist;

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public PathValidationOptions()
	{
	}

	#endregion

} // End class.
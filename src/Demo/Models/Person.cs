namespace DigitalProduction.Demo;

/// <summary>
/// A person.
/// </summary>
public class Person
{
	#region Construction

	/// <summary>
	/// Default constructor.  Required for serialization.
	/// </summary>
	public Person()
	{
	}

	#endregion

	#region Properties

	/// <summary>
	/// First name.
	/// </summary>
	public string FirstName { get; set; } = "";

	/// <summary>
	/// Last name.
	/// </summary>
	public string LastName { get; set; } = "";

	/// <summary>
	/// Age.
	/// </summary>
	public int Age { get; set; } = 0;

	#endregion

	#region Methods

	public bool Matches(string search)
	{
		if (string.IsNullOrEmpty(search))
		{
			return false;
		}

		return FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
			   LastName.Contains(search, StringComparison.OrdinalIgnoreCase);
	}

	#endregion

} // End class.
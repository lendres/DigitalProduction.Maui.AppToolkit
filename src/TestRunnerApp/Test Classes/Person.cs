using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

/// <summary>
/// A person.
/// </summary>
public class Person
{
	#region Construction

	/// <summary>
	/// Constructor to populate fields.
	/// </summary>
	public Person(string name, int age, Gender gender)
	{
		Name	= name;
		Age		= age;
		Gender	= gender;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Name.
	/// </summary>
	[XmlAttribute("name")]
	public string Name { get; set; } = "";

	/// <summary>
	/// Age.
	/// </summary>
	[XmlAttribute("age")]
	public int Age { get; set; } = 0;

	/// <summary>
	/// Gender.
	/// </summary>
	[XmlAttribute("gender")]
	public Gender Gender  { get; set; } = Gender.Female;

	#endregion

} // End class.
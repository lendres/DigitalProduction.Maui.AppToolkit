using DigitalProduction.ComponentModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

/// <summary>
/// A family.
/// </summary>
[XmlRoot("family")]
[DisplayName("Family Members")]
[Alias("Family Members")]
[Alias("Relatives")]
[Description("A group of related people.")]
public class Family
{
	#region Construction
	#endregion

	#region Properties

	/// <summary>
	/// Number of people in the family.
	/// </summary>
	[XmlIgnore()]
	public int NumberOfMembers { get => Members.Count; }

	/// <summary>
	/// Members of the family.
	/// </summary>
	[XmlArray("members"), XmlArrayItem("member")]
	public List<Person> Members { get; set; } = new();

	#endregion

	#region Methods

	/// <summary>
	/// Find a person in the family by name.
	/// </summary>
	/// <param name="name">Name of the Person to find.</param>
	/// <returns>The first Person in the list with the specified name.</returns>
	public Person? GetPerson(string name)
	{
		return Members.Find(x => x.Name == name);
	}

	#endregion

} // End class.
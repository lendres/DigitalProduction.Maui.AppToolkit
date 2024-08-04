using DigitalProduction.XML.Serialization;
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

/// <summary>
/// A generic company.
/// </summary>
[XmlRoot("company")]
public class Company
{
	#region Construction
	#endregion

	#region Properties

	/// <summary>
	/// Name.
	/// </summary>
	[XmlAttribute("name")]
	public string Name { get;  set; } = "";

	/// <summary>
	/// Number of people in the family.
	/// </summary>
	[XmlIgnore()]
	public int NumberOfEmployees { get => Employees.Count; }

	/// <summary>
	/// Employees.
	/// </summary>
	[XmlArray("employees"), XmlArrayItem("employee")]
	public List<Person> Employees { get; set; } = new();

	/// <summary>
	/// Assets.
	/// </summary>
	[XmlArray("assets"), XmlArrayItem("asset")]
	public List<Asset> Assets { get; set; } = new();

	#endregion

	#region Methods

	/// <summary>
	/// Find a person in the family by name.
	/// </summary>
	/// <param name="name">Name of the Person to find.</param>
	/// <returns>The first Person in the list with the specified name.</returns>
	public Person? GetEmployee(string name)
	{
		return Employees.Find(x => x.Name == name);
	}
	
	#endregion

	#region XML
	
	/// <summary>
	/// Create an instance from a file.
	/// </summary>
	/// <param name="path">The file to read from.</param>
	/// <returns>The deserialized file types.</returns>
	public static T? Deserialize<T>(string path) where T : Company
	{
		// Deserialize the object creating a new instance.  Then we set the path to the location the file was deserialized
		// from.  That way the file can be saved back to that location if required.
#if WINDOWS
		T? company		= Serialization.DeserializeObject<T>(path);
		return company;
#else
		return null;
#endif
	}

	/// <summary>
	/// Write this object to a file.  The Path must be set and represent a valid path or this method will throw an exception.
	/// </summary>
	/// <exception cref="InvalidOperationException">Thrown when the projects path is not set or not valid.</exception>
	public void Serialize(string path)
	{
		Serialization.SerializeObject(this, path);
	}

#endregion

} // End class.
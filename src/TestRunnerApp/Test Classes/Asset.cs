using System.Xml.Serialization;

namespace DigitalProduction.UnitTests;

/// <summary>
/// A company asset.
/// </summary>
public class Asset
{
	#region Construction

	/// <summary>
	/// Constructor.
	/// </summary>
	public Asset(string name, int value, string description)
	{
		Name			= name;
		Value			= value;
		Description	= description;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Name.
	/// </summary>
	[XmlAttribute("name")]
	public string Name { get; set; } = "";

	/// <summary>
	/// Value.
	/// </summary>
	[XmlAttribute("value")]
	public int Value { get; set; } = 0;

	/// <summary>
	/// Description.
	/// </summary>
	[XmlElement("description")]
	public string Description { get; set; } = "";

	#endregion

} // End class.
using System.Xml.Serialization;
using System.ComponentModel;

namespace DigitalProduction.UnitTests;

/// <summary>
/// An airline company.
/// </summary>
[XmlRoot("airline")]
[DisplayName("Airline")]
[Description("A company that owns and operates airplanes.")]
public class AirlineCompany : Company
{
	#region Construction
	#endregion

	#region Properties

	/// <summary>
	/// Number of planes the airline has available.
	/// </summary>
	[XmlAttribute("numberofplanes")]
	public int NumberOfPlanes { get; set; } = 0;

	#endregion

} // End class.
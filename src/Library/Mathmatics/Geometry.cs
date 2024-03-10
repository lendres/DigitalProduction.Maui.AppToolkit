namespace DigitalProduction.Mathmatics;

/// <summary>
/// 
/// </summary>
public static class Geometry
{
	#region Fields

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	static Geometry()
	{
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Area of a ring (circle with inner concentric circle subtracted).
	/// </summary>
	/// <param name="outerdiameter">Diameter of outer circle.</param>
	/// <param name="innerdiameter">Diameter of inner circle.</param>
	public static double AreaOfCircleByDiameter(double outerdiameter, double innerdiameter)
	{
		return System.Math.PI / 4 * (System.Math.Pow(outerdiameter, 2) - System.Math.Pow(innerdiameter, 2));
	}

	#endregion

} // End class.
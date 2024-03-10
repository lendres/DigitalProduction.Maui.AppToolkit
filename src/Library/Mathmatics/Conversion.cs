namespace DigitalProduction.Mathmatics;

/// <summary>
/// Class for converting units.
/// </summary>
public static class Conversion
{
	#region Fields

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	static Conversion()
	{
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	#region Angles

	/// <summary>
	/// Convert degrees to radians.
	/// </summary>
	/// <param name="angle">Angle to convert.</param>
	public static double DegreesToRadians(double angle)
	{
		return angle * System.Math.PI / 180.0;
	}

	/// <summary>
	/// Convert degrees to radians.
	/// </summary>
	/// <param name="angles">Angle to convert.</param>
	public static List<double> DegreesToRadians(List<double> angles)
	{
		int				count		= angles.Count;
		List<double>	output		= new List<double>(count);

		for (int i = 0; i < count; i++)
		{
			output.Add(angles[i] * System.Math.PI / 180.0);
		}
		return output;
	}

	/// <summary>
	/// Convert degrees to radians.
	/// </summary>
	/// <param name="angles">Angle to convert.</param>
	public static void DegreesToRadiansInPlace(List<double> angles)
	{
		int				count		= angles.Count;

		for (int i = 0; i < count; i++)
		{
			angles[i] *= System.Math.PI / 180.0;
		}
	}

	/// <summary>
	/// Convert radians to degrees.
	/// </summary>
	/// <param name="angle">Angle to convert.</param>
	public static double RadiansToDegrees(double angle)
	{
		return angle * 180.0 / System.Math.PI;
	}

	/// <summary>
	/// Convert radians to degrees.
	/// </summary>
	/// <param name="angles">Angle to convert.</param>
	public static List<double> RadiansToDegrees(List<double> angles)
	{
		int				count		= angles.Count;
		List<double>	output		= new List<double>(count);

		for (int i = 0; i < count; i++)
		{
			output.Add(angles[i] * 180.0 / System.Math.PI);
		}
		return output;
	}

	/// <summary>
	/// Convert radians to degrees.
	/// </summary>
	/// <param name="angles">Angle to convert.</param>
	public static void RadiansToDegreesInPlace(List<double> angles)
	{
		int				count		= angles.Count;

		for (int i = 0; i < count; i++)
		{
			angles[i] *= 180.0 / System.Math.PI;
		}
	}

	/// <summary>
	/// Convert radians to degrees.
	/// </summary>
	/// <param name="angles">Angle to convert.</param>
	public static List<double> RadiansToRevolutions(List<double> angles)
	{
		int				count		= angles.Count;
		List<double>	output		= new List<double>(count);

		for (int i = 0; i < count; i++)
		{
			output.Add(angles[i] / 2.0 / System.Math.PI);
		}
		return output;
	}

	/// <summary>
	/// Convert radians to degrees.
	/// </summary>
	/// <param name="angles">Angle to convert.</param>
	public static void RadiansToRevolutionsInPlace(List<double> angles)
	{
		int				count		= angles.Count;

		for (int i = 0; i < count; i++)
		{
			angles[i] /= 2.0 * System.Math.PI;
		}
	}

	#endregion

	#region Angular Velocity

	/// <summary>
	/// Convert RPM to Hertz (where 1 revolution in 1 second is 1 Hertz).  Hertz would be
	/// equivalent to 1 revolution per 1 second.
	/// </summary>
	/// <param name="rpm">Revolutions per minute.</param>
	public static double RpmToHertz(double rpm)
	{
		return rpm / 60.0;
	}

	#endregion

	#region Length

	/// <summary>
	/// Convert feet to inches.
	/// </summary>
	/// <param name="feet">Feet.</param>
	public static double FeetToInches(double feet)
	{
		return feet * 12.0;
	}

	/// <summary>
	/// Convert inches to feet.
	/// </summary>
	/// <param name="inches">Inches.</param>
	public static double InchesToFeet(double inches)
	{
		return inches / 12.0;
	}

	#endregion

	#region Vibration

	/// <summary>
	/// Convert frequency (Hertz) into period (seconds).
	/// </summary>
	/// <param name="frequency">Frequency in Hertz.</param>
	public static double FrequencyToPeriod(double frequency)
	{
		return 1.0 / frequency;
	}

	/// <summary>
	/// Convert frequency Hertz into period (seconds).
	/// </summary>
	/// <param name="period">Period in seconds.</param>
	public static double PeriodToFrequency(double period)
	{
		return 1.0 / period;
	}

	/// <summary>
	/// Convert angular frequency (radians) to frequency (Hertz).
	/// </summary>
	/// <param name="angularFrequency">Angular frequency in radians.</param>
	public static double AngularFrequencyToFrequency(double angularFrequency)
	{
		return angularFrequency / 2 / System.Math.PI;
	}

	/// <summary>
	/// Convert frequency (Hertz) to angular frequency (radians).
	/// </summary>
	/// <param name="frequency">Angular frequency in radians.</param>
	public static double FrequencyToAngularFrequency(double frequency)
	{
		return frequency * 2.0 * System.Math.PI;
	}

	/// <summary>
	/// Convert angular frequency (radians) to frequency (Hertz).
	/// </summary>
	/// <param name="angularFrequency">Angular frequency in radians.</param>
	public static double AngularFrequencyToPeriod(double angularFrequency)
	{
		return 2.0 * System.Math.PI / angularFrequency;
	}

	/// <summary>
	/// Convert angular frequency (radians) to frequency (Hertz).
	/// </summary>
	/// <param name="period">Angular frequency in radians.</param>
	public static double PeriodToAngularFrequency(double period)
	{
		return 2.0 * System.Math.PI / period;
	}

	#endregion

	#region Rounding

	/// <summary>
	/// Round down the integer part (characteristic) of a number.
	/// </summary>
	/// <param name="value">Value to round.</param>
	/// <param name="position">Integer position to round to.</param>
	public static int FloorToIntegerPart(double value, int position)
	{
		return (int)System.Math.Floor(value / position) * position;
	}

	/// <summary>
	/// Round up the integer part (characteristic) of a number.
	/// </summary>
	/// <param name="value">Value to round.</param>
	/// <param name="position">Integer position to round to.</param>
	public static int CeilingToIntegerPart(double value, int position)
	{
		return (int)System.Math.Ceiling(value / position) * position;
	}

	#endregion

	#endregion

} // End class.
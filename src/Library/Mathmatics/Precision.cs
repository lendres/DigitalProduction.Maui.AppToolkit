namespace DigitalProduction.Mathmatics;

/// <summary>
/// Math utilities.
/// </summary>
public static class Precision
{
	#region Fields

	/// <summary>The epsilon, threshold using for determining whether two numbers are equal or not.</summary>
	private static double _epsilon = 1e-10;

	#endregion

	#region Properties

	/// <summary>
	/// The allowed threshold that are numbers are allowed to be different, but still considered equal.
	/// </summary>
	public static double Epsilon
	{
		get
		{
			return Precision._epsilon;
		}

		set
		{
			Precision._epsilon = value;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Determines if the input is zero within the allotted precision.
	/// </summary>
	/// <param name="val">The value to check.</param>
	public static bool IsZero(double val)
	{
		return IsZero(val, _epsilon);
	}

	/// <summary>
	/// Determines if the input is zero within the allotted precision.
	/// </summary>
	/// <param name="val">The value to check.</param>
	/// <param name="epsilon">The specified precision.</param>
	public static bool IsZero(double val, double epsilon)
	{
		if (System.Math.Abs(val) < epsilon)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Determines if the first input is greater than the second input within the allotted precision.
	/// </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	public static bool LessThan(double value1, double value2)
	{
		return LessThanZero(value1 - value2, _epsilon);
	}

	/// <summary>
	/// Determines if the first input is less than the second input within the allotted precision.
	/// </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The specified precision.</param>
	public static bool LessThan(double value1, double value2, double epsilon)
	{
		return LessThanZero(value1 - value2, epsilon);
	}

	/// <summary>
	/// Determines if the input is less than zero within the allotted precision.
	/// </summary>
	/// <param name="val">The value to check.</param>
	public static bool LessThanZero(double val)
	{
		return LessThanZero(val, _epsilon);
	}

	/// <summary>
	/// Determines if the input is less than zero within the allotted precision.
	/// </summary>
	/// <param name="val">The value to check.</param>
	/// <param name="epsilon">The specified precision.</param>
	public static bool LessThanZero(double val, double epsilon)
	{
		return val < -epsilon;
	}

	/// <summary>
	/// Determines if the first input is greater than the second input within the allotted precision.
	/// </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	public static bool GreaterThan(double value1, double value2)
	{
		return GreaterThanZero(value1 - value2, _epsilon);
	}

	/// <summary>
	/// Determines if the first input is greater than the second input within the allotted precision.
	/// </summary>
	/// <param name="value1">The first value.</param>
	/// <param name="value2">The second value.</param>
	/// <param name="epsilon">The specified precision.</param>
	public static bool GreaterThan(double value1, double value2, double epsilon)
	{
		return GreaterThanZero(value1 - value2, epsilon);
	}

	/// <summary>
	/// Determines if the input is greater than zero within the allotted precision.
	/// </summary>
	/// <param name="val">The value to check.</param>
	public static bool GreaterThanZero(double val)
	{
		return GreaterThanZero(val, _epsilon);
	}

	/// <summary>
	/// Determines if the input is greater than zero within the allotted precision.
	/// </summary>
	/// <param name="val">The value to check.</param>
	/// <param name="epsilon">The specified precision.</param>
	public static bool GreaterThanZero(double val, double epsilon)
	{
		return val > epsilon;
	}

	#region Equal and Not Equal

	/// <summary>
	/// Determines two numbers are equal within the default precision.
	/// </summary>
	/// <param name="val1">The first value.</param>
	/// <param name="val2">The second value.</param>
	public static bool Equal(double val1, double val2)
	{
		return Equal(val1, val2, _epsilon);
	}

	/// <summary>
	/// Determines two numbers are equal within the specified precision.
	/// </summary>
	/// <param name="val1">The first value.</param>
	/// <param name="val2">The second value.</param>
	/// <param name="epsilon">Precision required to consider the two value equal.</param>
	public static bool Equal(double val1, double val2, double epsilon)
	{
		double diff = System.Math.Abs(val1 - val2);
		return diff <= epsilon;
	}

	/// <summary>
	/// Determines two numbers are equal within the default precision.
	/// </summary>
	/// <param name="val1">The first value.</param>
	/// <param name="val2">The second value.</param>
	public static bool NotEqual(double val1, double val2)
	{
		return NotEqual(val1, val2, _epsilon);
	}

	/// <summary>
	/// Determines two numbers are equal within the specified precision.
	/// </summary>
	/// <param name="val1">The first value.</param>
	/// <param name="val2">The second value.</param>
	/// <param name="epsilon">Precision required to consider the two value equal.</param>
	public static bool NotEqual(double val1, double val2, double epsilon)
	{
		return !Equal(val1, val2, epsilon);
	}

	#endregion

	#region Floor and Ceiling

	/// <summary>
	/// Round up to the specified decimal place.
	/// </summary>
	/// <param name="val">Value to round.</param>
	/// <param name="roundTo">Precision to round to, e.g. 10, 100, 1000, et cetera.</param>
	public static double CeilingWithPrecision(double val, int roundTo)
	{
		if (val % roundTo != 0)
		{
			return (val - val % roundTo) + roundTo;
		}
		else
		{
			return val;
		}
	}

	/// <summary>
	/// Round down to the specified decimal place.
	/// </summary>
	/// <param name="val">Value to round.</param>
	/// <param name="roundTo">Precision to round to, e.g. 10, 100, 1000, et cetera.</param>
	public static double FloorWithPrecision(double val, int roundTo)
	{
		if (val % roundTo != 0)
		{
			return val - val % roundTo;
		}
		else
		{
			return val;
		}
	}

	#endregion

	#endregion

} // End class.
using Color = System.Drawing.Color;

namespace DigitalProduction.Graphics;

/// <summary>
/// A class for generating color maps.
///
/// Notes:
/// https://betterfigures.org/2015/06/23/picking-a-colour-scale-for-scientific-graphics/
/// </summary>
public static class ColorMapping
{
	#region Fields

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	static ColorMapping()
	{
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Converts a list of doubles to a list of colors.  Maps values from 0 to 1 to a cool to warm space.
	/// </summary>
	/// <param name="values">A list of unit doubles (doubles that range from 0 =&lt; x &gt;= 1.0).</param>
	public static Color[] MorelandCoolToWarm(double[] values)
	{
		int size		= values.Length;
		Color[] colors	= new Color[size];

		for (int i = 0; i < size; i++)
		{
			colors[i] = MorelandCoolToWarm(values[i]);
		}

		return colors;
	}

	/// <summary>
	/// Converts a list of doubles to a list of colors.  Maps values from 0 to 1 to a cool to warm space.
	/// </summary>
	/// <param name="values">A list of unit doubles (doubles that range from 0 =&lt; x &gt;= 1.0).</param>
	public static List<Color> MorelandCoolToWarm(List<double> values)
	{
		int size			= values.Count;
		List<Color> colors	= new List<Color>(size);

		for (int i = 0; i < size; i++)
		{
			colors.Add(MorelandCoolToWarm(values[i]));
		}

		return colors;
	}

	/// <summary>
	/// A mapping of a value from 0 to 1 into a cool to warm color space.
	///
	/// Based on mapping proposed by Kenneth Moreland.
	/// http://www.kennethmoreland.com/color-maps/
	/// </summary>
	/// <param name="x">Scalar to map.</param>
	public static Color MorelandCoolToWarm(double x)
	{
		System.Diagnostics.Debug.Assert(x >= 0 && x <= 1.0, "The provided scalar is out of range, it must be 0 >= x <= 1.0.");

		double r = 3.5778 * Math.Pow(x, 5) - 8.5142 * Math.Pow(x, 4) + 5.359 * Math.Pow(x, 3) - 1.2696 * Math.Pow(x, 2) + 1.2119 * x + 0.3314;
		double g = -25.022 * Math.Pow(x, 6) + 71.403 * Math.Pow(x, 5) - 73.57 * Math.Pow(x, 4) + 31.883 * Math.Pow(x, 3) - 7.2798 * Math.Pow(x, 2) + 2.3481 * x + 0.2733;
		double b = -4.0767 * Math.Pow(x, 5) + 11.81 * Math.Pow(x, 4) - 9.8693 * Math.Pow(x, 3) + 0.1751 * Math.Pow(x, 2) + 1.3535 * x + 0.7625;

		return Color.FromArgb(ScaleFromUnityTo256Color(r), ScaleFromUnityTo256Color(g), ScaleFromUnityTo256Color(b));
	}

	/// <summary>
	/// A mapping of a double ranged from 0 to 1 into an int in the range of 0 to 255.
	/// </summary>
	/// <param name="scalar">Value to convert.</param>
	public static int ScaleFromUnityTo256Color(double scalar)
	{
		int color = (int)(scalar*255);

		// Handle any round off issues or out of range issues.
		// Check maximum value.
		if (color > 255)
		{
			color = 255;
		}

		// Check minimum value.
		if (color < 0)
		{
			color = 0;
		}

		return color;
	}

	#endregion

} // End class.
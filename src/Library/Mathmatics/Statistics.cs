using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DigitalProduction.Mathmatics
{
	/// <summary>
	/// 
	/// </summary>
	public static class Statistics
	{
		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		static Statistics()
		{
		}

		#endregion

		#region Methods

		/// <summary>
		/// List extension to calculate the average of an array of values.
		/// </summary>
		/// <param name="values">Values to the average of.</param>
		public static double Average(double[] values)
		{
			double average = 0;

			int numberOfEntries = values.Length;
			for (int i = 0; i < numberOfEntries; i++)
			{
				average += values[i];
			}

			average /= numberOfEntries;

			return average;
		}

		/// <summary>
		/// List extension to calculate the average of two arrays of values.
		/// </summary>
		/// <param name="xValues">X values to the average of.</param>
		/// <param name="yValues">Y values to the average of.</param>
		public static double[] Average(double[] xValues, double[] yValues)
		{
			System.Diagnostics.Debug.Assert(xValues.Length == yValues.Length, "Array lengths do not match in covariance calculation.");

			double[] average = new double[2] { 0, 0 };

			int numberOfEntries = xValues.Length;
			for (int i = 0; i < numberOfEntries; i++)
			{
				average[0] += xValues[i];
				average[1] += yValues[i];
			}

			average[0] /= numberOfEntries;
			average[1] /= numberOfEntries;

			return average;
		}

		/// <summary>
		/// List extension to calculate the covariance of a List of Vector2D.
		/// </summary>
		/// <param name="xValues">X values to find the covariance of.</param>
		/// <param name="yValues">Y values to find the covariance of.</param>
		public static double Covariance(double[] xValues, double[] yValues)
		{
			System.Diagnostics.Debug.Assert(xValues.Length == yValues.Length, "Array lengths do not match in covariance calculation.");

			double covariance = 0;

			double[] average = Average(xValues, yValues);

			int numberOfEntries = xValues.Length;
			for (int i = 0; i < numberOfEntries; i++)
			{
				covariance += (xValues[i] - average[0]) * (yValues[i] - average[1]);
			}

			covariance /= numberOfEntries;

			return covariance;
		}

		/// <summary>
		/// Calculations the Person Correlation Coefficient which is a measure of how close a set of data points
		/// is to linear.  The coefficient (p) is in the range of
		/// 
		/// -1 &lt;= p &lt;= 1
		/// 
		/// Where -1 is perfect negative correlation (negative slope).
		/// 
		/// The coefficient is given by: 
		/// \rho _{X,Y}={\frac {\operatorname {cov} (X,Y)}{\sigma _{X}\sigma _{Y}}}} {\displaystyle \rho _{X,Y}={\frac {\operatorname {cov} (X,Y)}{\sigma _{X}\sigma _{Y}}}}
		/// 
		/// p = cov(x,y) / stddev(x) / stddev(y)
		/// </summary>
		/// <param name="xValues">X values.</param>
		/// <param name="yValues">Y values.</param>
		/// <remarks>
		/// This could be calculated using the covariance and standard deviation functions in the class, however, that requires extra loops through the data,
		/// so we will do things a little more manually here.  It doesn't add much code and only requires 1 loop instead of 3.
		/// </remarks>
		public static double PearsonCorrelationCoefficient(double[] xValues, double[] yValues)
		{
			System.Diagnostics.Debug.Assert(xValues.Length == yValues.Length, "Array lengths do not match in covariance calculation.");

			double[] average = Average(xValues, yValues);

			double covariance			= 0;
			double xStandardDeviation	= 0;
			double yStandardDeviation	= 0;

			int numberOfEntries = xValues.Length;
			for (int i = 0; i < numberOfEntries; i++)
			{
				covariance			+= (xValues[i] - average[0]) * (yValues[i] - average[1]);
				double xDifference	=  xValues[i] - average[0];
				double yDifference	=  yValues[i] - average[1];
				xStandardDeviation	+= xDifference * xDifference;
				yStandardDeviation	+= yDifference * yDifference;
			}

			covariance			/= numberOfEntries;
			xStandardDeviation	= Math.Sqrt(xStandardDeviation / numberOfEntries);
			yStandardDeviation	= Math.Sqrt(yStandardDeviation / numberOfEntries);

			if (Precision.Equal(yStandardDeviation, 0))
			{
				// Horizontal line.
				return 1;
			}

			if (Precision.Equal(xStandardDeviation, 0))
			{
				// Vertical line.
				return 1;
			}

			return covariance / xStandardDeviation / yStandardDeviation;
		}

		/// <summary>
		/// List extension to calculate the covariance of a List of Vector2D.
		/// </summary>
		/// <param name="values">Values to the covariance of.</param>
		public static double StandardDeviation(double[] values)
		{
			double standardDeviation = 0;

			double average = Average(values);

			int numberOfEntries = values.Length;
			for (int i = 0; i < numberOfEntries; i++)
			{
				double xDifference	=  values[i] - average;
				standardDeviation	+= xDifference * xDifference;
			}

			standardDeviation = Math.Sqrt(standardDeviation / numberOfEntries);

			return standardDeviation;
		}

		#endregion

	} // End class.
} // End namespace.
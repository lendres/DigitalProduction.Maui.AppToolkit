using DigitalProduction.Mathmatics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// Test cases the the Mathmatics namespace.
	/// </summary>
	[TestClass]
	public class MathTests
	{
		#region Members

		private static double _epsilon		= Precision.Epsilon;

		#endregion

		#region Tests

		/// <summary>
		/// Covariance test.
		/// </summary>
		[TestMethod]
		public void Covariance()
		{
			string errorMessage			= "Covariance is not correct.";
			const int numberOfEntries	= 6;
			double[] xValues			= new double[numberOfEntries] { 5, 6, 7, 8, 9, 10 };
			double[] yValues			= new double[numberOfEntries] { 1, 2, 3, 4, 5, 6 };

			Assert.AreEqual(Statistics.Covariance(xValues, yValues), 2.9167, 0.0001, errorMessage);
		}

		/// <summary>
		/// Pearson Correlation Coefficient measure of the linear correlation between points.
		/// </summary>
		[TestMethod]
		public void PearsonCorrelation()
		{
			string errorMessage			= "Person Correlation Coefficient is not correct.";
			const int numberOfEntries	= 4;
			double[] xValues			= new double[numberOfEntries] { 1, 2, 3, 4 };
			double[] yValues			= new double[numberOfEntries] { 1, 2, 3, 4 };

			// Perfect correlation.
			Assert.AreEqual(Statistics.PearsonCorrelationCoefficient(xValues, yValues), 1, _epsilon, errorMessage);

			// Perfect negative correlation.
			xValues						= new double[numberOfEntries] { -1, -2, -3, -4 };
			Assert.AreEqual(Statistics.PearsonCorrelationCoefficient(xValues, yValues), -1, _epsilon, errorMessage);

			// Horizontal line.
			xValues						= new double[numberOfEntries] {  1,  2,  3,  4 };
			yValues						= new double[numberOfEntries] { 10, 10, 10, 10 };
			Assert.AreEqual(Statistics.PearsonCorrelationCoefficient(xValues, yValues), 1, _epsilon, errorMessage);

			// Vertical line.
			xValues						= new double[numberOfEntries] { 10, 10, 10, 10 };
			yValues						= new double[numberOfEntries] {  1,  2,  3,  4 };
			Assert.AreEqual(Statistics.PearsonCorrelationCoefficient(xValues, yValues), 1, _epsilon, errorMessage);

			// Test on a circle.
			xValues						= new double[numberOfEntries] { 1, 0, -1, 0 };
			yValues						= new double[numberOfEntries] { 0, 1, 0, -1 };
			Assert.AreEqual(Statistics.PearsonCorrelationCoefficient(xValues, yValues), 0, _epsilon, errorMessage);
		}

		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void StandardDeviation()
		{
			string errorMessage			= "Standard deviation is not correct.";
			const int numberOfEntries	= 8;
			double[] xValues			= new double[numberOfEntries] { 10, 12, 23, 23, 16, 23, 21, 16 };

			Assert.AreEqual(Statistics.StandardDeviation(xValues), 4.8989794855664, _epsilon, errorMessage);
		}

		#endregion

	} // End class.
} // End namespace.
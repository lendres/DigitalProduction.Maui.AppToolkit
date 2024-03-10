using DigitalProduction.Mathmatics;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// Test cases the the Mathmatics namespace.
	/// </summary>
	public class MathTests
	{
		#region Members

		private static readonly double _epsilon		= Precision.Epsilon;

		#endregion

		#region Tests

		/// <summary>
		/// Covariance test.
		/// </summary>
		[Fact]
		public void Covariance()
		{
			//string errorMessage			= "Covariance is not correct.";
			double[] xValues			= [5, 6, 7, 8, 9, 10];
			double[] yValues			= [1, 2, 3, 4, 5, 6];

			Assert.Equal(2.9167, Statistics.Covariance(xValues, yValues), 4);
		}

		/// <summary>
		/// Pearson Correlation Coefficient measure of the linear correlation between points.
		/// </summary>
		[Fact]
		public void PearsonCorrelation()
		{
			//string errorMessage			= "Person Correlation Coefficient is not correct.";
			double[] xValues			= [1, 2, 3, 4];
			double[] yValues			= [1, 2, 3, 4];

			// Perfect correlation.
			Assert.Equal(1.0, Statistics.PearsonCorrelationCoefficient(xValues, yValues), _epsilon);

			// Perfect negative correlation.
			xValues                     = [-1, -2, -3, -4];
			Assert.Equal(-1.0, Statistics.PearsonCorrelationCoefficient(xValues, yValues), _epsilon);

			// Horizontal line.
			xValues						= [ 1,  2,  3,  4];
			yValues                     = [10, 10, 10, 10];
			Assert.Equal(1.0, Statistics.PearsonCorrelationCoefficient(xValues, yValues), _epsilon);

			// Vertical line.
			xValues						= [10, 10, 10, 10];
			yValues						= [ 1,  2,  3,  4];
			Assert.Equal(1.0, Statistics.PearsonCorrelationCoefficient(xValues, yValues), _epsilon);

			// Test on a circle.
			xValues						= [1, 0, -1, 0];
			yValues						= [0, 1, 0, -1];
			Assert.Equal(0.0, Statistics.PearsonCorrelationCoefficient(xValues, yValues), _epsilon);
		}

		/// <summary>
		/// 
		/// </summary>
		[Fact]
		public void StandardDeviation()
		{
			//string errorMessage			= "Standard deviation is not correct.";
			double[] xValues			= [10, 12, 23, 23, 16, 23, 21, 16];

			Assert.Equal(4.8989794855664, Statistics.StandardDeviation(xValues), _epsilon);
		}

		#endregion

	} // End class.
}
//using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalProduction.Mathmatics
{
	/// <summary>
	/// 
	/// </summary>
	public static class SignalProcessing
	{
		#region Fields

		#endregion

		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		static SignalProcessing()
		{
		}

		#endregion

		#region Properties

		#endregion

		#region Methods

		///// <summary>
		///// Numerical derivative.  Differentiates the input function with respect to time.
		///// </summary>
		///// <param name="time">List of DateTimes.</param>
		///// <param name="intervalType"></param>
		///// <param name="function">Function (values) to take the derivative of.</param>
		//public static List<double> Derivative(List<DateTime> time, DateTimeIntervalType intervalType, List<double> function)
		//{
		//	int count = time.Count;

		//	if (function.Count != count)
		//	{
		//		throw new Exception("Error taking the derivative.  Time and function vectors/lists are not the same length.");
		//	}
		//	if (count < 2)
		//	{
		//		throw new Exception("Error taking the derivative.  You must have at least two entries to take the derivative.");
		//	}

		//	List<double> derivative = new List<double>(count);

		//	for (int i = 1; i < count; i++)
		//	{
		//		TimeSpan timeSpan		= time[i] - time[i-1];
		//		double timeInterval		= ConvertTimeSpanToInterval(timeSpan, intervalType);
		//		derivative.Add((function[i]-function[i-1]) / timeInterval);
		//	}

		//	// Copy the last entry and add it so that the output List is the same size as the input List.
		//	derivative.Add(derivative[count-2]);

		//	return derivative;
		//}

		///// <summary>
		///// Convert a TimeSpan to a double representing the requested interval type.
		///// </summary>
		///// <param name="timeSpan">TimeSpan to convert.</param>
		///// <param name="intervalType">Desired output units (what time length is TimeSpan expressed in?).  For example 14 days or 2 week.</param>
		//public static double ConvertTimeSpanToInterval(TimeSpan timeSpan, DateTimeIntervalType intervalType)
		//{
		//	switch (intervalType)
		//	{
		//		case DateTimeIntervalType.Days:
		//		{
		//			return timeSpan.TotalDays;
		//		}

		//		case DateTimeIntervalType.Hours:
		//		{
		//			return timeSpan.TotalHours;
		//		}

		//		case DateTimeIntervalType.Milliseconds:
		//		{
		//			return timeSpan.TotalMilliseconds;
		//		}

		//		case DateTimeIntervalType.Minutes:
		//		{
		//			return timeSpan.TotalMinutes;
		//		}

		//		case DateTimeIntervalType.Months:
		//		{
		//			return timeSpan.TotalDays / 30;
		//		}

		//		case DateTimeIntervalType.Seconds:
		//		{
		//			return timeSpan.TotalSeconds;
		//		}

		//		case DateTimeIntervalType.Weeks:
		//		{
		//			return timeSpan.TotalDays / 7;
		//		}

		//		case DateTimeIntervalType.Years:
		//		{
		//			return timeSpan.TotalDays / 365;
		//		}

		//		default:
		//		{
		//			throw new Exception("The TimeSpan cannot be converted to that type of DateTimeIntervalType.");
		//		}
		//	}
		//}

		/// <summary>
		/// Unwrap the phase angle of an input array.
		/// </summary>
		/// <param name="angles">Input angles (in radians).</param>
		public static List<double> Unwrap(List<double> angles)
		{
			int		revolutionCount	= 0;
			double	tolerance		= System.Math.PI;
			int		count			= angles.Count;

			List<double> output = new List<double>(count);

			for (int i = 0; i < count-1; i++)
			{
				output.Add(angles[i] + 2*System.Math.PI*revolutionCount);

				if (System.Math.Abs(angles[i+1] - angles[i]) > System.Math.Abs(tolerance))
				{
					if (angles[i+1] < angles[i])
					{
						revolutionCount = revolutionCount + 1;
					}
					else
					{
						revolutionCount = revolutionCount - 1;
					}
				}
			}

			// Add last entry.
			output.Add(angles[count-1] + 2*System.Math.PI*revolutionCount);

			return output;
		}

		#endregion

	} // End class.
} // End namespace.
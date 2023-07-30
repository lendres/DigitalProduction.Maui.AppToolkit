using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalProduction.Numerics
{
	/// <summary>
	/// Settings used for methods related to finite precision calculations.
	/// </summary>
	public class PrecisionSettings
	{
		#region Enumerations

		#endregion

		#region Delegates

		#endregion

		#region Events

		#endregion

		#region Fields

		private double		_doubleZeroThreshold		= double.Epsilon * 1000;
		private float		_floatZeroThreshold			= float.Epsilon * 100;

		#endregion

		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		public PrecisionSettings()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Threshold for determining if a double value is zero.
		/// </summary>
		public double DoubleZeroThreshold
		{
			get
			{
				return _doubleZeroThreshold;
			}

			set
			{
				_doubleZeroThreshold = value;
			}
		}

		/// <summary>
		/// Threshold for determining if a float value is zero.
		/// </summary>
		public float FloatZeroThreshold
		{
			get
			{
				return _floatZeroThreshold;
			}

			set
			{
				_floatZeroThreshold = value;
			}
		}


		#endregion

		#region Methods

		#endregion

		#region XML

		#endregion

	} // End class.
} // End namespace.
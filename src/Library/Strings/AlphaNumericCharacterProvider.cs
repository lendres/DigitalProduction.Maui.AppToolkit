using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitalProduction.Strings
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAlphaNumericStringProvider
	{
		#region Methods

		/// <summary>
		/// Interface for getting enumerable.
		/// </summary>
		public IEnumerable<string> Get();

		#endregion

	} // End class.

	/// <summary>
	/// 
	/// </summary>
	public class EnglishUpperCaseAlphabet : IAlphaNumericStringProvider
	{
		#region Methods

		/// <summary>
		/// Upper case A through Z in English.
		/// </summary>
		public IEnumerable<string> Get()
		{
			for (char c = 'A'; c <= 'Z'; c++)
			{
				yield return c.ToString();
			}
		}

		#endregion

	} // End class.

	/// <summary>
	/// 
	/// </summary>
	public class EnglishLowerCaseAlphabet : IAlphaNumericStringProvider
	{
		#region Methods

		/// <summary>
		/// Lower case a through z in English.
		/// </summary>
		public IEnumerable<string> Get()
		{
			for (char c = 'a'; c <= 'z'; c++)
			{
				yield return c.ToString();
			}
		}

		#endregion

	} // End class.


	/// <summary>
	/// 
	/// </summary>
	public class Digits : IAlphaNumericStringProvider
	{
		#region Methods

		/// <summary>
		/// The digits 0 through 9.
		/// </summary>
		public IEnumerable<string> Get()
		{
			for (char c = '0'; c <= '9'; c++)
			{
				yield return c.ToString();
			}
		}

		#endregion

	} // End class.

	/// <summary>
	/// 
	/// </summary>
	public class Counter : IAlphaNumericStringProvider
	{
		#region Methods

		/// <summary>
		/// The digits 0 through 9.
		/// </summary>
		public IEnumerable<string> Get()
		{
			int i = 0;
			while (true)
			{
				yield return i++.ToString();
			}
		}

		#endregion

	} // End class.

} // End namespace.
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// A company asset.
	/// </summary>
	public class Asset
	{
		#region Members

		private string				_name				= "";
		private int					_value				= 0;
		private string				_description		= "";

		#endregion

		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Asset()
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public Asset(string name, int value, string description)
		{
			_name			= name;
			_value			= value;
			_description	= description;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Name.
		/// </summary>
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return _name;
			}

			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Value.
		/// </summary>
		[XmlAttribute("value")]
		public int Value
		{
			get
			{
				return _value;
			}

			set
			{
				_value = value;
			}
		}

		/// <summary>
		/// Description.
		/// </summary>
		[XmlElement("description")]
		public string Description
		{
			get
			{
				return _description;
			}

			set
			{
				_description = value;
			}
		}

		#endregion

	} // End class.
} // End namespace.
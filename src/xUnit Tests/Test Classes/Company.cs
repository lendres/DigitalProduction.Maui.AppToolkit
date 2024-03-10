using DigitalProduction.XML.Serialization;
using System.Xml.Serialization;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// A generic company.
	/// </summary>
	[XmlRoot("company")]
	public class Company
	{
		#region Members

		private string				_name			= "";
		private List<Person>		_employees		= new();
		private List<Asset>			_assets			= new();

		#endregion

		#region Construction

		public Company()
		{
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
		/// Number of people in the family.
		/// </summary>
		[XmlIgnore()]
		public int NumberOfEmployees
		{
			get
			{
				return _employees.Count;
			}
		}

		/// <summary>
		/// Employees.
		/// </summary>
		[XmlArray("employees"), XmlArrayItem("employee")]
		public List<Person> Employees
		{
			get
			{
				return _employees;
			}

			set
			{
				_employees = value;
			}
		}

		/// <summary>
		/// Assets.
		/// </summary>
		[XmlArray("assets"), XmlArrayItem("asset")]
		public List<Asset> Assets
		{
			get
			{
				return _assets;
			}

			set
			{
				_assets = value;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Find a person in the family by name.
		/// </summary>
		/// <param name="name">Name of the Person to find.</param>
		/// <returns>The first Person in the list with the specified name.</returns>
		public Person? GetEmployee(string name)
		{
			return _employees.Find(x => x.Name == name);
		}
		
		#endregion

		#region XML
		
		/// <summary>
		/// Create an instance from a file.
		/// </summary>
		/// <param name="path">The file to read from.</param>
		/// <returns>The deserialized file types.</returns>
		public static T Deserialize<T>(string path) where T : Company
		{
			// Deserialize the object creating a new instance.  Then we set the path to the location the file was deserialized
			// from.  That way the file can be saved back to that location if required.
			T company		= Serialization.DeserializeObject<T>(path);
			return company;
		}

		/// <summary>
		/// Write this object to a file.  The Path must be set and represent a valid path or this method will throw an exception.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when the projects path is not set or not valid.</exception>
		public void Serialize(string path)
		{
			Serialization.SerializeObject(this, path);
		}

		#endregion

	} // End class.
} // End namespace.
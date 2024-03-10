using System.Xml.Serialization;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// A person.
	/// </summary>
	public class Person
	{
		#region Members

		private string				_name				= "";
		private int					_age				= 0;
		private	Gender				_gender				= Gender.Female;

		#endregion

		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Person() {}

		/// <summary>
		/// Constructor to populate fields.
		/// </summary>
		public Person(string name, int age, Gender gender)
		{
			_name		= name;
			_age		= age;
			_gender		= gender;
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
		/// Age.
		/// </summary>
		[XmlAttribute("age")]
		public int Age
		{
			get
			{
				return _age;
			}

			set
			{
				_age = value;
			}
		}

		/// <summary>
		/// Gender.
		/// </summary>
		[XmlAttribute("gender")]
		public Gender Gender
		{
			get
			{
				return _gender;
			}

			set
			{
				_gender = value;
			}
		}

		#endregion

	} // End class.
} // End namespace.
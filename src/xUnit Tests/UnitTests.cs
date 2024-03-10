using DigitalProduction.Reflection;
using DigitalProduction.XML.Serialization;

namespace DigitalProduction.UnitTests
{
	public class UnitTests
	{
		#region XML Serialization

		/// <summary>
		/// Basic serialization and deserialization test.
		/// </summary>
		[Fact]
		public void XmlSerialization1()
		{
			const string path = "test1.xml";

			Family family = CreateFamily();

			Serialization.SerializeObject(family, path);
			Family familyDeserialized = Serialization.DeserializeObject<Family>(path);
			Assert.NotNull(familyDeserialized);

			Person? person = familyDeserialized.GetPerson("Mom");
			Assert.NotNull(person);
			Assert.Equal(36, person.Age);

			person = familyDeserialized.GetPerson("Son");
			Assert.NotNull(person);
			Assert.Equal(4, person.Age);

			System.IO.File.Delete(path);
		}

		/// <summary>
		/// 
		/// </summary>
		[Fact]
		public void XmlSerialization2()
		{
			const string path = "test2.xml";

			AirlineCompany company = CreateAirline();

			company.Serialize(path);
			AirlineCompany? deserialized = Company.Deserialize<AirlineCompany>(path);
			Assert.NotNull(deserialized);

			Person? person = deserialized.GetEmployee("Manager");
			Assert.NotNull(person);

			Assert.Equal(36, person.Age);
			Assert.Equal(10, deserialized.NumberOfPlanes);

			System.IO.File.Delete(path);
		}

		/// <summary>
		/// Test the XML writer that writes full closing elements and never uses the short element close.
		/// </summary>
		[Fact]
		public void XmlTextWriterFullTest()
		{
			const string path = "test1.xml";

			AirlineCompany company = CreateAirline();
			//company.Employees.Add(new Person("", 20, Gender.Male));
			//company.Employees.Add(new Person(" ", 20, Gender.Male));
			//company.Employees.Add(new Person(null, 20, Gender.Male));
			company.Assets.Add(new Asset("Asset 1", 1, "Some asset."));
			company.Assets.Add(new Asset("", 2, ""));
			company.Assets.Add(new Asset(" ", 3, " "));
			company.Assets.Add(new Asset("", 4, ""));

			Serialization.SerializeObjectFullEndElement(company, path);
		}

		/// <summary>
		/// Serialization settings.
		/// </summary>
		[Fact]
		public void SerializationSettingsTest()
		{
			const string path1 = "test1.xml";
			const string path2 = "test2.xml";

			AirlineCompany company = CreateAirline();

			SerializationSettings settings				= new(company, path1);
			//settings.XmlSettings.Indent					= false;
			settings.XmlSettings.NewLineOnAttributes	= false;
			Serialization.SerializeObject(settings);

			settings.XmlSettings.NewLineOnAttributes	= true;
			settings.OutputFile							= path2;
			Serialization.SerializeObject(settings);
		}

		#endregion

		#region Attribute Tests

		/// <summary>
		/// Test retrieval of attributes.
		/// </summary>
		[Fact]
		public void Attributes()
		{
			AirlineCompany company = CreateAirline();
			Assert.Equal("Airline", DigitalProduction.Reflection.Attributes.GetDisplayName(company));
			Assert.Equal("Airline", DigitalProduction.Reflection.Attributes.GetDisplayName(typeof(AirlineCompany)));

			Family family = CreateFamily();
			List<string> aliases = DigitalProduction.Reflection.Attributes.GetAliases(family);
			Assert.Equal("Family Members", aliases[0]);
			Assert.Equal("Relatives", aliases[1]);
		}

		/// <summary>
		/// Test retrieval of alternate name attributes.
		/// </summary>
		[Fact]
		public void AlternateNamesAttribute()
		{
			Assert.Equal("737", DigitalProduction.Reflection.Attributes.GetAlternateName(AirPlaneType.Boeing737, AlternateNameType.ShortName));
			Assert.Equal("Boeing 747", DigitalProduction.Reflection.Attributes.GetAlternateName(AirPlaneType.Boeing747, AlternateNameType.LongName));
		}

		#endregion

		#region Helper Function

		/// <summary>
		/// Helper function to create an airline.
		/// </summary>
		/// <returns>A new airline populated with some default values.</returns>
		private static AirlineCompany CreateAirline()
		{
			AirlineCompany company = new()
			{
				Name            = "Oceanic",
				NumberOfPlanes  = 10
			};
			company.Employees.Add(new Person("Manager", 36, Gender.Female));
			company.Employees.Add(new Person("Luggage Handler", 37, Gender.Male));
			company.Employees.Add(new Person("Pilot", 28, Gender.Female));
			company.Employees.Add(new Person("Captain", 30, Gender.Male));
			return company;
		}

		/// <summary>
		/// Helper function to create a family.
		/// </summary>
		/// <returns>A new Family populated with some default values.</returns>
		private static Family CreateFamily()
		{
			Family family = new();
			family.Members.Add(new Person("Mom", 36, Gender.Female));
			family.Members.Add(new Person("Dad", 37, Gender.Male));
			family.Members.Add(new Person("Daughter", 6, Gender.Female));
			family.Members.Add(new Person("Son", 4, Gender.Male));
			return family;
		}

		#endregion

	} // End class.
} // End namespace.
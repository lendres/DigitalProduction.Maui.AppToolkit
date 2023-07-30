using DigitalProduction.Reflection;
using DigitalProduction.XML.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DigitalProduction.UnitTests
{
	[TestClass]
	public class UnitTests
	{
		#region XML Serialization

		/// <summary>
		/// Basic serialization and deserialization test.
		/// </summary>
		[TestMethod]
		public void XmlSerialization1()
		{
			const string path = "test1.xml";

			Family family = CreateFamily();

			Serialization.SerializeObject(family, path);
			Family familyDeserialized = Serialization.DeserializeObject<Family>(path);
			Assert.IsNotNull(familyDeserialized);

			Person? person = familyDeserialized.GetPerson("Mom");
			Assert.IsNotNull(person);
			Assert.AreEqual(person.Age, 36);

			person = familyDeserialized.GetPerson("Son");
			Assert.IsNotNull(person);
			Assert.AreEqual(person.Age, 4);

			System.IO.File.Delete(path);
		}

		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void XmlSerialization2()
		{
			const string path = "test2.xml";

			AirlineCompany company = CreateAirline();

			company.Serialize(path);
			AirlineCompany? deserialized = Company.Deserialize<AirlineCompany>(path);
			Assert.IsNotNull(deserialized);

			Person? person = deserialized.GetEmployee("Manager");
			Assert.IsNotNull(person);

			Assert.AreEqual(person.Age, 36);
			Assert.AreEqual(deserialized.NumberOfPlanes, 10);

			System.IO.File.Delete(path);
		}

		/// <summary>
		/// Test the XML writer that writes full closing elements and never uses the short element close.
		/// </summary>
		[TestMethod]
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
		[TestMethod]
		public void SerializationSettingsTest()
		{
			const string path1 = "test1.xml";
			const string path2 = "test2.xml";

			AirlineCompany company = CreateAirline();

			SerializationSettings settings				= new SerializationSettings(company, path1);
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
		[TestMethod]
		public void Attributes()
		{
			AirlineCompany company = CreateAirline();
			Assert.AreEqual(DigitalProduction.Reflection.Attributes.GetDisplayName(company), "Airline");
			Assert.AreEqual(DigitalProduction.Reflection.Attributes.GetDisplayName(typeof(AirlineCompany)), "Airline");

			Family family = CreateFamily();
			List<string> aliases = DigitalProduction.Reflection.Attributes.GetAliases(family);
			Assert.AreEqual(aliases[0], "Family Members");
			Assert.AreEqual(aliases[1], "Relatives");
		}

		/// <summary>
		/// Test retrieval of alternate name attributes.
		/// </summary>
		[TestMethod]
		public void AlternateNamesAttribute()
		{
			Assert.AreEqual(DigitalProduction.Reflection.Attributes.GetAlternateName(AirPlaneType.Boeing737, AlternateNameType.ShortName), "737");
			Assert.AreEqual(DigitalProduction.Reflection.Attributes.GetAlternateName(AirPlaneType.Boeing747, AlternateNameType.LongName), "Boeing 747");
		}

		#endregion

		#region Helper Function

		/// <summary>
		/// Helper function to create an airline.
		/// </summary>
		/// <returns>A new airline populated with some default values.</returns>
		private static AirlineCompany CreateAirline()
		{
			AirlineCompany company	= new AirlineCompany();
			company.Name			= "Oceanic";
			company.NumberOfPlanes	= 10;
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
			Family family = new Family();
			family.Members.Add(new Person("Mom", 36, Gender.Female));
			family.Members.Add(new Person("Dad", 37, Gender.Male));
			family.Members.Add(new Person("Daughter", 6, Gender.Female));
			family.Members.Add(new Person("Son", 4, Gender.Male));
			return family;
		}

		#endregion

	} // End class.
} // End namespace.
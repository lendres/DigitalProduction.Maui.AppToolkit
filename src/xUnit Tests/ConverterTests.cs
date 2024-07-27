using DigitalProduction.Converters;

namespace DigitalProduction.UnitTests;

public class ConcreteEnumDescription : EnumToDescriptionConverter<TestingType>
{
	public ConcreteEnumDescription() { }
}

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class ConverterTests
{
	#region Members
	#endregion

	#region Tests

	/// <summary>
	/// Test to convert a relative path to an absolute path.
	/// </summary>
	[Fact]
	public void EnumToDescriptionConverterTest()
	{
		string errorMessage									= "Enum description test failed.";
		EnumToDescriptionConverter<TestingType> converter	= new();

		TestingType result = (TestingType)converter.Convert("Type 1", typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(result == TestingType.Type1, errorMessage);

		string stringResult = (string)converter.ConvertBack(TestingType.Type1, typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(stringResult == "Type 1", errorMessage);
	}

	/// <summary>
	/// Test to convert a relative path to an absolute path.
	/// </summary>
	[Fact]
	public void ConcreteEnumToDescriptionConverterTest()
	{
		string errorMessage					= "Enum description test failed.";
		ConcreteEnumDescription converter	= new();

		TestingType result = (TestingType)converter.Convert("Type 1", typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(result == TestingType.Type1, errorMessage);

		string stringResult = (string)converter.ConvertBack(TestingType.Type1, typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(stringResult == "Type 1", errorMessage);
	}

	#endregion

} // End class.
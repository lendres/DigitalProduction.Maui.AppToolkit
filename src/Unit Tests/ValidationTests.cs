using DigitalProduction.Maui.Converters;
using System.Diagnostics;

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

		string stringResult = (string)converter.Convert(TestingType.Type1, typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(stringResult == "Type 1", errorMessage);

		TestingType result = (TestingType)converter.ConvertBack("Type 1", typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(result == TestingType.Type1, errorMessage);
	}

	/// <summary>
	/// Test to convert a relative path to an absolute path.
	/// </summary>
	[Fact]
	public void ConcreteEnumToDescriptionConverterTest()
	{
		string errorMessage					= "Enum description test failed.";
		ConcreteEnumDescription converter	= new();

		string stringResult = (string)converter.Convert(TestingType.Type1, typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(stringResult == "Type 1", errorMessage);

		TestingType result = (TestingType)converter.ConvertBack("Type 1", typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);
		Assert.True(result == TestingType.Type1, errorMessage);
	}

		/// <summary>
	/// Test to convert a relative path to an absolute path.
	/// </summary>
	[Fact]
	public void AreDebuggingConverterTest()
	{
		string errorMessage				= "AreDebugging description test failed.";
		AreDebuggingConverter converter	= new();

		bool solution	= false;
		string mode		= "Release mode.";
		#if DEBUG
			solution	= true;
			mode		= "Debug mode.";
			Debug.WriteLine("Debug mode.");
		#else
			Debug.WriteLine("Release mode.");
		#endif

		bool result = (bool)converter.Convert(TestingType.Type1, typeof(TestingType), null, System.Globalization.CultureInfo.CurrentCulture);

		// Create a string array with the lines of text
		string[] lines = {
			mode,
			"Result: "+result.ToString(),
			"Expected result: "+solution,
			"Passed: "+(result==solution).ToString(),
			converter.AssemblyName,
			"Attributes size: "+converter.DebuggableAttributes.Count,
			"Just in time tracking enabled: "+converter.IsJustInTimeTrackingEnabled
		};
		WriteDebugInfo(lines);

		Assert.True(result == solution, errorMessage);
	}

	private void WriteDebugInfo(string[] lines)
	{
		string filePath = Path.Combine(DigitalProduction.Reflection.Assembly.LibraryPath!, "DebugMessage.txt");

		using (StreamWriter outputFile = new StreamWriter(filePath))
		{
			foreach (string line in lines)
			{
				outputFile.WriteLine(line);
			}
        }
	}

	#endregion

} // End class.
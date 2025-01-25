using DigitalProduction.Maui.Validation;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class ValidationTests
{
	#region Members
	#endregion

	#region Tests

	/// <summary>
	/// Test for files existing/not existing.
	/// </summary>
	[Fact]
	public void FileExists()
	{
		string                  file                    = "Test File.txt";
		string                  noFile                  = "Does Not Exist File.fake";

		FileExistsRule			fileExistsRule			= new();
		FileDoesNotExistsRule	fileDoesNotExistRule	= new();

		Assert.True(fileExistsRule.Check(file));
		Assert.True(fileDoesNotExistRule.Check(noFile));

		Assert.False(fileExistsRule.Check(noFile));
		Assert.False(fileDoesNotExistRule.Check(file));
	}

	/// <summary>
	/// Test for is numeric rule.
	/// </summary>
	[Fact]
	public void IsNumericRuleTest()
	{
		string errorMessage	= "IsNumericRule test failed.";

		// Basic checks that non-numeric is false and numeric is true.
		IsNumericRule rule	= new();

		Assert.False(rule.Check(null), errorMessage);
		Assert.False(rule.Check(""), errorMessage);
		Assert.False(rule.Check("A"), errorMessage);

		Assert.True(rule.Check("01"), errorMessage);
		Assert.True(rule.Check("1.2"), errorMessage);

		// Check minimum and maximums.
		rule.MinimumValue = 1.5;
		rule.MaximumValue = 10;
		Assert.False(rule.Check("0.2"), errorMessage);
		Assert.True(rule.Check("1.5"), errorMessage);
		Assert.True(rule.Check("6"), errorMessage);
		Assert.True(rule.Check("10"), errorMessage);
		Assert.False(rule.Check("12"), errorMessage);
		
		// Check decimal places.		
		rule.MinimumDecimalPlaces = 1;
		rule.MaximumDecimalPlaces = 2;
		Assert.False(rule.Check("2"), errorMessage);
		Assert.True(rule.Check("2.1"), errorMessage);
		Assert.True(rule.Check("2.12"), errorMessage);
		Assert.False(rule.Check("2.123"), errorMessage);
	}

	#endregion

} // End class.
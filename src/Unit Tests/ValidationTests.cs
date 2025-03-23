using DigitalProduction.Maui.Validation;
using Microsoft.Maui.Media;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class ValidationTests
{
	#region Members
		
	private readonly string		_file		= "Test File.txt";
	private readonly string		_noFile		= "Does Not Exist File.fake";

	#endregion

	#region File and Directory Tests

	/// <summary>
	/// Test for files existing/not existing.
	/// </summary>
	[Fact]
	public void FileExistsSimple()
	{
		FileExistsRule          fileExistsNoSearchRule  = new() { SearchCurrentDirectory = false };
		FileExistsRule          fileExistsRule			= new() { SearchApplicationDirectory = true };
		FileDoesNotExistsRule	fileDoesNotExistRule	= new() { SearchApplicationDirectory = true };

		Assert.True(fileExistsRule.Check(_file));
		Assert.True(fileDoesNotExistRule.Check(_noFile));

		// Check with entire path.
		string directory	= DigitalProduction.Reflection.Assembly.ExecutablePath!;
		string fileWithPath	= Path.Combine(directory, _file);
		Assert.True(fileExistsNoSearchRule.Check(fileWithPath));

		Assert.False(fileExistsNoSearchRule.Check(_file));
		Assert.False(fileExistsRule.Check(_noFile));
		Assert.False(fileDoesNotExistRule.Check(_file));

		Assert.False(fileExistsRule.Check(""));
		Assert.False(fileExistsRule.Check(" "));
		Assert.False(fileExistsRule.Check(null));
	}

	/// <summary>
	/// Test for files existing/not existing.
	/// </summary>
	[Fact]
	public void FileExistsWithSearchDirectory()
	{
		// Setup, create a directory for testing and add a new file.
		string tempDirectory	= DigitalProduction.IO.Path.GetTemporaryDirectory();
		string newFile			= Path.Combine(tempDirectory, "New File.txt");
		File.Copy(_file, newFile);

		FileExistsRule          fileExistsRule          = new() { SearchDirectories = [tempDirectory] };
		FileDoesNotExistsRule	fileDoesNotExistRule	= new() { SearchDirectories = [tempDirectory] };

		Assert.True(fileExistsRule.Check(newFile));
		Assert.True(fileDoesNotExistRule.Check(_noFile));

		Assert.False(fileExistsRule.Check(_noFile));
		Assert.False(fileDoesNotExistRule.Check(newFile));

		// Clean up.
		File.Delete(Path.Combine(tempDirectory, newFile));
		Directory.Delete(tempDirectory);
	}

	#endregion

	#region Numeric Tests

	/// <summary>
	/// Test for is numeric rule.
	/// </summary>
	[Fact]
	public void IsNumericRuleTest()
	{
		string errorMessage	= nameof(IsNumericRule) + " test failed.";

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

	/// <summary>
	/// Test for is numeric rule.
	/// </summary>
	[Fact]
	public void IsNotZeroNumericRuleTest()
	{
		string errorMessage	= nameof(IsNotZeroNumericRule) + " test failed.";

		// Basic checks that non-numeric is false and numeric is true.
		IsNotZeroNumericRule rule = new();

		Assert.False(rule.Check("A"), errorMessage);
		Assert.True(rule.Check("01"), errorMessage);
		Assert.True(rule.Check("1.2"), errorMessage);
		Assert.True(rule.Check("01.20"), errorMessage);
		Assert.True(rule.Check("0.00000000001"), errorMessage);
		Assert.False(rule.Check("0"), errorMessage);
		Assert.False(rule.Check("0.0"), errorMessage);
	}

	#endregion

	#region String Tests

	/// <summary>
	/// Test for is IsNotNullOrEmptyRule rule.
	/// </summary>
	[Fact]
	public void IsNotNullOrEmptyRuleTest()
	{
		string errorMessage	= nameof(IsNotNullOrEmptyRule) + " test failed.";

		// Basic checks that non-numeric is false and numeric is true.
		IsNotNullOrEmptyRule rule = new();

		Assert.False(rule.Check(""), errorMessage);
		Assert.False(rule.Check(null), errorMessage);
		Assert.True(rule.Check("01"), errorMessage);
		Assert.True(rule.Check("A"), errorMessage);
		Assert.True(rule.Check("a a"), errorMessage);
	}

	/// <summary>
	/// Test for is IsNotNullOrEmptyRule rule.
	/// </summary>
	[Fact]
	public void IsNotDuplicateStringRuleTest()
	{
		string errorMessage	= nameof(IsNotDuplicateStringRule) + " test failed.";

		// Basic checks that non-numeric is false and numeric is true.
		IsNotDuplicateStringRule rule = new()
		{
			Values                  = ["Name 1", "Name 2"],
			ExcludeValue            = "Exclude Name"
		};

		// Case insensitive tests.
		Assert.False(rule.Check("Name 2"), errorMessage);
		Assert.False(rule.Check("name 2"), errorMessage);
		Assert.True(rule.Check("exclude name"), errorMessage);
		Assert.True(rule.Check("Does Not Exist"), errorMessage);

		// Case sensitive tests.
		rule.CaseSensitive = true;
		Assert.False(rule.Check("Name 1"), errorMessage);
		Assert.False(rule.Check("Name 2"), errorMessage);
		Assert.True(rule.Check("name 1"), errorMessage);
		Assert.True(rule.Check("Exclude Name"), errorMessage);
		Assert.True(rule.Check("exclude name"), errorMessage);
		Assert.True(rule.Check("Does Not Exist"), errorMessage);
	}

	#endregion

} // End class.
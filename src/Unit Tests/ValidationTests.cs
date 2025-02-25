using DigitalProduction.Maui.Validation;

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

	#region Tests

	/// <summary>
	/// Test for files existing/not existing.
	/// </summary>
	[Fact]
	public void FileExistsSimple()
	{
		FileExistsRule          fileExistsNoSearchRule  = new();
		FileExistsRule          fileExistsRule			= new()
		{
			SearchApplicationDirectory = true
		};
		FileDoesNotExistsRule	fileDoesNotExistRule	= new()
		{
			SearchApplicationDirectory = true
		};

		Assert.True(fileExistsRule.Check(_file));
		Assert.True(fileDoesNotExistRule.Check(_noFile));

		// Check with entire path.
		string directory	= DigitalProduction.Reflection.Assembly.ExecutablePath!;
		string fileWithPath	= Path.Combine(directory, _file);
		Assert.True(fileExistsNoSearchRule.Check(fileWithPath));

		Assert.False(fileExistsNoSearchRule.Check(_file));
		Assert.False(fileExistsRule.Check(_noFile));
		Assert.False(fileDoesNotExistRule.Check(_file));
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

		FileExistsRule          fileExistsRule          = new()
		{
			SearchDirectories = [tempDirectory]
		};
		FileDoesNotExistsRule	fileDoesNotExistRule	= new()
		{
			SearchDirectories = [tempDirectory]
		};

		Assert.True(fileExistsRule.Check(newFile));
		Assert.True(fileDoesNotExistRule.Check(_noFile));

		Assert.False(fileExistsRule.Check(_noFile));
		Assert.False(fileDoesNotExistRule.Check(newFile));

		// Clean up.
		File.Delete(Path.Combine(tempDirectory, newFile));
		Directory.Delete(tempDirectory);
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

	/// <summary>
	/// Test for is numeric rule.
	/// </summary>
	[Fact]
	public void IsNotZeroNumericRuleTest()
	{
		string errorMessage	= "IsNotZeroNumericRule test failed.";

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

} // End class.
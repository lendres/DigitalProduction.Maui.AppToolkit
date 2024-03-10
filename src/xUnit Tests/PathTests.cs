using Path = DigitalProduction.IO.Path;

namespace DigitalProduction.UnitTests
{
	/// <summary>
	/// Test cases the the Mathmatics namespace.
	/// </summary>
	public class PathTests
	{
		#region Members

		#endregion

		#region Tests

		/// <summary>
		/// Test to convert a relative path to an absolute path.
		/// </summary>
		[Fact]
		public void ConvertToAbsolutePathFromCurrentDirectory()
		{
			string errorMessage		= "Relative path is not correct.";
			string currentDirectory = System.IO.Directory.GetCurrentDirectory();

			string relativePath		= "\\Path\\To\\Location";
			string solution			= currentDirectory + relativePath;

			// Test the ".\" relative path notation.
			string result			= Path.ConvertToAbsolutePath("."+relativePath);
			Assert.True(result == solution, errorMessage);

			// Test the "..\" relative path notation.
			solution				= Path.Combine(Path.ChangeDirectoryDotDot(currentDirectory), relativePath);
			result					= Path.ConvertToAbsolutePath(".." + relativePath);
			Assert.True(result == solution, errorMessage);

			// Test the "..\..\" relative path notation.
			solution				= Path.Combine(Path.ChangeDirectoryDotDot(currentDirectory, 2), relativePath);
			result					= Path.ConvertToAbsolutePath("..\\.." + relativePath);
			Assert.True(result == solution, errorMessage);
		}

		/// <summary>
		/// Test to convert a relative path to an absolute path.
		/// </summary>
		[Fact]
		public void ConvertToAbsolutePathFromArbitraryDirectory()
		{
			string errorMessage     = "Relative path is not correct.";
			string currentDirectory = "C:\\Temp\\Sub\\";

			string relativePath     = "\\Path\\To\\Location";
			string solution         = Path.Combine(currentDirectory, relativePath);

			// Test the ".\" relative path notation.
			string result           = Path.ConvertToAbsolutePath("."+relativePath, currentDirectory);
			Assert.True(result == solution, errorMessage);

			// Test the "..\" relative path notation.
			solution                = Path.Combine(Path.ChangeDirectoryDotDot(currentDirectory), relativePath);
			result                  = Path.ConvertToAbsolutePath(".." + relativePath, currentDirectory);
			Assert.True(result == solution, errorMessage);

			// Test the "..\..\" relative path notation.
			solution                = Path.Combine(Path.ChangeDirectoryDotDot(currentDirectory, 2), relativePath);
			result                  = Path.ConvertToAbsolutePath("..\\.." + relativePath, currentDirectory);
			Assert.True(result == solution, errorMessage);
		}

		/// <summary>
		/// Test to convert a relative path to an absolute path.
		/// </summary>
		[Fact]
		public void ConvertToRelativePath()
		{
			string errorMessage		= "Relative path is not correct.";
			string currentDirectory	= "C:\\Temp\\Sub Folder\\";

			string relativePath		= "Path\\To\\New Location";
			string inputPath		= currentDirectory + relativePath;
			string solution         = ".\\" + relativePath;

			// Test converting relative to the "current" directory.
			string result			= Path.ConvertToRelativePath(inputPath, currentDirectory);
			Assert.True(result == solution, errorMessage);

			// Test converting relative to a parallel directory.
			string parallelDirectory	= "From\\Old Location";
			solution					= "..\\..\\" + relativePath;
			result						= Path.ConvertToRelativePath(inputPath, currentDirectory+parallelDirectory);
			Assert.True(result == solution, errorMessage);

			// Test converting converting when paths are not relative.
			solution					= inputPath;
			result						= Path.ConvertToRelativePath(inputPath, "D:\\Completely Different\\Path");
			Assert.True(result == solution, errorMessage);
		}

		#endregion

	} // End class.
} // End namespace.
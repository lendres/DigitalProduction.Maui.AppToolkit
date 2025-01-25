namespace DigitalProduction.Maui.Validation;

public abstract class FileExistsBase : ValidationRuleBase<string>
{
	public List<string> SearchDirectories { get; set; } = [];

	protected bool FileExists(string fileName)
	{
		// Check if the full path was provided or if it exists in the current directory.
		if (File.Exists(fileName))
		{
			return true;
		}

		// Search any specified directories.
		foreach (string directory in SearchDirectories)
		{
			if (File.Exists(Path.Combine(directory, fileName)))
			{
				return true;
			}
		}
		return false;
	}
}
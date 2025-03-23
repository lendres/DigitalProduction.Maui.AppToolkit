namespace DigitalProduction.Maui.Validation;

public abstract class FileExistsBase : ValidationRuleBase<string>
{
	public bool				SearchCurrentDirectory { get; set; }		= false;
	public bool				SearchApplicationDirectory { get; set; }	= false;
	public List<string>		SearchDirectories { get; set; }				= [];

	protected bool FileExists(string? fileName)
	{
		// Check if the full path was provided.
		if (!string.IsNullOrEmpty(System.IO.Path.GetDirectoryName(fileName)))
		{
			if (File.Exists(fileName))
			{
				return true;
			}
		}

		// Check if it is in the current active directory.
		if (SearchCurrentDirectory)
		{
			if (File.Exists(fileName))
			{
				return true;
			}
		}

		// Check if it exists in the application directory.
		if (SearchApplicationDirectory)
		{
			string? path = DigitalProduction.Reflection.Assembly.ExecutablePath;
			if (path != null)
			{
				path = System.IO.Path.Combine(path, fileName);
				if (File.Exists(path))
				{
					return true;
				}
			}
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
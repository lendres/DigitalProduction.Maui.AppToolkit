using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

namespace DigitalProduction.Maui.UI;

/// <summary>Shared preference access for AppToolkit and its host application.</summary>
public static class PreferenceStorage
{
	private static readonly Lazy<IPreferences> _preferences = new(CreatePreferences);

	public static IPreferences Default => _preferences.Value;

	private static IPreferences CreatePreferences()
	{
#if WINDOWS
		if (AppInfo.Current.PackagingModel == AppPackagingModel.Unpackaged)
		{
			return new FilePreferences(Path.Combine(FileSystem.AppDataDirectory, "..", "Settings", "preferences.dat"));
		}
#endif
		return Preferences.Default;
	}
}

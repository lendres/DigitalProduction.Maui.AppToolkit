namespace DigitalProduction.Maui.UI;

public static partial class LifecycleEventsInstaller
{
	public static void ConfigureLifecycleEvents(MauiAppBuilder builder, LifecycleOptions? lifecycleOptions = null)
	{
		PlatformConfigureLifecycleEvents(builder, lifecycleOptions);
	}

	static partial void PlatformConfigureLifecycleEvents(MauiAppBuilder builder, LifecycleOptions? lifecycleOptions);
}
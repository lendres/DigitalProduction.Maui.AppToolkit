using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;

namespace DigitalProduction.UI;

public static partial class LifecycleEventsInstaller
{

	public static void ConfigureLifecycleEvents(MauiAppBuilder builder)
	{
		PlatformConfigureLifecycleEvents(builder);
	}

	static partial void PlatformConfigureLifecycleEvents(MauiAppBuilder builder);
}
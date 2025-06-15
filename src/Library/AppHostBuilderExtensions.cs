using CommunityToolkit.Maui;

namespace DigitalProduction.Maui;

/// <summary>
/// Generic MAUI extensions for custom fonts.
/// </summary>
public static class AppHostBuilderExtensions
{
    /// <summary>
    /// Configures fonts for the MAUI application.
    /// </summary>
    /// <param name="builder">The MAUI application builder.</param>
    /// <returns>Configured MAUI app builder with custom settings.</returns>
    public static MauiAppBuilder UseDigitalProductionMauiAppToolkit(this MauiAppBuilder builder)
    {
        builder.ConfigureFonts(fonts =>
        {
			fonts.AddFont("IBMPlexMono-Bold.ttf", "IBMPlexMono-Bold");
			fonts.AddFont("IBMPlexMono-Regular.ttf", "IBMPlexMono-Regular");
        });

        return builder;
    }
}
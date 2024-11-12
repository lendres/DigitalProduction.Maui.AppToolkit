namespace DigitalProduction;

public static class AppHostBuilderExtensions
{
	public static MauiAppBuilder UseDigitalProduction(this MauiAppBuilder builder)
	{

		builder.Services.AddSingleton<DigitalProduction.Resources.Styles.Colors>();
		builder.Services.AddSingleton<DigitalProduction.Resources.Styles.Styles>();

		return builder;
	}
}
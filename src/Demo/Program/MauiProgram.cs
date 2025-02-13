using CommunityToolkit.Maui;
using CommunityToolkit.Maui.ApplicationModel;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Media;
using CommunityToolkit.Maui.Storage;
using DigitalProduction.Maui.ViewModels;
using DigitalProduction.Demo.Pages;
using DigitalProduction.Demo.ViewModels;
using Microsoft.Extensions.Logging;

namespace DigitalProduction.Demo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		RegisterViewsAndViewModels(builder.Services);
		RegisterEssentials(builder.Services);
		#if DEBUG
			builder.Logging.AddDebug();
		#endif

		return builder.Build();
	}

	static void RegisterViewsAndViewModels(in IServiceCollection services)
	{
		services.AddTransient<ViewsGalleryPage, ViewsGalleryViewModel>();
		services.AddTransientWithShellRoute<AboutPage, AboutPageViewModel>();
		services.AddTransientWithShellRoute<DataGridPage, DataGridPageViewModel>();

		services.AddTransient<DialogsGalleryPage, DialogsGalleryViewModel>();
		services.AddTransientWithShellRoute<PathValidationPage, ValidationPageViewModel>();
	}

	static IServiceCollection AddTransientWithShellRoute<TPage, TViewModel>(this IServiceCollection services) 
		where TPage : BasePage<TViewModel>
		where TViewModel : BaseViewModel
	{
		return services.AddTransientWithShellRoute<TPage, TViewModel>(AppShell.GetPageRoute<TViewModel>());
	}

	static void RegisterEssentials(in IServiceCollection services)
	{
		services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);
		services.AddSingleton<IDeviceInfo>(DeviceInfo.Current);
		services.AddSingleton<IFileSaver>(FileSaver.Default);
		services.AddSingleton<IFolderPicker>(FolderPicker.Default);
		services.AddSingleton<IBadge>(Badge.Default);
		services.AddSingleton<ISpeechToText>(SpeechToText.Default);
		services.AddSingleton<ITextToSpeech>(TextToSpeech.Default);
	}
}
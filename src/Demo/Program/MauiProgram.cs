using CommunityToolkit.Maui;
using CommunityToolkit.Maui.ApplicationModel;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Media;
using CommunityToolkit.Maui.Storage;
using DigitalProduction.Maui.ViewModels;
using DPMauiDemo.Pages;
using DPMauiDemo.ViewModels;
using Microsoft.Extensions.Logging;

namespace DPMauiDemo;

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
		services.AddTransientWithShellRoute<WelcomePage, AboutViewModel>();
		
		services.AddTransient<ControlsGalleryPage, ControlsGalleryViewModel>();
		services.AddTransientWithShellRoute<AboutPage, AboutPageViewModel>();

		services.AddTransient<DialogsGalleryPage, DialogsGalleryViewModel>();
		services.AddTransientWithShellRoute<DataGridPage, DataGridPageViewModel>();
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
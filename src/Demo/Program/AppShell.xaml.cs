using DigitalProduction.Maui.ViewModels;
using DigitalProduction.Demo.Pages;
using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo;

public partial class AppShell : Shell
{
	private static readonly IReadOnlyDictionary<Type, (Type GalleryPageType, Type ContentPageType)> viewModelMappings = new Dictionary<Type, (Type, Type)>(
	[
		CreateViewModelMapping<AboutPage, AboutPageViewModel, ViewsGalleryPage, ViewsGalleryViewModel>(),
		CreateViewModelMapping<DataGridPage, DataGridPageViewModel, ViewsGalleryPage, ViewsGalleryViewModel>(),
		CreateViewModelMapping<PathValidationPage, ValidationPageViewModel, DialogsGalleryPage, WorkFlowsGalleryViewModel>()
	]);

	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(DataGridExamplePage), typeof(DataGridExamplePage));
	}

	public static string GetPageRoute<TViewModel>() where TViewModel : BaseViewModel
	{
		return GetPageRoute(typeof(TViewModel));
	}

	public static string GetPageRoute(Type viewModelType)
	{
		if (!viewModelType.IsAssignableTo(typeof(BaseViewModel)))
		{
			throw new ArgumentException($"{nameof(viewModelType)} must implement {nameof(BaseViewModel)}", nameof(viewModelType));
		}

		if (!viewModelMappings.TryGetValue(viewModelType, out (Type GalleryPageType, Type ContentPageType) mapping))
		{
			throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings. Please register your ViewModel in {nameof(AppShell)}.{nameof(viewModelMappings)}");
		}

		UriBuilder uri = new("", GetPageRoute(mapping.GalleryPageType, mapping.ContentPageType));
		return uri.Uri.OriginalString[..^1];
	}

	static string GetPageRoute(Type galleryPageType, Type contentPageType) => $"//{galleryPageType.Name}/{contentPageType.Name}";

	static KeyValuePair<Type, (Type GalleryPageType, Type ContentPageType)> CreateViewModelMapping<TPage, TViewModel, TGalleryPage, TGalleryViewModel>()
		where TPage : BasePage<TViewModel>
		where TViewModel : BaseViewModel
		where TGalleryPage : BaseGalleryPage<TGalleryViewModel>
		where TGalleryViewModel : BaseGalleryViewModel
	{
		return new KeyValuePair<Type, (Type GalleryPageType, Type ContentPageType)>(typeof(TViewModel), (typeof(TGalleryPage), typeof(TPage)));
	}
}
using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class ViewsGalleryPage(IDeviceInfo deviceInfo, ViewsGalleryViewModel galleryViewModel) :
	BaseGalleryPage<ViewsGalleryViewModel>("Views", deviceInfo, galleryViewModel)
{
}
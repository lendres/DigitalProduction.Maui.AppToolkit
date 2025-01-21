using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public class ControlsGalleryPage(IDeviceInfo deviceInfo, ControlsGalleryViewModel galleryViewModel) :
	BaseGalleryPage<ControlsGalleryViewModel>("Controls", deviceInfo, galleryViewModel)
{
}
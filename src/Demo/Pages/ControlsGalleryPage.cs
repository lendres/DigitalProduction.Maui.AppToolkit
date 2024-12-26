using DPMauiDemo.ViewModels;

namespace DPMauiDemo.Pages;

public class ControlsGalleryPage(IDeviceInfo deviceInfo, ControlsGalleryViewModel galleryViewModel) :
	BaseGalleryPage<ControlsGalleryViewModel>("Controls", deviceInfo, galleryViewModel)
{
}
using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public class DialogsGalleryPage(IDeviceInfo deviceInfo, DialogsGalleryViewModel galleryViewModel) :
	BaseGalleryPage<DialogsGalleryViewModel>("Dialogs", deviceInfo, galleryViewModel)
{
}
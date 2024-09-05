using DPMauiDemo.ViewModels;

namespace DPMauiDemo.Pages;

public class DialogsGalleryPage(IDeviceInfo deviceInfo, DialogsGalleryViewModel galleryViewModel) :
	BaseGalleryPage<DialogsGalleryViewModel>("Dialogs", deviceInfo, galleryViewModel)
{
}
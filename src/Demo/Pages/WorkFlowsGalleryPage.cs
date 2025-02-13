using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class WorkFlowsGalleryPage(IDeviceInfo deviceInfo, WorkFlowsGalleryViewModel galleryViewModel) :
	BaseGalleryPage<WorkFlowsGalleryViewModel>("Dialogs", deviceInfo, galleryViewModel)
{
}
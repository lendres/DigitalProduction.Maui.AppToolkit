using DigitalProduction.Demo.Models;
using DigitalProduction.Demo.Pages;

namespace DigitalProduction.Demo.ViewModels;

public partial class ControlsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<AboutPageViewModel>("About Popups", "Views for displaying information about the application.")
]);
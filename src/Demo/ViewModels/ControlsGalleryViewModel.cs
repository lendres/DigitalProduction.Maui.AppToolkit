using DPMauiDemo.Models;
using DPMauiDemo.Pages;

namespace DPMauiDemo.ViewModels;

public partial class ControlsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<AboutPageViewModel>("About Popups", "Views for displaying information about the application.")
]);
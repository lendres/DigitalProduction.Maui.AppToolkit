using DPMauiDemo.Models;
using DPMauiDemo.Pages;

namespace DPMauiDemo.ViewModels;

public partial class ControlsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<AboutPageViewModel>(nameof(AboutPage), "About views for displaying information about the application.")
]);
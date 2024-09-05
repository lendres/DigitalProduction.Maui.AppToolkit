using DPMauiDemo.Models;
using DPMauiDemo.Pages;

namespace DPMauiDemo.ViewModels;

public class DialogsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<EmptyViewModel>(nameof(AboutPage), "About views for displaying information about the application."),
]);
using DigitalProduction.Demo.Models;

namespace DigitalProduction.Demo.ViewModels;

public partial class ViewsGalleryViewModel() : BaseGalleryViewModel(
[
	// The strings are the title and a description.  These are the values shown in the gallery page.
	SectionModel.Create<AboutPageViewModel>("About Popups", "Views for displaying information about the application."),
	SectionModel.Create<DataGridPageViewModel>("Data Grid", "DataGrid examples."),
	SectionModel.Create<StylesPageViewModel>("Styles", "Style examples.")
]);
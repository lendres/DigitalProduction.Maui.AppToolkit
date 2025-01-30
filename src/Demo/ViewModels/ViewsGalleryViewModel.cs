using DigitalProduction.Demo.Models;
using DigitalProduction.Demo.Pages;

namespace DigitalProduction.Demo.ViewModels;

public partial class ViewsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<AboutPageViewModel>("About Popups", "Views for displaying information about the application."),
	SectionModel.Create<DataGridPageViewModel>("Data Grid", "DataGrid examples.")
]);
using DigitalProduction.Demo.Models;
using DigitalProduction.Demo.Pages;

namespace DigitalProduction.Demo.ViewModels;

public partial class DialogsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<DataGridPageViewModel>(nameof(DataGridPage), "DataGrid examples.")
]);
using DPMauiDemo.Models;
using DPMauiDemo.Pages;

namespace DPMauiDemo.ViewModels;

public partial class DialogsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<DataGridPageViewModel>(nameof(DataGridPage), "DataGrid examples.")
]);
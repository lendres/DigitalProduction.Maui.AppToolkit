using DigitalProduction.Demo.Models;

namespace DigitalProduction.Demo.ViewModels;

public partial class DialogsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<ValidationPageViewModel>("Path Validation", "File and directory validation example.")
]);
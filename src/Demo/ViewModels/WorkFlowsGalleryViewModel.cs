using DigitalProduction.Demo.Models;

namespace DigitalProduction.Demo.ViewModels;

public partial class WorkFlowsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<PathValidationPageViewModel>("Path Validation", "File and directory validation example."),
	SectionModel.Create<SaveBeforeExitPageViewModel>("Save Before Exit", "Save before exit example.")
]);
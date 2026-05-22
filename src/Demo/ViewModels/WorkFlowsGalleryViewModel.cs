using DigitalProduction.Demo.Models;

namespace DigitalProduction.Demo.ViewModels;

public partial class WorkFlowsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<PathValidationPageViewModel>("Path Validation", "File and directory validation examples."),
	SectionModel.Create<SaveBeforeExitPageViewModel>("Save Before Exit", "Save before exit example."),
	SectionModel.Create<ConvertersPageViewModel>("Converters", "Converter examples.")
]);
using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class SaveBeforeExitPage : BasePage<SaveBeforeExitPageViewModel>
{
	public SaveBeforeExitPage(SaveBeforeExitPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}
}
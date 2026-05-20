using DigitalProduction.Demo.ViewModels;
using DigitalProduction.Maui.Services;

namespace DigitalProduction.Demo.Pages;

public partial class SaveBeforeExitPage : BasePage<SaveBeforeExitPageViewModel>
{
	public SaveBeforeExitPage(SaveBeforeExitPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	async public void OnNew(object sender, EventArgs eventArgs)
	{
		SaveChoice closeChoice = await BindingContext.SaveService.PromptSaveChangesAsync();

		switch (closeChoice)
		{
			case SaveChoice.ContinueWithoutSaving:
				BindingContext.SaveText = "Code Behind without Saving";
				break;
			default:
				return;
		}
	}
}
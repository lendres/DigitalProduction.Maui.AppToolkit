using CommunityToolkit.Maui.Storage;
using DigitalProduction.Demo.ViewModels;
using DigitalProduction.Maui.Services;

namespace DigitalProduction.Demo.Pages;

public partial class SaveBeforeExitPage : BasePage<SaveBeforeExitPageViewModel>
{
	#region Fields

	private readonly ISaveService	_saveBeforeExit			= DigitalProduction.Maui.Services.ServiceProvider.GetService<ISaveService>();

	#endregion

	public SaveBeforeExitPage(SaveBeforeExitPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	#region Events

	async void OnSubmit(object sender, EventArgs eventArgs)
	{
		await DisplayAlert("Success", "All entries are valid!", "Ok");
	}

	#endregion
}
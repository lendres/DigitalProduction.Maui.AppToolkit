using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class DataGridExamplePage : BasePage<DataGridViewModel>
{
	public DataGridExamplePage(DataGridViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	async void OnDisplayMessage(object sender, EventArgs eventArgs)
	{
		await DisplayAlert("Message", "This command is non-functioning.", "Ok");
	}

	async void OnDelete(object sender, EventArgs eventArgs)
	{
		bool result = await DisplayAlert("Delete", "Delete the selected item, do you wish to continue?", "Yes", "No");

		if (result)
		{
			BindingContext.Delete();
		}
	}
}
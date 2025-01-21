using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class DataGridPage : BasePage<DataGridPageViewModel>
{
	public DataGridPage(DataGridPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	async void OnButtonDataGrid1Clicked(object? sender, EventArgs args)
	{
		await Shell.Current.GoToAsync(nameof(DataGridExamplePage), true);
	}
}
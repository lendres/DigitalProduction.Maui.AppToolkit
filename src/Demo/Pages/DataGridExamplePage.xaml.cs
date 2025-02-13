namespace DigitalProduction.Demo.Pages;

public partial class DataGridExamplePage : ContentPage
{
	public DataGridExamplePage()
	{
		InitializeComponent();
	}

	async void OnDisplayMessage(object sender, EventArgs eventArgs)
	{
		await DisplayAlert("Message", "This command is non-functioning.", "Ok");
	}
}
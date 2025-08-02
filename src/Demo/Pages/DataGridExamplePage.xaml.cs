using DigitalProduction.Demo.ViewModels;
using DigitalProduction.Maui.Storage;
using Microsoft.Maui;

namespace DigitalProduction.Demo.Pages;

[QueryProperty(nameof(NavigationCommand), "NavigationCommand")]
[QueryProperty(nameof(NavigationObject), "NavigationObject")]
public partial class DataGridExamplePage : BasePage<DataGridViewModel>
{
	#region Fields

	//private readonly DataGridViewModel		_viewModel;

	#endregion

	#region Construction

	public DataGridExamplePage(DataGridViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	#endregion

	#region Properties

	public string NavigationCommand { get; set; } = string.Empty;

	public Person NavigationObject { get; set; } = new();

	#endregion

	async void OnDisplayMessage(object sender, EventArgs eventArgs)
	{
		await DisplayAlert("Message", "This command is non-functioning.", "Ok");
	}

	/// <summary>
	/// Navigation back from the bibliography edit page.  The NavigationCommand and NavigationObject get set and this gets called.
	/// </summary>
	/// <param name="eventArgs"></param>
	protected override void OnNavigatedTo(NavigatedToEventArgs eventArgs)
	{
		base.OnNavigatedTo(eventArgs);

		switch (NavigationCommand)
		{
			case "Save":
				BindingContext.Insert(NavigationObject);
				break;

			case "Replace":
				BindingContext.ReplaceSelected(NavigationObject);
				break;
		}

		PeopleDataGrid.ScrollTo(BindingContext.SelectedItem!, ScrollToPosition.Center, true);
	}

	async void OnEditPerson(object sender, EventArgs eventArgs)
	{
		//await Shell.Current.GoToAsync(nameof(EditRawBibEntryForm), true, new Dictionary<string, object>
		//{
		//	{ "AddMode",  false },
		//	{ "BibEntry", BindingContext.SelectedItem! }
		//});
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
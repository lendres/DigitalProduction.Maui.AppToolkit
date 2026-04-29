using CommunityToolkit.Maui.Views;
using DigitalProduction.Demo.ViewModels;
using DigitalProduction.Maui.Views;
using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Demo.Pages;

[QueryProperty(nameof(NavigationCommand), "NavigationCommand")]
[QueryProperty(nameof(NavigationObject), "NavigationObject")]
public partial class DataGridExamplePage : BasePage<DataGridViewModel>
{
	#region Fields
	#endregion

	#region Construction

	public DataGridExamplePage(DataGridViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
		for (int i = 0; i < 50; i++)
		{ 
			BindingContext.AddPeople();
		}
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

			case "Cancel":
				// Do nothing.
				break;
		}

		PeopleDataGrid.ScrollTo(BindingContext.SelectedItem!, ScrollToPosition.Center, true);
	}

	async void OnEdit(object sender, EventArgs eventArgs)
	{
		await Shell.Current.GoToAsync(nameof(PersonEditPage), true, new Dictionary<string, object>
		{
			{ "Person", BindingContext.SelectedItem! }
		});
	}

	async void OnDelete(object sender, EventArgs eventArgs)
	{
		bool result = await DisplayAlert("Delete", "Delete the selected item, do you wish to continue?", "Yes", "No");

		if (result)
		{
			BindingContext.Delete();
			PeopleDataGrid.ScrollTo(BindingContext.SelectedItem!, ScrollToPosition.Center, true);
		}
	}

	#region Menu

	void OnFind(object sender, EventArgs eventArgs)
	{
		ShowFindDialogBox();
	}

	void OnFindNext(object sender, EventArgs eventArgs)
	{
		if (BindingContext.RequireSearchString)
		{
			ShowFindDialogBox();
		}
		else
		{
			FindInDataGridView();
		}
	}

	private async void ShowFindDialogBox()
	{
		SearchTermsViewModel    viewModel   = new();
		SearchTermsView         view        = new(viewModel);
		object?                 result      = await Shell.Current.ShowPopupAsync(view);

		if (result is bool boolResut && boolResut)
		{
			bool foundEntries = BindingContext.Find(viewModel.SearchTermsString);
			if (!foundEntries)
			{
				await DisplayAlert("Not Found", "No entries found for the specified search term(s).\nSearch string: "+viewModel.SearchTermsString , "OK");
			}
			else
			{
				FindInDataGridView();
			}
		}
	}

	private void FindInDataGridView()
	{
		BindingContext.SelectNextFoundItem();
		PeopleDataGrid.ScrollTo(BindingContext.SelectedItem!, ScrollToPosition.Center, true);
	}
	

	private void OnScrollToSelection(object sender, EventArgs eventArgs)
	{
		if (BindingContext.SelectedItem != null)
		{
			PeopleDataGrid.ScrollTo(BindingContext.SelectedItem, ScrollToPosition.Center, true);
		}
	}
	
	#endregion
}
using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class PersonEditPage : BasePage<PersonViewModel>
{
	public PersonEditPage(PersonViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	async public void OnSave(object sender, EventArgs eventArgs)
	{
		// Navigate back with a result.
		Dictionary<string, object> navigationParameter = new()
		{
			{ "NavigationCommand",	"Replace" },
			{ "NavigationObject",	BindingContext.Person! }
		};
		await Shell.Current.GoToAsync("../", true, navigationParameter);
	}

	async public void OnCancel(object sender, EventArgs eventArgs)
	{
		// Navigate back with a result.
		Dictionary<string, object> navigationParameter = new()
		{
			{ "NavigationCommand",	"Cancel" },
			{ "Result",				BindingContext.Person! }
		};
		await Shell.Current.GoToAsync("../", true, navigationParameter);
	}
}
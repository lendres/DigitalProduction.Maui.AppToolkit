using CommunityToolkit.Maui.Views;
using DigitalProduction.UI;
using DigitalProduction.ViewModels;
using DPMauiDemo.ViewModels;

namespace DPMauiDemo.Pages;

public partial class AboutPage : BasePage<EmptyViewModel>
{
	public AboutPage(EmptyViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}

	async void OnButtonClicked(object? sender, EventArgs args)
	{
		AboutView1 view = new(new AboutViewModel(true));
		_ = await Shell.Current.ShowPopupAsync(view);
	}
}
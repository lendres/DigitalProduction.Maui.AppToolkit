using CommunityToolkit.Maui.Views;
using DigitalProduction.Views;
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

	async void OnButtonAbout1Clicked(object? sender, EventArgs args)
	{
		AboutView1 view = new(
			new AboutViewModel(true)
			{
				ShowDocumentationAddress = false
			}
		);
		_ = await Shell.Current.ShowPopupAsync(view);
	}

	async void OnButtonAbout2Clicked(object? sender, EventArgs args)
	{
		AboutView1 view = new(new AboutViewModel(true));
		_ = await Shell.Current.ShowPopupAsync(view);
	}
}
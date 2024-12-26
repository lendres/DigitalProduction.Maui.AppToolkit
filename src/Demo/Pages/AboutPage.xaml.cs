using CommunityToolkit.Maui.Views;
using DigitalProduction.Views;
using DPMauiDemo.ViewModels;

namespace DPMauiDemo.Pages;

public partial class AboutPage : BasePage<AboutPageViewModel>
{
	public AboutPage(AboutPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	async void OnButtonAbout1Clicked(object? sender, EventArgs args)
	{
		AboutView1 view = new(
			new DigitalProduction.ViewModels.AboutViewModel(true)
			{
				ShowDocumentationAddress = false
			}
		);
		_ = await Shell.Current.ShowPopupAsync(view);
	}

	async void OnButtonAbout2Clicked(object? sender, EventArgs args)
	{
		AboutView1 view = new(new DigitalProduction.ViewModels.AboutViewModel(true));
		_ = await Shell.Current.ShowPopupAsync(view);
	}
}
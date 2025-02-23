using CommunityToolkit.Maui.Views;
using DigitalProduction.Maui.Views;
using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class AboutPage : BasePage<AboutPageViewModel>
{
	public AboutPage(AboutPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	async void OnButtonAbout1Clicked(object? sender, EventArgs eventArgs)
	{
		AboutView1 view = new(
			new DigitalProduction.Maui.ViewModels.AboutViewModel(true)
			{
				ShowDocumentationAddress = false
			}
		);
		_ = await Shell.Current.ShowPopupAsync(view);
	}

	async void OnButtonAbout2Clicked(object? sender, EventArgs eventArgs)
	{
		AboutView1 view = new(new DigitalProduction.Maui.ViewModels.AboutViewModel(true));
		_ = await Shell.Current.ShowPopupAsync(view);
	}
}
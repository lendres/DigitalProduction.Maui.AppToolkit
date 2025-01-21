using CommunityToolkit.Maui.Views;
using DigitalProduction.Maui.Views;
using DigitalProduction.Maui.ViewModels;
using DPMauiDemo.ViewModels;

namespace DPMauiDemo.Pages;

public partial class WelcomePage : BasePage<AboutViewModel>
{
	public WelcomePage(AboutViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	async void OnButtonAbout1Clicked(object? sender, EventArgs args)
	{
		AboutView1 view = new(
			new DigitalProduction.Maui.ViewModels.AboutViewModel(true)
			{
				ShowDocumentationAddress = false
			}
		);
		_ = await Shell.Current.ShowPopupAsync(view);
	}

	async void OnButtonAbout2Clicked(object? sender, EventArgs args)
	{
		AboutView1 view = new(new DigitalProduction.Maui.ViewModels.AboutViewModel(true));
		_ = await Shell.Current.ShowPopupAsync(view);
	}
}
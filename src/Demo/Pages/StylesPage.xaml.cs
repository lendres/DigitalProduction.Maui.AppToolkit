using CommunityToolkit.Maui.Storage;
using DigitalProduction.Demo.ViewModels;
namespace DigitalProduction.Demo.Pages;

public partial class StylesPage : BasePage<StylesPageViewModel>
{
	public StylesPage(StylesPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}
}
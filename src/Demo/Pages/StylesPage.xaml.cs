using DigitalProduction.Demo.ViewModels;
namespace DigitalProduction.Demo.Pages;

public partial class StylesPage : BasePage<StylesPageViewModel>
{
	public StylesPage(StylesPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
		MyPicker.SelectedIndex = 0;
		MySwitch.IsToggled = true;
		MySlider.Value = 0.35;
	}
}
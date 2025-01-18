using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Maui.Views;

public partial class AboutView1 : PopupView
{
	#region Construction

	public AboutView1(AboutViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	#endregion
}
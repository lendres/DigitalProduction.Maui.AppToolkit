using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Maui.Views;

public partial class SearchTermsView : PopupView
{
	#region Construction

	public SearchTermsView(SearchTermsViewModel viewModel)
	{
		BindingContext	= viewModel;
		InitializeComponent();
	}

	#endregion
}
using CommunityToolkit.Mvvm.ComponentModel;
using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Demo.ViewModels;

public partial class StylesPageViewModel : BaseViewModel
{
	#region Fields
	#endregion

	#region Construction

	public StylesPageViewModel()
	{
	}

	#endregion

	#region Properties

	[ObservableProperty]
	public partial bool							EnableControls { get; set; }

	#endregion
}
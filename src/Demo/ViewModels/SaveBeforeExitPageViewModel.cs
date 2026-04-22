using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalProduction.Maui.Validation;
using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Demo.ViewModels;

public partial class SaveBeforeExitPageViewModel : BaseViewModel
{
	#region Construction

	public SaveBeforeExitPageViewModel()
	{
		Save();
	}

	#endregion

	#region Properties

	[ObservableProperty]
	public partial string SaveText { get; set; } = "Saved";

	#endregion

	#region Methods

	[RelayCommand]
	private void Modify()
	{
		SaveText = "Modified";
	}

	[RelayCommand]
	private void Save()
	{
		SaveText = "Saved";
	}


	#endregion
}
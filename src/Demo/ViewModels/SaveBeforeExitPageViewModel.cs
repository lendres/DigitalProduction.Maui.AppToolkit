using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalProduction.Maui.Services;
using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Demo.ViewModels;

public partial class SaveBeforeExitPageViewModel : BaseViewModel
{
	#region Fields

	private bool					_isModified				= false;

	#endregion

	#region Construction

	public SaveBeforeExitPageViewModel()
	{
		Save();

		ISaveService saveBeforeExitService			= DigitalProduction.Maui.Services.ServiceProvider.GetService<ISaveService>();
		saveBeforeExitService.IsModifiedFunction	= IsModified;
		saveBeforeExitService.SaveFunction			= SaveAsync;
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
		SaveText	= "Modified";
		_isModified	= true;
	}

	[RelayCommand]
	private void Save()
	{
		SaveText	= "Saved";
		_isModified	= false;
	}

	public bool IsModified() => _isModified;

	async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
	{
		Save();
		return true;
	}

	#endregion
}
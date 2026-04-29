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

	public SaveBeforeExitPageViewModel(ISaveService saveBeforeExitService)
	{
		Save();

		SaveBeforeExitService						= saveBeforeExitService;
		SaveBeforeExitService.IsModifiedFunction	= IsModified;
		SaveBeforeExitService.SaveFunction			= SaveAsync;
	}

	#endregion

	#region Properties

	[ObservableProperty]
	public partial string SaveText { get; set; } = "Saved";

	public ISaveService SaveBeforeExitService { get; private set; }

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

	[RelayCommand]
	private async Task PromptForSave()
	{
		SaveChoice closeChoice = await SaveBeforeExitService.PromptSaveChangesAsync();

		switch (closeChoice)
		{
			case SaveChoice.ContinueWithoutSaving:
				SaveText = "ViewModel without Saving";
				break;
			default:
				return;
		}
	}

	public bool IsModified() => _isModified;

	async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
	{
		Save();
		return true;
	}

	#endregion
}
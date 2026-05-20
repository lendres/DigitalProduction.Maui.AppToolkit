using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalProduction.Maui.Services;
using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Demo.ViewModels;

public partial class SaveBeforeExitPageViewModel : BaseViewModel
{
	#region Construction

	public SaveBeforeExitPageViewModel(ISaveService saveBeforeExitService)
	{
		Save();

		SaveService						= saveBeforeExitService;
		SaveService.IsModifiedFunction	= GetIsModified;
		SaveService.SaveFunction		= SaveAsync;
	}

	#endregion

	#region Properties

	[ObservableProperty]
	public partial string SaveText { get; set; } = "Saved";

	[ObservableProperty]
	public partial bool IsModified { get; set; } = false;

	public ISaveService SaveService { get; private set; }

	#endregion

	#region Methods

	[RelayCommand]
	private void Modify()
	{
		SaveText	= "Modified";
		IsModified	= true;
	}

	[RelayCommand]
	private void Save()
	{
		SaveText	= "Saved";
		IsModified	= false;
	}

	[RelayCommand]
	private async Task PromptForSave()
	{
		SaveChoice closeChoice = await SaveService.PromptSaveChangesAsync();

		switch (closeChoice)
		{
			case SaveChoice.ContinueWithoutSaving:
				SaveText = "ViewModel without Saving";
				break;
			default:
				return;
		}
	}

	public bool GetIsModified() => IsModified;

	async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
	{
		Save();
		return true;
	}

	#endregion
}
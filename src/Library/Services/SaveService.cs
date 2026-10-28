using CommunityToolkit.Maui.ApplicationModel;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using DigitalProduction.Maui.Views;

namespace DigitalProduction.Maui.Services;

public class SaveService : ISaveService
{
	#region Fields

	private Func<bool>?								_isModifiedFunction			= null;
	private Func<CancellationToken, Task<bool>>?	_saveFunction				= null;

	private readonly IPageProvider					_pageProvider;

	private const string							_saveAndContinueText		= "Save and Continue";
    private const string							_continueWithoutSavingText	= "Continue without Saving";
    private const string							_cancelOptionText			= "Cancel";

	#endregion

	#region Construction

    public SaveService(IPageProvider pageProvider)
    {
		_pageProvider = pageProvider;
    }

	#endregion

	#region Properties

	public bool IsModified
	{
		get
		{
			if (_isModifiedFunction != null)
			{
				return _isModifiedFunction();
			}
			else
			{
				return false;
			}
		}
	}

	public Func<bool>? IsModifiedFunction
	{
		private get	=> _isModifiedFunction;
		set			=> _isModifiedFunction = value ?? throw new ArgumentNullException(nameof(value));
	}

	public Func<CancellationToken, Task<bool>>? SaveFunction
	{
		private get	=> _saveFunction;
		set			=> _saveFunction = value ?? throw new ArgumentNullException(nameof(value));
	}

	#endregion

	#region Methods

	public async Task<SaveChoice> PromptSaveChangesAsync()
	{
		if (!IsModified)
		{
			return SaveChoice.SaveNotRequired;
		}

		Page? page = _pageProvider.Page;

		if (page == null)
		{
			return SaveChoice.Cancel;
		}

		var result = await page.ShowPopupAsync<SaveChoice>(new SaveChangesView(), new PopupOptions { CanBeDismissedByTappingOutsideOfPopup = false, Shape = null, Shadow = null });
		SaveChoice saveChoice	= result.WasDismissedByTappingOutsideOfPopup ? SaveChoice.Cancel : result.Result;

		if (saveChoice == SaveChoice.SaveAndContinue)
		{
			bool saveSucceeded = await SaveAsync();
			return saveSucceeded ? SaveChoice.SaveAndContinue : SaveChoice.Cancel;
		}
		else
		{
			return saveChoice;
		}
	}

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
		if (IsModified && SaveFunction != null)
		{
			return await SaveFunction(cancellationToken);
		}
		return true;
	}

	#endregion
}
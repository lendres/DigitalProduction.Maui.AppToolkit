using CommunityToolkit.Maui.ApplicationModel;

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
			return SaveChoice.SavingNotRequired;
		}

		Page? page = _pageProvider.CurrentPage;

        if (page == null)
        {
            return SaveChoice.Cancel;
        }

        string result = await page.DisplayActionSheet(
            "Do you want to save the changes?",
            null,
            null,
            _saveAndContinueText,
            _continueWithoutSavingText,
			_cancelOptionText);

        switch (result)
        {
			case _saveAndContinueText:
				bool saveSucceeded = await SaveAsync();
				return saveSucceeded ? SaveChoice.SaveAndContinue : SaveChoice.Cancel;

			case _continueWithoutSavingText:
				return SaveChoice.ContinueWithoutSaving;

			default:
				return SaveChoice.Cancel;
        };
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
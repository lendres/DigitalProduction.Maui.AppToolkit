namespace DigitalProduction.Maui.Services;

public class SaveService : ISaveService
{
	#region Fields

	private Func<bool>?								_isModifiedFunction	= null;
	private Func<CancellationToken, Task<bool>>?	_saveFunction		= null;

	#endregion

	#region Construction

    public SaveService()
    {
    }

    public SaveService(Func<CancellationToken, Task<bool>> saveFunction)
    {
        _saveFunction = saveFunction ?? throw new ArgumentNullException(nameof(saveFunction));
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
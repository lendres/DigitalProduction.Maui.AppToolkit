using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalProduction.Maui.Services;

public class SaveService : ISaveService
{
	#region Fields

	private Func<CancellationToken, Task>? _saveFunction = null;

	#endregion

	#region Construction

    public SaveService()
    {
    }

    public SaveService(Func<CancellationToken, Task> saveFunction)
    {
        _saveFunction = saveFunction ?? throw new ArgumentNullException(nameof(saveFunction));
    }

	#endregion

	#region Properties

	public bool HasUnsavedChanges { get; private set; }

	#endregion

	#region Methods

	public void MarkDirty()
    {
        HasUnsavedChanges = true;
    }

    public void MarkClean()
    {
        HasUnsavedChanges = false;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
		if (_saveFunction != null)
		{
			await _saveFunction(cancellationToken);
			HasUnsavedChanges = false;
		}
	}

	#endregion
}
namespace DigitalProduction.Maui.Services;

public interface ISaveService
{
    bool IsModified { get; }

	Func<bool>? IsModifiedFunction { set; }

	Func<CancellationToken, Task<bool>>? SaveFunction { set; }

    Task<bool> SaveAsync(CancellationToken cancellationToken = default);
}
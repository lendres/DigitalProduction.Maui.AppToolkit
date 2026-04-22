namespace DigitalProduction.Maui.Services;

public interface ISaveService
{
    bool HasUnsavedChanges { get; }

    void MarkDirty();

    void MarkClean();

    Task SaveAsync(CancellationToken cancellationToken = default);
}
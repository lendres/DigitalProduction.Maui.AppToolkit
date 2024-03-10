namespace DigitalProduction.ComponentModel;

/// <summary>
/// Provides and interface for an object that is revertible.  The object can save its current state and after changes are made to it, either keep
/// the changes or revert to the saved state.
/// </summary>
public interface IRevertible
{
	#region Functions.

	/// <summary>
	/// Beginning of the edit.  Current state should be saved.
	/// </summary>
	void BeginEdit();

	/// <summary>
	/// Reject the changes made since the last BeginEdit.
	/// </summary>
	void RejectChanges();

	/// <summary>
	/// Accept (commit) the changes made since the last BeginEdit.
	/// </summary>
	void AcceptChanges();

	#endregion

} // End interface.
namespace DigitalProduction.UI;

public partial class DigitalProductionMainPage : ContentPage
{
	#region Fields

	#endregion

	#region Construction

	public DigitalProductionMainPage()
	{
		this.Loaded += this.OnLoaded;	
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	void OnLoaded(object? sender, EventArgs eventArgs)
	{
		InstallOnLoaded(sender, eventArgs);
	}

	partial void InstallOnLoaded(object? sender, EventArgs eventArgs);

	/// <summary>
	/// Exit command.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="eventArgs">Event arguments.</param>
	protected void OnExit(object sender, EventArgs eventArgs)
	{
		Application.Current?.Quit();
	}

	#endregion
}
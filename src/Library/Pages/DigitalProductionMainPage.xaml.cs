using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Maui.Pages;

public partial class DigitalProductionMainPage<TViewModel> : BasePage<TViewModel> where TViewModel : BaseViewModel
{
	#region Fields

	#endregion

	#region Construction

	public DigitalProductionMainPage(object? viewModel = null) :
		base(viewModel)
	{
		Loaded += OnLoaded;	
	}

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
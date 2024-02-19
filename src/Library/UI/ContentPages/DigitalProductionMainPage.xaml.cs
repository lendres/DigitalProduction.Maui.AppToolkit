using Microsoft.Maui.Platform;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace DigitalProduction.UI;

public partial class DigitalProductionMainPage : ContentPage
{
	#region Fields

	#endregion

	#region Construction

	public DigitalProductionMainPage()
	{
		//		InitializeComponent();
		//this.Loaded += this.OnLoaded;
		DoNothing();
	}

	#endregion

	#region Properties

	#endregion

	#region Methods
	partial void DoNothing();

	//private partial void OnLoaded(object sender, EventArgs eventArgs);

	/// <summary>
	/// Exit command.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="eventArgs">Event arguments.</param>
	protected void OnExit(object sender, EventArgs eventArgs)
	{
		Application.Current.Quit();
	}

	#endregion
}
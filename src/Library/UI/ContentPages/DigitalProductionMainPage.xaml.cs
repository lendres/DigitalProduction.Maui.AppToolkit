namespace DigitalProduction.Controls;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
#if WINDOWS
using Microsoft.UI.Windowing;
#endif

public partial class DigitalProductionMainPage : ContentPage
{
	#region Fields

	#endregion

	#region Construction

	public DigitalProductionMainPage()
	{
		InitializeComponent();
		this.Loaded += this.OnLoaded;
		
	}

	private void OnLoaded(object sender, EventArgs eventArgs)
	{
	}

	#endregion
}
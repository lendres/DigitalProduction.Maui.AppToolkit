using DigitalProduction.Maui.ViewModels;
using DigitalProduction.Maui.Services;

namespace DigitalProduction.Maui.Views;

public partial class SaveChangesView : ThreeButtonView<SaveChoice>
{
	#region Construction

	public SaveChangesView()
	{
		Title			= "Unsaved Changes";
		Button1Value	= SaveChoice.SaveAndContinue;
		Button2Value	= SaveChoice.ContinueWithoutSaving;
		Button3Value	= SaveChoice.Cancel;
	}

	#endregion
}
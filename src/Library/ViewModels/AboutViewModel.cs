using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Data.Translation.Validation;
using DigitalProduction.Validation;

namespace DigitalProduction.ViewModels;

public partial class AboutViewModel : ObservableObject
{
	#region Fields

	[ObservableProperty]
	private string				_title					= "";
	
	[ObservableProperty]
	private string				_authors				= "";

	[ObservableProperty]
	private string				_website				= "";

	[ObservableProperty]
	private string				_issuesAddress			= "";

	[ObservableProperty]
	private string				_version				= "";

	#endregion

	#region Construction

	public AboutViewModel()
	{
	}

	#endregion
		
	#region Properties
	#endregion
}
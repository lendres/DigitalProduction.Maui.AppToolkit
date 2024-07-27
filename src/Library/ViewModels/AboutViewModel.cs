using CommunityToolkit.Mvvm.ComponentModel;

namespace DigitalProduction.ViewModels;

public partial class AboutViewModel : ObservableObject
{
	#region Fields

	[ObservableProperty]
	private string				_title					= "";
	
	[ObservableProperty]
	private string				_product				= "";

	[ObservableProperty]
	private string				_version				= "";

	[ObservableProperty]
	private string				_authors				= "";

	[ObservableProperty]
	private string				_copyright				= "";

	[ObservableProperty]
	private string				_company				= "";

	[ObservableProperty]
	private string				_Description			= "";

	[ObservableProperty]
	private string				_website				= "";

	[ObservableProperty]
	private string				_issuesAddress			= "";



	#endregion

	#region Construction

	public AboutViewModel()
	{
		System.Reflection.Assembly? entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
		System.Diagnostics.Debug.Assert(entryAssembly != null);

		Title			= string.Format("About {0}", DigitalProduction.Reflection.Assembly.Product(entryAssembly));
		Product			= DigitalProduction.Reflection.Assembly.Product(entryAssembly);
		Version			= DigitalProduction.Reflection.Assembly.Version(entryAssembly);
		Authors			= DigitalProduction.Reflection.Assembly.Authors(entryAssembly);
		Copyright		= DigitalProduction.Reflection.Assembly.Copyright(entryAssembly);
		Company			= DigitalProduction.Reflection.Assembly.Company(entryAssembly);
		Description		= DigitalProduction.Reflection.Assembly.Description(entryAssembly);
		Website			= DigitalProduction.Reflection.Assembly.Website(entryAssembly);
		IssuesAddress	= DigitalProduction.Reflection.Assembly.IssuesAddress(entryAssembly);
	}

	#endregion
		
	#region Properties
	#endregion
}
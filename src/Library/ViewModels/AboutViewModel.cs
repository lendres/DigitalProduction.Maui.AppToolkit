using CommunityToolkit.Mvvm.ComponentModel;

namespace DigitalProduction.Maui.ViewModels;

public partial class AboutViewModel : BaseViewModel
{
	#region Fields
	#endregion

	#region Construction

	public AboutViewModel() :
		this(true)
	{
	}

	public AboutViewModel(bool threeDigitVersion)
	{
		System.Reflection.Assembly? entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
		System.Diagnostics.Debug.Assert(entryAssembly != null);

		System.Reflection.Assembly? thisAssembly = System.Reflection.Assembly.GetCallingAssembly();
		System.Diagnostics.Debug.Assert(thisAssembly != null);

		Title					= string.Format("About {0}", DigitalProduction.Reflection.Assembly.Product(entryAssembly));
		Product					= DigitalProduction.Reflection.Assembly.Product(entryAssembly);
		Version					= DigitalProduction.Reflection.Assembly.Version(entryAssembly, threeDigitVersion);
		AppToolkitVersion		= DigitalProduction.Reflection.Assembly.Version(thisAssembly, threeDigitVersion);
		Authors					= DigitalProduction.Reflection.Assembly.Authors(entryAssembly);
		Copyright				= DigitalProduction.Reflection.Assembly.Copyright(entryAssembly);
		Company					= DigitalProduction.Reflection.Assembly.Company(entryAssembly);
		Description				= DigitalProduction.Reflection.Assembly.Description(entryAssembly);
		Website					= DigitalProduction.Reflection.Assembly.Website(entryAssembly);
		IssuesAddress			= DigitalProduction.Reflection.Assembly.IssuesAddress(entryAssembly);
		DocumentationAddress	= DigitalProduction.Reflection.Assembly.DocumentationAddress(entryAssembly);
	}

	#endregion

	#region Properties

	[ObservableProperty]
	public partial string			Title { get; set; }						= "";
	
	[ObservableProperty]
	public partial string			Product { get; set; }					= "";

	[ObservableProperty]
	public partial string			Version { get; set; }					= "";

	[ObservableProperty]
	public partial string			AppToolkitVersion { get; set; }			= "";

	[ObservableProperty]
	public partial bool				ShowVersion { get; set; }				= true;

	[ObservableProperty]
	public partial string			Authors { get; set; }					= "";

	[ObservableProperty]
	public partial bool				ShowAuthors { get; set; }				= true;

	[ObservableProperty]
	public partial string			Copyright { get; set; }					= "";

	[ObservableProperty]
	public partial bool				ShowCopyright { get; set; }				= true;

	[ObservableProperty]
	public partial string			Company { get; set; }					= "";

	[ObservableProperty]
	public partial bool				ShowCompany { get; set; }				= true;

	[ObservableProperty]
	public partial string			Description { get; set; }				= "";

	[ObservableProperty]
	public partial bool				ShowDescription { get; set; }			= true;

	[ObservableProperty]
	public partial string			Website { get; set; }					= "";

	[ObservableProperty]
	public partial bool				ShowWebsite { get; set; }				= true;

	[ObservableProperty]
	public partial string			IssuesAddress { get; set; }				= "";

	[ObservableProperty]
	public partial bool				ShowIssuesAddress { get; set; }			= true;

	[ObservableProperty]
	public partial string			DocumentationAddress { get; set; }		= "";

	[ObservableProperty]
	public partial bool				ShowDocumentationAddress { get; set; }	= true;

	#endregion
		
	#region Properties
	#endregion
}
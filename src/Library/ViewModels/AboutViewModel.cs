using CommunityToolkit.Mvvm.ComponentModel;

namespace DigitalProduction.ViewModels;

public partial class AboutViewModel : BaseViewModel
{
	#region Fields

	[ObservableProperty]
	private string				_title						= "";
	
	[ObservableProperty]
	private string				_product					= "";

	[ObservableProperty]
	private string				_version					= "";

	[ObservableProperty]
	private bool				_showVersion				= true;

	[ObservableProperty]
	private string				_authors					= "";

	[ObservableProperty]
	private bool				_showAuthors				= true;

	[ObservableProperty]
	private string				_copyright					= "";

	[ObservableProperty]
	private bool				_showCopyright				= true;

	[ObservableProperty]
	private string				_company					= "";

	[ObservableProperty]
	private bool				_showCompany				= true;

	[ObservableProperty]
	private string				_Description				= "";

	[ObservableProperty]
	private bool				_showDescription			= true;

	[ObservableProperty]
	private string				_website					= "";

	[ObservableProperty]
	private bool				_showWebsite				= true;

	[ObservableProperty]
	private string				_issuesAddress				= "";

	[ObservableProperty]
	private bool				_showIssuesAddress			= true;

	[ObservableProperty]
	private string				_documentationAddress		= "";

	[ObservableProperty]
	private bool				_showDocumentationAddress	= true;

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

		Title					= string.Format("About {0}", DigitalProduction.Reflection.Assembly.Product(entryAssembly));
		Product					= DigitalProduction.Reflection.Assembly.Product(entryAssembly);
		Version					= DigitalProduction.Reflection.Assembly.Version(entryAssembly, threeDigitVersion);
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
	#endregion
}
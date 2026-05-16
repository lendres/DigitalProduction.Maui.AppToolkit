using CommunityToolkit.Maui.Storage;
using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class PathValidationPage : BasePage<PathValidationPageViewModel>
{
	#region Construction

	public PathValidationPage(PathValidationPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	#endregion

	#region Events

	async void OnBrowseForInputFile(object sender, EventArgs eventArgs)
	{
		PickOptions pickOptions = new() { PickerTitle="Select an Input File", FileTypes=DigitalProduction.Maui.IO.FileTypes.Xml };
		FileResult? result      = await BrowseForFile(pickOptions);

		if (result != null)
		{
			InputFileEntry.Text = result.FullPath;
		}
	}

	async void OnBrowseForAuxiliaryFile(object sender, EventArgs eventArgs)
	{
		PickOptions pickOptions = new() { PickerTitle="Select an Auxiliary File", FileTypes=DigitalProduction.Maui.IO.FileTypes.Xml };
		FileResult? result = await BrowseForFile(pickOptions);

		if (result != null)
		{
			AuxiliaryFileEntry.Text = result.FullPath;
		}
	}

	async void OnBrowseOutputDirectory(object sender, EventArgs eventArgs)
	{
		CancellationToken cancellationToken	= new();
		FolderPickerResult folderResult		= await FolderPicker.PickAsync(OutputDirectoryEntry.Text, cancellationToken);
		if (folderResult.IsSuccessful)
		{
			OutputDirectoryEntry.Text = folderResult.Folder.Path;
		}
	}

	public static async Task<FileResult?> BrowseForFile(PickOptions options)
	{
		try
		{
			return await FilePicker.PickAsync(options);
		}
		catch
		{
			// The user canceled or something went wrong.
		}

		return null;
	}

	async void OnSubmit(object sender, EventArgs eventArgs)
	{
		await DisplayAlert("Success", "All entries are valid!", "Ok");
	}

	#endregion
}
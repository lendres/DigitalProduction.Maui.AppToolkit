using CommunityToolkit.Maui.Storage;
using DigitalProduction.Demo.ViewModels;
namespace DigitalProduction.Demo.Pages;

public partial class ValidationPage : BasePage<ValidationPageViewModel>
{
	public ValidationPage(ValidationPageViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}

	#region Events

	async void OnBrowseForInputFile(object sender, EventArgs eventArgs)
	{
		ValidationPageViewModel? viewModel = BindingContext as ValidationPageViewModel;
		System.Diagnostics.Debug.Assert(viewModel != null);
		
		PickOptions pickOptions = new() { PickerTitle="Select an Input File", FileTypes=DigitalProduction.Maui.IO.FileTypes.Xml };
		FileResult? result      = await BrowseForFile(pickOptions);

		if (result != null)
		{
			InputFileEntry.Text = result.FullPath;
		}
	}

	async void OnBrowseOutputDirectory(object sender, EventArgs eventArgs)
	{
		CancellationToken cancellationToken = new();
		FolderPickerResult folderResult = await FolderPicker.PickAsync(OutputDirectoryEntry.Text, cancellationToken);
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
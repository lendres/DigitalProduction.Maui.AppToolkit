using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalProduction.Maui.Validation;
using DigitalProduction.Maui.ViewModels;

namespace DigitalProduction.Demo.ViewModels;

public partial class PathValidationPageViewModel : BaseViewModel
{
	#region Construction

	public PathValidationPageViewModel()
	{
		AddValidations();
	}

	#endregion

	#region Properties

	// Input file.
	[ObservableProperty, NotifyPropertyChangedFor(nameof(IsSubmittable))]
	public partial ValidatableObject<string>		InputFile { get; set; }							= new();

	// Directory.
	[ObservableProperty, NotifyPropertyChangedFor(nameof(IsSubmittable))]
	public partial ValidatableObject<string>		OutputDirectory { get; set; }					= new();

	[ObservableProperty]
	public partial bool								IsSubmittable { get; set; }

	#endregion

	#region Validation

	private void AddValidations()
	{
		InputFile.Validations.Add(new IsNotNullOrEmptyRule	{ ValidationMessage = "A file name is required." });
		InputFile.Validations.Add(new FileExistsRule		{ ValidationMessage = "The file does not exist." });
		ValidateInputFile();

		OutputDirectory.Validations.Add(new IsNotNullOrEmptyRule		{ ValidationMessage = "A directory is required." });
		OutputDirectory.Validations.Add(new DirectoryNameIsValidRule	{ ValidationMessage = "The directory path is not valid." });
		OutputDirectory.Validations.Add(new DirectoryExistsRule			{ ValidationMessage = "The directory does not exist." });
		ValidateOutputDirectory();
	}

	[RelayCommand]
	private void ValidateInputFile()
	{
		InputFile.Validate();
		ValidateSubmittable();
	}

	[RelayCommand]
	private void ValidateOutputDirectory()
	{
		OutputDirectory.Validate();
		ValidateSubmittable();
	}

	public bool ValidateSubmittable() => IsSubmittable = InputFile.IsValid && OutputDirectory.IsValid;

	#endregion
}
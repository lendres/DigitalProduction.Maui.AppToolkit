using CommunityToolkit.Maui.Behaviors;
using DigitalProduction.IO;
using DigitalProduction.Maui.Behaviors;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using Path = System.IO.Path;

namespace DigitalProduction.UnitTests;

[Collection("MAUI dispatching")]
public class ValidationBehaviorTests : IDisposable
{
	readonly IDispatcherProvider originalDispatcher = DispatcherProvider.Current;

	public ValidationBehaviorTests() => DispatcherProvider.SetCurrent(new TestDispatcher());
	public void Dispose() => DispatcherProvider.SetCurrent(originalDispatcher);

	sealed class TestDispatcher : IDispatcherProvider, IDispatcher
	{
		public IDispatcher GetForCurrentThread() => this;
		public bool IsDispatchRequired => false;
		public bool Dispatch(Action action) { action(); return true; }
		public bool DispatchDelayed(TimeSpan delay, Action action) => throw new NotSupportedException();
		public IDispatcherTimer CreateTimer() => throw new NotSupportedException();
	}
	[Fact]
	public async Task FileValidationUpdatesValidityAndError()
	{
		using var behavior = new FileExistsValidationBehavior { Value = Path.GetFullPath("Test File.txt") };
		await behavior.ForceValidate();
		Assert.True(behavior.IsValid);
		Assert.False(behavior.IsNotValid);
		Assert.Equal(PathErrorType.None, behavior.Error);

		behavior.Value += ".missing";
		await behavior.ForceValidate();
		Assert.False(behavior.IsValid);
		Assert.True(behavior.IsNotValid);
		Assert.Equal(PathErrorType.FileNotFound, behavior.Error);
		Assert.False(behavior.IsRunning);
	}

	[Fact]
	public async Task DirectoryValidationDistinguishesFilesAndDirectories()
	{
		using var behavior = new DirectoryExistsValidationBehavior { Value = AppContext.BaseDirectory };
		await behavior.ForceValidate();
		Assert.True(behavior.IsValid);
		Assert.Equal(PathErrorType.None, behavior.Error);
		behavior.Value = Path.GetFullPath("Test File.txt");
		await behavior.ForceValidate();
		Assert.False(behavior.IsValid);
		Assert.Equal(PathErrorType.DirectoryNotFound, behavior.Error);
	}

	[Fact]
	public async Task FileNameValidationRechecksPathRequirementWithoutRecursingOnError()
	{
		using var behavior = new FileNameValidationBehavior
		{
			Flags = ValidationFlags.ValidateOnValueChanged,
			Value = Path.Combine(AppContext.BaseDirectory, Guid.NewGuid().ToString(), "file.txt")
		};
		await behavior.ForceValidate();
		Assert.True(behavior.IsValid);
		Assert.Equal(PathValidationResult.Valid, behavior.Error);
		behavior.RequireFileToExist = true;
		Assert.False(behavior.IsValid);
		Assert.NotEqual(PathValidationResult.Valid, behavior.Error);
		behavior.RequireFileToExist = false;
		Assert.True(behavior.IsValid);
	}

	[Fact]
	public async Task CanceledValidationDoesNotChangeValidity()
	{
		using var behavior = new FileExistsValidationBehavior { Value = "missing" };
		using var cancellation = new CancellationTokenSource();
		cancellation.Cancel();
		await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await behavior.ForceValidate(cancellation.Token));
		Assert.True(behavior.IsValid);
		Assert.False(behavior.IsRunning);
	}

	#region File Name Validation

	[Theory]
	[InlineData("existing", false, PathValidationResult.Valid)]
	[InlineData("existing", true, PathValidationResult.Valid)]
	[InlineData("missing", false, PathValidationResult.Valid)]
	[InlineData("missing", true, PathValidationResult.PathDoesNotExist)]
	[InlineData("missing-directory", false, PathValidationResult.Valid)]
	[InlineData("missing-directory", true, PathValidationResult.PathDoesNotExist)]
	[InlineData("directory", false, PathValidationResult.Valid)]
	[InlineData("directory", true, PathValidationResult.PathDoesNotExist)]
	public async Task FileNameValidationChecksFullPathsAndOptionalFileExistence(string scenario, bool requireFileToExist, PathValidationResult expected)
	{
		using FileNameValidationFiles files = new();
		string value = scenario switch
		{
			"existing"          => files.ExistingFile,
			"missing"           => Path.Combine(files.DirectoryPath, "missing.txt"),
			"missing-directory" => Path.Combine(files.DirectoryPath, "missing", "input.txt"),
			"directory"         => files.DirectoryPath,
			_                   => throw new ArgumentOutOfRangeException(nameof(scenario))
		};
		Assert.True(Path.IsPathFullyQualified(value));
		if (scenario == "existing")
		{
			Assert.True(File.Exists(value));
		}

		using FileNameValidationBehavior behavior = new()
		{
			RequireFileToExist	= requireFileToExist,
			Value				= value
		};
		await behavior.ForceValidate();

		Assert.Equal(expected, behavior.Error);
		Assert.Equal(expected == PathValidationResult.Valid, behavior.IsValid);
		Assert.Equal(expected != PathValidationResult.Valid, behavior.IsNotValid);
		Assert.False(behavior.IsRunning);
	}

	[Theory]
	[InlineData(null, false, PathValidationResult.ZeroLength)]
	[InlineData(null, true, PathValidationResult.ZeroLength)]
	[InlineData("", false, PathValidationResult.ZeroLength)]
	[InlineData("", true, PathValidationResult.ZeroLength)]
	[InlineData("   ", false, PathValidationResult.ZeroLength)]
	[InlineData("   ", true, PathValidationResult.ZeroLength)]
	[InlineData("invalid?name.txt", false, PathValidationResult.InvalidCharacters)]
	[InlineData("invalid?name.txt", true, PathValidationResult.InvalidCharacters)]
	[InlineData(".hidden", false, PathValidationResult.FileNameNotProvided)]
	[InlineData(".hidden", true, PathValidationResult.FileNameNotProvided)]
	[InlineData("CON.txt", false, PathValidationResult.DeviceName)]
	[InlineData("CON.txt", true, PathValidationResult.DeviceName)]
	public async Task FileNameValidationPreservesSpecificSyntaxErrors(string? name, bool requireFileToExist, PathValidationResult expected)
	{
		using FileNameValidationFiles files = new();
		string? value = string.IsNullOrWhiteSpace(name) ? name : Path.Combine(files.DirectoryPath, name);
		using FileNameValidationBehavior behavior = new()
		{
			RequireFileToExist	= requireFileToExist,
			Value				= value
		};
		await behavior.ForceValidate();

		Assert.Equal(expected, behavior.Error);
		Assert.False(behavior.IsValid);
		Assert.True(behavior.IsNotValid);
		Assert.False(behavior.IsRunning);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task FileNameValidationRejectsTooLongNamesAndDirectoryOnlyPaths(bool requireFileToExist)
	{
		using FileNameValidationFiles files = new();
		using FileNameValidationBehavior behavior = new()
		{
			RequireFileToExist	= requireFileToExist,
			Value				= Path.Combine(files.DirectoryPath, new string('a', 256))
		};
		await behavior.ForceValidate();
		Assert.Equal(PathValidationResult.TooLong, behavior.Error);
		Assert.True(behavior.IsNotValid);

		behavior.Value = files.DirectoryPath + Path.DirectorySeparatorChar;
		await behavior.ForceValidate();
		Assert.Equal(PathValidationResult.ZeroLength, behavior.Error);
		Assert.True(behavior.IsNotValid);
	}

	[Fact]
	public async Task FileNameValidationRevalidatesWhenTheExistenceOptionOrValueChanges()
	{
		using FileNameValidationFiles files = new();
		using FileNameValidationBehavior behavior = new()
		{
			Flags	= ValidationFlags.ValidateOnValueChanged,
			Value	= Path.Combine(files.DirectoryPath, "missing.txt")
		};
		await behavior.ForceValidate();
		Assert.Equal(PathValidationResult.Valid, behavior.Error);

		behavior.RequireFileToExist = true;
		Assert.Equal(PathValidationResult.PathDoesNotExist, behavior.Error);
		Assert.True(behavior.IsNotValid);

		behavior.Value = files.ExistingFile;
		Assert.Equal(PathValidationResult.Valid, behavior.Error);
		Assert.True(behavior.IsValid);

		behavior.Value = Path.Combine(files.DirectoryPath, "invalid?name.txt");
		Assert.Equal(PathValidationResult.InvalidCharacters, behavior.Error);
		Assert.True(behavior.IsNotValid);

		behavior.RequireFileToExist = false;
		Assert.Equal(PathValidationResult.InvalidCharacters, behavior.Error);

		behavior.Value = Path.Combine(files.DirectoryPath, "missing.txt");
		Assert.Equal(PathValidationResult.Valid, behavior.Error);
		Assert.True(behavior.IsValid);
		Assert.False(behavior.IsRunning);
	}

	[Fact]
	public async Task FileNameValidationRechecksTheFileSystemWhenForced()
	{
		using FileNameValidationFiles files = new();
		using FileNameValidationBehavior behavior = new()
		{
			RequireFileToExist	= true,
			Value				= files.ExistingFile
		};
		await behavior.ForceValidate();
		Assert.Equal(PathValidationResult.Valid, behavior.Error);

		File.Delete(files.ExistingFile);
		await behavior.ForceValidate();
		Assert.Equal(PathValidationResult.PathDoesNotExist, behavior.Error);
		Assert.True(behavior.IsNotValid);

		File.WriteAllText(files.ExistingFile, "restored");
		await behavior.ForceValidate();
		Assert.Equal(PathValidationResult.Valid, behavior.Error);
		Assert.True(behavior.IsValid);
	}

	/// <summary>
	/// Owns an isolated directory and file for full-path filename validation.
	/// </summary>
	private sealed class FileNameValidationFiles : IDisposable
	{
		public string DirectoryPath { get; }
		public string ExistingFile { get; }

		public FileNameValidationFiles()
		{
			string uniqueName		= "FileNameValidation-" + Guid.NewGuid().ToString("N");
			DirectoryPath			= Path.Combine(Directory.GetCurrentDirectory(), uniqueName);
			ExistingFile			= Path.Combine(DirectoryPath, "input file.txt");

			Directory.CreateDirectory(DirectoryPath);
			File.WriteAllText(ExistingFile, "existing");
		}

		public void Dispose()
		{
			File.Delete(ExistingFile);
			Directory.Delete(DirectoryPath);
		}
	}

	#endregion

	[Fact]
	public async Task UnsetValuesAreInvalidInsteadOfThrowing()
	{
		using var file = new FileExistsValidationBehavior();
		using var directory = new DirectoryExistsValidationBehavior();
		using var name = new FileNameValidationBehavior();
		await file.ForceValidate();
		await directory.ForceValidate();
		await name.ForceValidate();
		Assert.Equal(PathErrorType.FileNotFound, file.Error);
		Assert.Equal(PathErrorType.DirectoryNotFound, directory.Error);
		Assert.Equal(PathValidationResult.ZeroLength, name.Error);
		Assert.True(file.IsNotValid && directory.IsNotValid && name.IsNotValid);
	}
}

[CollectionDefinition("MAUI dispatching", DisableParallelization = true)]
public class MauiDispatchingCollection;

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

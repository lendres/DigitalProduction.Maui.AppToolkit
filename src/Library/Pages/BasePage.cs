using DigitalProduction.Maui.ViewModels;
using System.Diagnostics;

namespace DigitalProduction.Maui.Pages;

public abstract class BasePage<TViewModel>(TViewModel viewModel) : BasePage(viewModel) where TViewModel : BaseViewModel
{
	public BasePage(object? viewModel = null) : base(viewModel)
	{
	}

	public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

public abstract class BasePage : ContentPage
{
	protected BasePage(object? viewModel = null)
	{
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Debug.WriteLine($"OnAppearing: {Title}");
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		Debug.WriteLine($"OnDisappearing: {Title}");
	}
}
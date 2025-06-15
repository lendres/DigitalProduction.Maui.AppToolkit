using DigitalProduction.Demo.ViewModels;

namespace DigitalProduction.Demo.Pages;

public partial class DataGridStyleExamplePage : BasePage<DataGridViewModel>
{
	public DataGridStyleExamplePage(DataGridViewModel viewModel) :
		base(viewModel)
	{
		InitializeComponent();
	}
}
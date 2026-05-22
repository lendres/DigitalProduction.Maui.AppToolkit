using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalProduction.Maui.Validation;
using DigitalProduction.Maui.ViewModels;
using DigitalProduction.Demo.Enums;

namespace DigitalProduction.Demo.ViewModels;

public partial class ConvertersPageViewModel : BaseViewModel
{
	#region Construction

	public ConvertersPageViewModel()
	{
	}

	#endregion

	#region Properties

	[ObservableProperty]
	public partial TestingType EnumValue { get; set; } = TestingType.Type1;


	#endregion

	#region Commands

	[RelayCommand]
	public void CycleEnumValue()
	{
		EnumValue = EnumValue switch
		{
			TestingType.Type1 => TestingType.Type2,
			TestingType.Type2 => TestingType.Type3,
			TestingType.Type3 => TestingType.Type1,
			_ => TestingType.Type1
		};
	}

	#endregion
}
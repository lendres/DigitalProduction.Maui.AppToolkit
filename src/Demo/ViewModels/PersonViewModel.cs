using CommunityToolkit.Mvvm.ComponentModel;
using DigitalProduction.Maui.ViewModels;
using Microsoft.Maui;

namespace DigitalProduction.Demo.ViewModels;

[QueryProperty(nameof(Person), "Person")]
public partial class PersonViewModel() : BaseViewModel
{
	#region Properties

	[ObservableProperty]
	public partial Person Person
{ get;
set; }

	#endregion
}
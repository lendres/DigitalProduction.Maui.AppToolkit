using CommunityToolkit.Mvvm.ComponentModel;
using DigitalProduction.Maui.Controls;
using DigitalProduction.Maui.ViewModels;
using System.Collections.ObjectModel;

namespace DigitalProduction.Demo.ViewModels;

public partial class DataGridViewModel : DataGridBaseViewModel<Person>
{
	public DataGridViewModel()
	{
		Items = new ObservableCollection<Person>()
		{
			new Person()
			{
				FirstName   = "Jane",
				LastName    = "Doe",
				Age         = 30
			},
			new Person()
			{ 
				FirstName   = "John",
				LastName    = "Doe",
				Age         = 31
			},
			new Person()
			{ 
				FirstName   = "Jim",
				LastName    = "Doe",
				Age         = 6
			},
			new Person()
			{ 
				FirstName   = "Jessie",
				LastName    = "Doe",
				Age         = 8
			}
		};

		StyleType = LayoutStyle.Compact;
		StyleType = LayoutStyle.Loose;
	}

	[ObservableProperty]
	public partial Style?							Style { get; set; }

	[ObservableProperty]
	public partial LayoutStyle						StyleType { get; set; }

	partial void OnStyleTypeChanged(LayoutStyle value)
	{
		switch (value)
		{
			case LayoutStyle.Loose:
			{
				if (Application.Current!.Resources.TryGetValue("DataGridDefaultLooseStyle", out object? style))
				{
					Style = (Style)style;
				}
				break;
			}
			case LayoutStyle.Compact:
			{
				if (Application.Current!.Resources.TryGetValue("DataGridDefaultCompactStyle", out object? style))
				{
					Style = (Style)style;
				}
				break;
			}
		}
	}
}
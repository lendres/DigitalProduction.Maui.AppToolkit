using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalProduction.Maui.Controls;
using DigitalProduction.Maui.ViewModels;
using System.Collections.ObjectModel;

namespace DigitalProduction.Demo.ViewModels;

public partial class DataGridViewModel : DataGridBaseViewModel<Person>
{
	#region Construction

	public DataGridViewModel()
	{
		Items = new ObservableCollection<Person>();

		StyleType = LayoutStyle.Compact;
		StyleType = LayoutStyle.Loose;
	}

	#endregion

	#region Methods and Commands

	[RelayCommand]
	private void AddPeople()
	{
		Items!.Add(new Person() { FirstName = "Jane", LastName = "Doe", Age = 30 });
		Items.Add(new Person() { FirstName = "John", LastName = "Doe", Age = 31	});
		Items.Add(new Person() { FirstName = "Jim", LastName = "Doe", Age = 6 });
		Items.Add(new Person() { FirstName = "Jessie", LastName = "Doe", Age = 8 });
	}

	[RelayCommand]
	private void Save()
	{
		Modified = false;
	}

	#endregion

	#region Style Testing

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
	
	#endregion
}
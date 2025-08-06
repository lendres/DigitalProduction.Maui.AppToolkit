using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DigitalProduction.Maui.Controls;
using DigitalProduction.Maui.ViewModels;
using DigitalProduction.Projects;
using Microsoft.Maui;
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
	public void AddPeople()
	{
		System.Diagnostics.Debug.Assert(Items != null);
		Items.Add(new Person() { FirstName = "Jane",	LastName = "Doe",	Age = 30 });
		Items.Add(new Person() { FirstName = "John",	LastName = "Doe",	Age = 31 });
		Items.Add(new Person() { FirstName = "Jim",		LastName = "Doe",	Age = 6 });
		Items.Add(new Person() { FirstName = "Jessie",	LastName = "Doe",	Age = 8 });
	}

	[RelayCommand]
	private void Save()
	{
		Modified = false;
	}

	/// <summary>
	/// Searches the bibliography for the specified search string in the author and title fields.
	/// </summary>
	/// <param name="search">Search term.</param>
	/// <returns>True if at least one BibEntry is found, false if no entries are found.</returns>
	public override bool Find(string search)
	{
		if (Items == null || Items.Count == 0)
		{
			return false;
		}
		List<Person> findResults    = [Items[0], Items[2]];
		return SetSearchResults(search, findResults);
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
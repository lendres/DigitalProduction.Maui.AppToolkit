using DigitalProduction.Maui.ViewModels;
using System.Collections.ObjectModel;

namespace DPMauiDemo.ViewModels;

public class DataGridViewModel : DataGridBaseViewModel<Person>
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
	}
}
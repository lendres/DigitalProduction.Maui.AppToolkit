using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace DigitalProduction.Maui.ViewModels;

/// <summary>
/// Base class for viewmodels that display a DataGrid.
/// </summary>
/// <typeparam name="T">A class that is used as a bindable object in the DataGrid.</typeparam>
public partial class DataGridBaseViewModel<T> : BaseViewModel, INotifyPropertyChanged where T : class
{
	#region Fields
	#endregion

	#region Construction

	public DataGridBaseViewModel()
	{
	}

	#endregion

	#region Properties

	[ObservableProperty]
	public partial bool								Modified { get; set; }						= false;

	[ObservableProperty]
	public partial ObservableCollection<T>?			Items { get; set; }

	[ObservableProperty]
	public partial T?								SelectedItem { get; set; }					= null;

	[ObservableProperty]
	public partial T?								ItemToEdit { get; set; }					= null;

	[ObservableProperty]
	public partial bool								IsRefreshing { get; set; }					= false;

	[ObservableProperty]
	public partial bool								HeaderBordersVisible { get; set; }			= false;

	[ObservableProperty]
	public partial Thickness						BorderThickness { get; set; }				= new(0);

	[ObservableProperty]
	public partial SelectionMode					SelectionMode { get; set; }					= SelectionMode.Single;

	public static ImmutableList<SelectionMode>		SelectionModes	{ get => Enum.GetValues<SelectionMode>().Cast<SelectionMode>().ToImmutableList(); }

	#endregion

	#region Commands

	[RelayCommand]
	public virtual void Refresh()
	{
		IsRefreshing = true;
		// Do work here.
		IsRefreshing = false;
	}

	[RelayCommand]
	public virtual void EditComplete()
	{
		ItemToEdit = null;
	}

	[RelayCommand]
	public virtual void Edit(T item)
	{
		ArgumentNullException.ThrowIfNull(item);
		ItemToEdit = item;
	}

	[RelayCommand]
	public virtual void Tapped(object item)
	{
		if (item is T)
		{
			Debug.WriteLine($@"Item tapped: {item}");
		}
	}

	#endregion

	#region Methods

	public virtual void ReplaceSelected(T newItem)
	{
		if (SelectedItem != null && Items != null)
		{
			int position = Items.IndexOf(SelectedItem);
			Delete();
			Insert(newItem, position);
		}
	}

	public virtual void Insert(T item, int position = 0)
	{
		if (Items != null)
		{
			// Cannot insert past last position.
			if (position > Items.Count-1)
			{
				position = Items.Count;
			}

			Items.Insert(position, item);
			Modified = true;
		}
	}
	
	public virtual void Delete()
	{
		if (SelectedItem != null && Items != null)
		{
			Items.Remove(SelectedItem);
			Modified = true;
		}
	}

	public virtual void Sort(IComparer<T> comparer)
	{
		if (Items is not null)
		{
			List<T> ordered = [.. Items.Order(comparer)];
			Items.Clear();

			foreach (T item in ordered)
			{
				Items.Add(item);
			}
		}
	}

	#endregion

} // End class.
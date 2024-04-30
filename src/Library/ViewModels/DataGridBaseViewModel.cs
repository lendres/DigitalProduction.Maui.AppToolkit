using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace DigitalProduction.ViewModels;

/// <summary>
/// Base class for viewmodels that display a DataGrid.
/// </summary>
/// <typeparam name="T">A class that is used as a bindable object in the DataGrid.</typeparam>
public partial class DataGridBaseViewModel<T> : ObservableObject, INotifyPropertyChanged where T : class
{
	#region Fields

	[ObservableProperty]
	private bool									_modified						= false;

	[ObservableProperty]
	private ObservableCollection<T>?				_items;

	[ObservableProperty]
	private T?										_selectedItem					= null;

	[ObservableProperty]
	private T?										_itemToEdit						= null;

	[ObservableProperty]
	private bool									_isRefreshing					= false;

	[ObservableProperty]
	private bool									_headerBordersVisible			= false;

	[ObservableProperty]
	private Thickness								_borderThickness				= new(0);

	[ObservableProperty]
	private SelectionMode							_selectionMode					= SelectionMode.Single;

	public static ImmutableList<SelectionMode>		SelectionModes					=> Enum.GetValues<SelectionMode>().Cast<SelectionMode>().ToImmutableList();

	#endregion

	#region Construction

	public DataGridBaseViewModel()
	{
	}

	#endregion

	[RelayCommand]
	private void Refresh()
	{
		IsRefreshing = true;
		// Do work here.
		IsRefreshing = false;
	}

	[RelayCommand]
	private void EditComplete()
	{
		ItemToEdit = null;
	}

	[RelayCommand]
	private void Edit(T item)
	{
		ArgumentNullException.ThrowIfNull(item);
		ItemToEdit = item;
	}

	[RelayCommand]
	protected virtual void Tapped(object item)
	{
		if (item is T tItem)
		{
			Debug.WriteLine($@"Item tapped: {tItem}");
		}
	}

	public void ReplaceSelected(T newItem)
	{
		if (SelectedItem != null && Items != null)
		{
			T selectedItem = SelectedItem;
			int position = Items.IndexOf(selectedItem);
			Items.Remove(SelectedItem);
			Items.Insert(position, newItem);
			Modified = true;
		}
	}

	public void Insert(T item, int position = 0)
	{
		if (Items != null)
		{
			// Cannot insert past last position.
			if (position > Items.Count-1)
			{
				position = Items.Count-1;
			}

			Items.Insert(position, item);
			Modified = true;
		}
	}

	public void Delete()
	{
		if (SelectedItem != null && Items != null)
		{
			Items.Remove(SelectedItem);
			Modified = true;
		}
	}
}
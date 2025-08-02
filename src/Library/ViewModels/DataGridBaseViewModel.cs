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
public abstract partial class DataGridBaseViewModel<T> : BaseViewModel, INotifyPropertyChanged where T : class
{
	#region Fields

	private string?                     _findString;
	private int                         _findIndex			= 0;
	private List<T>?					_findResults;

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

	public bool										RequireSearchString { get => _findString == null; }

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

	/// <summary>
	/// Searches the bibliography for the specified search string in the author and title fields.
	/// </summary>
	/// <param name="search">Search term.</param>
	/// <returns>True if at least one BibEntry is found, false if no entries are found.</returns>
	public abstract bool Find(string search);

	protected bool SetSearchResults(string search, List<T> findResults)
	{
		// Reset index for new search.	
		_findIndex  = 0;
		_findString = search;

		if (findResults.Count > 0)
		{
			_findString		= search;
			_findResults	= findResults;
			return true;
		}
		else
		{
			_findString		= null;
			_findResults	= null;
			return false;
		}
	}

	private void UpdateFind()
	{
		if (_findString != null)
		{
			Find(_findString);
		}
	}

	/// <summary>
	/// Selects the next found item in the search results.
	/// </summary>
	public void SelectNextFoundItem()
	{
		T searchBibEntry	= _findResults![_findIndex++];
		SelectedItem		= searchBibEntry;
		
		// Reset index if we reach the end of the list.
		if (_findIndex >= _findResults.Count)
		{
			_findIndex = 0;
		}
	}

	public virtual void ReplaceSelected(T newItem, bool select = true)
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

			if (select)
			{
				SelectedItem = item;
			}
			UpdateFind();
		}
	}
	
	public virtual void Delete()
	{
		if (SelectedItem != null && Items != null)
		{
			Items.Remove(SelectedItem);
			SelectedItem	= null;
			Modified		= true;

			if (selectNext)
			{
				// If we are not at the end of the list, select the next item.
				if (currentIndex < Items.Count && currentIndex > -1)
				{
					SelectedItem = Items[currentIndex];
				}
				else if (Items.Count > 0)
				{
					// If we are at the end of the list, select the last item.
					SelectedItem = Items[^1];
				}
			}

			UpdateFind();
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
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
	/// 
	/// Children should override this method to provide search functionality.  After finding entries,
	/// they should call SetSearchResults() to set the results to allow navigation through them.
	/// </summary>
	/// <param name="search">Search term.</param>
	/// <returns>True if at least one item is found, false if no entries are found.</returns>
	public abstract bool Find(string search);

	/// <summary>
	/// Internal method to update the current search results.
	/// </summary>
	private void UpdateFind()
	{
		if (_findString != null)
		{
			Find(_findString);
		}
	}

	/// <summary>
	/// Call this method after performing a search to set the results, which saves them for navigation.
	/// </summary>
	/// <param name="search"></param>
	/// <param name="findResults"></param>
	/// <returns>True if the results contain items, false otherwise.</returns>
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

	/// <summary>
	/// Replaces the currently selected item with a new item.  The new item is inserted at the same position.
	/// </summary>
	/// <param name="newItem">Item to replace the selected item with.</param>
	/// <param name="select">If true, the item will be set as the current (selected) in the DataGridView.</param>
	public virtual void ReplaceSelected(T newItem, bool select = true)
	{
		if (SelectedItem != null && Items != null)
		{
			int position = Items.IndexOf(SelectedItem);
			Delete();
			Insert(newItem, position, select);
		}
	}

	/// <summary>
	/// Inserts an item into the collection at the specified position.
	/// </summary>
	/// <param name="item">Item to insert.</param>
	/// <param name="position">Position to insert the item at.</param>
	/// <param name="select">If true, the item will be set as the current (selected) in the DataGridView.</param>
	public virtual void Insert(T item, int position = 0, bool select = true)
	{
		if (Items != null)
		{
			// Cannot insert past last position.
			if (position > Items.Count-1)
			{
				position = Items.Count;
			}

			Items.Insert(position, item);

			FinalizeInsert(item, select);
		}
	}

	/// <summary>
	/// Completes the insert operation by setting the modified flag, selecting the item, and updating the search results.
	/// Call this method after inserting an item to perform these common tasks.  This is separated out into its own method to allow for reuse 
	/// if a derived class needs to override the Insert() method to perform additional tasks (such as event hookup) or insert in a different position.
	/// For example, if an automatic positioning system is used.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="select"></param>
	protected void FinalizeInsert(T item, bool select = true)
	{
		Modified = true;
		if (select)
		{
			SelectedItem = item;
		}
		UpdateFind();
	}

	/// <summary>
	/// Deletes the currently selected item from the collection.
	/// </summary>
	/// <param name="selectNext">
	/// If true, the next item in the DataGridView will be selected as the current item.  If the deleted item is the last item,
	/// then the previous item (last item in the list) will be selected.
	/// If false, no item will be selected.
	/// </param>
	public virtual void Delete(bool selectNext = true)
	{
		if (SelectedItem != null && Items != null)
		{
			// If we want to select the next item, we need to know the index of the current item.
			int currentIndex = Items.IndexOf(SelectedItem);

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

	/// <summary>
	/// Sorts the items in the collection using the specified comparer.
	/// </summary>
	/// <param name="comparer">Comparer used to sort with.</param>
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
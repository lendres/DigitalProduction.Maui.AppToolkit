using System.Xml.Serialization;

namespace DigitalProduction.Generic;

/// <summary>
/// Stores a matrix (2 dimensional array) of data which can be accessed by an enumeration, but the data does not have
/// to be stored in the matrix in the same order as the items in the enumeration are defined.
/// </summary>
/// <typeparam name="TKey">Enumeration type used as a key to access data.</typeparam>
/// <typeparam name="TData">Type of data to store in the matrix.</typeparam>
public class MappingList<TKey, TData>
{
	#region Fields

	// Total number of possible keys.  Take as the size of the enumeration which defines TKey.
	int										_numberOfKeys;

	List<TKey>								_activeKeys;
	int										_numberOfActiveKeys;

	// Array which maps the enumerations to the position in "_data".  This maps between the enumeration values 
	// and the location in the array "_data" that the data is in.
	private int[]							_map;

	// Main data set.  Data is stored with the keys as rows.  So to access data the key is the first index,
	// the index to that key (which data point in the row) is the second index.  Example layout:
	//
	// Key						Entries
	//					1		2		3		4		5
	// TKey.First		3.1		4.2		5.5		6.1		7.1
	// TKey.Second		98.6	86.3	76.5	92.4	82.3
	// TKey.Third		15351	16523	18352	14366	13546
	private readonly List<TData?>                    _data                   = new();

	#endregion

	#region Construction

	/// <summary>
	/// Parameterless constructor for serialization.
	/// </summary>
	protected MappingList()
	{
		_numberOfKeys       = Reflection.Enumerations.NumberOfDefinedItems<TKey>();
		_activeKeys         = new();
		_numberOfActiveKeys = _activeKeys.Count;
		_map                = new int[_numberOfKeys];
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="activeKeys">List of active keys, in the order that they are contained in the data.</param>
	public MappingList(List<TKey> activeKeys)
	{
		_numberOfKeys		= Reflection.Enumerations.NumberOfDefinedItems<TKey>();
		_activeKeys			= new List<TKey>(activeKeys);
		_numberOfActiveKeys	= _activeKeys.Count;
		_map				= new int[_numberOfKeys];

		// First we need to initialize the entry map to values which are not valid.  That way we can determine
		// if the data is present or not.
		for (int i = 0; i < _numberOfKeys; i++)
		{
			// Set to an invalid value.
			_map[i] = -1;
		}

		// Now we can set the values in the entry map based on the provided active entries.
		for (int i = 0; i < _numberOfActiveKeys; i++)
		{
			// Set a map from the enumeration into the data container.
			_map[System.Convert.ToInt32(_activeKeys[i])] = i;
			_data.Add(default);
		}
	}

	/// <summary>
	/// Copy constructor.
	/// </summary>
	/// <param name="original">Copy source.</param>
	public MappingList(MappingList<TKey, TData> original)
	{
		_numberOfKeys		= original._numberOfKeys;
		_activeKeys			= new List<TKey>(original._activeKeys);
		_numberOfActiveKeys	= original._numberOfActiveKeys;
		_map				= new int[_numberOfKeys];

		// Copy map.
		original._map.CopyTo(_map, 0);

		// Copy data.
		for (int i = 0; i < _numberOfActiveKeys; i++)
		{
			_data.Add(original._data[i]);
		}
	}

	/// <summary>
	/// Subset extractor constructor.
	/// </summary>
	/// <param name="original">Copy source.</param>
	/// <param name="activeKeysToExtract">List of active keys, in the order that they are contained in the data, to be copied from the original.</param>
	public MappingList(MappingList<TKey, TData> original, List<TKey> activeKeysToExtract)
	{
		_numberOfKeys		= original._numberOfKeys;
		_activeKeys			= new List<TKey>(activeKeysToExtract);
		_numberOfActiveKeys	= activeKeysToExtract.Count;
		_map				= new int[_numberOfKeys];

		// Copy data.
		for (int i = 0; i < _numberOfActiveKeys; i++)
		{
			_map[System.Convert.ToInt32(_activeKeys[i])] = i;
			_data.Add(original[_activeKeys[i]]);
		}
	}

	#endregion

	#region Properties

	/// <summary>
	/// Total number of keys available in the enumeration used for TKey.
	/// </summary>
	[XmlAttribute("numberofkeys")]
	public int NumberOfKeys
	{
		get
		{
			return _numberOfKeys;
		}

		set
		{
			_numberOfKeys = value;
		}
	}

	/// <summary>
	/// List of keys which we have data for.
	/// </summary>
	[XmlArray("activekeys"), XmlArrayItem("key")]
	public List<TKey> ActiveKeys
	{
		get
		{
			return _activeKeys;
		}

		set
		{
			_activeKeys = value;
		}
	}

	/// <summary>
	/// Number of the keys that we have data for.
	/// </summary>
	[XmlAttribute("numberofactivekeys")]
	public int NumberOfActiveKeys
	{
		get
		{
			return _numberOfActiveKeys;
		}

		set
		{
			_numberOfActiveKeys = value;
		}
	}

	/// <summary>
	/// Map which specifies where data is in the data array.
	/// </summary>
	[XmlArray("map"), XmlArrayItem("index")]
	public int[] Map
	{
		get
		{
			return _map;
		}

		set
		{
			_map = value;
		}
	}

	/// <summary>
	/// Raw data.
	/// </summary>
	[XmlArray("data"), XmlArrayItem("item")]
	public List<TData?> Data
	{
		get
		{
			return _data;
		}
	}

	/// <summary>
	/// Brackets operator used to access data by it's key type.
	/// </summary>
	/// <param name="key">Which set of data to get.</param>
	[XmlIgnore()]
	public TData? this[TKey key]
	{
		get
		{
			int index = _map[System.Convert.ToInt32(key)];

			if (index < 0)
			{
				throw new Exception("The requested data is not available.");
			}

			return _data[index];
		}

		set
		{
			int index = _map[System.Convert.ToInt32(key)];

			if (index < 0)
			{
				throw new Exception("The requested data is not available.");
			}

			_data[index] = value;
		}
	}

	/// <summary>
	/// Brackets operator used to get data by its index.  Generally this would be used in a loop where are need to go over all
	/// the data without concern for exactly what data is in what position.
	/// </summary>
	/// <param name="index">Index of data to get.</param>
	[XmlIgnore()]
	public TData? this[int index]
	{
		get
		{
			if (index < 0 || index > _numberOfActiveKeys)
			{
				throw new Exception("The requested data is not available.");
			}

			return _data[index];
		}

		set
		{
			if (index < 0 || index > _numberOfActiveKeys)
			{
				throw new Exception("The requested data is not available.");
			}

			_data[index] = value;
		}
	}

	/// <summary>
	/// Length of the data (number of elements for each TKey).
	/// </summary>
	[XmlIgnore()]
	public int NumberOfEntries
	{
		get
		{
			return _data.Count;
		}
	}

	#endregion

	#region Other Methods

	/// <summary>
	/// Specifies if the TKey is active (has data associated with it).
	///
	/// Returns true is data exists for the key time, false otherwise.
	/// </summary>
	/// <param name="key">TKey to check.</param>
	public bool IsActiveKey(TKey key)
	{
		if (_map[System.Convert.ToInt32(key)] < 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	#endregion

	#region Data Access

	/// <summary>
	/// Adds a set of data entries to the back of the data.
	/// </summary>
	/// <param name="entries">Set of data, one entry per each active key type, in the same order as the active key types.</param>
	public void Set(List<TData> entries)
	{
		if (entries.Count != _numberOfActiveKeys)
		{
			throw new Exception("The entries supplied to MappingList are not sized correctly.");
		}

		for (int i = 0; i < _numberOfActiveKeys; i++)
		{
			_data[i] = entries[i];
		}
	}

	#endregion

} // End class.
using System.Collections;

namespace DigitalProduction.XML;

/// <summary>
/// Summary not provided for the class AttributeEnumerator.
/// </summary>
public class AttributeEnumerator : IEnumerator, IEnumerator<Attribute>
{
	#region Fields

	private List<Attribute>	_attributes;

	// Enumerators are positioned before the first element until the first MoveNext() call.
	private int				_position = -1;

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public AttributeEnumerator(List<Attribute> attributes)
	{
		_attributes = attributes;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Get the current entry.
	/// </summary>
	object IEnumerator.Current
	{
		get
		{
			try
			{
				return _attributes[_position];
			}
			catch (IndexOutOfRangeException)
			{
				throw new InvalidOperationException();
			}
		}
	}

	Attribute IEnumerator<Attribute>.Current
	{
		get
		{
			try
			{
				return _attributes[_position];
			}
			catch (IndexOutOfRangeException)
			{
				throw new InvalidOperationException();
			}
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Move to the next entry.
	///
	/// Returns true is there is another entry, false otherwise.
	/// </summary>
	bool IEnumerator.MoveNext()
	{
		_position++;
		return (_position < _attributes.Count);
	}

	/// <summary>
	/// Reset to the beginning of the entries.
	/// </summary>
	void IEnumerator.Reset()
	{
		_position = -1;
	}

	#endregion

	#region IDisposable Members

	void IDisposable.Dispose() {}

	#endregion

} // End class.
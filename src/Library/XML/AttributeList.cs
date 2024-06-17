namespace DigitalProduction.XML;

/// <summary>
/// Summary not provided for the class AttributeList.
/// </summary>
public class AttributeList : IEnumerable<Attribute>
{
	#region Fields

	private readonly List<Attribute>		_attributes		= [];

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public AttributeList()
	{
	}

	#endregion

	#region Properties

	/// <summary>
	/// Number of Attributes.
	/// </summary>
	public int Count { get => _attributes.Count; }

	#endregion

	#region Methods

	/// <summary>
	/// Add an Attribute.
	/// </summary>
	/// <param name="attribute">Attribute to add.</param>
	public void Add(Attribute attribute)
	{
		_attributes.Add(attribute);
	}

	/// <summary>
	/// Get an attribute.
	///
	/// Returns the Attribute at position "number" if it exists, null otherwise.
	/// </summary>
	/// <param name="number">Which Attribute to get.</param>
	public Attribute? GetAttribute(int number)
	{
		if (number < _attributes.Count || number >= 0)
		{
			return _attributes[number];
		}

		return null;
	}

	#endregion

	#region IEnumerable / IEnumerable<Attribute> Members

	/// <summary>
	/// Get an enumerator.
	/// </summary>
	public IEnumerator<Attribute> GetEnumerator()
	{
		return new AttributeEnumerator(_attributes);
	}

	/// <summary>
	/// Get an enumerator.
	/// </summary>
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return new AttributeEnumerator(_attributes);
	}

	#endregion

} // End class.
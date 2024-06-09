using System;

namespace DigitalProduction.XML;

/// <summary>
/// An XML data handler.
/// </summary>
public class XmlHandler
{
	#region Fields

	private string					_elementName		= "";
	private XmlHandlerFunction?		_callback;
	private HandlerType				_type				= HandlerType.None;

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public XmlHandler()
	{
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="type">HandlerType of this handler.</param>
	/// <param name="elementhandler">Function which handles the element if its found.</param>
	public XmlHandler(HandlerType type, XmlHandlerFunction elementhandler)
	{
		_callback	= elementhandler;
		_type		= type;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Name of the element this, Handler handles.
	/// </summary>
	public string ElementName { get => _elementName; set => _elementName = value; }

	/// <summary>
	/// Handler type.
	/// </summary>
	public HandlerType Type { get => _type; set => _type = value; }

	/// <summary>
	/// Callback function.
	/// </summary>
	public XmlHandlerFunction? HandlerFunction { get => _callback; set => _callback = value; }

	#endregion

	#region Functions

	#endregion

	#region IComparable Members

	/// <summary>
	/// Implements the CompareTo method of the IComparable interface.
	///
	/// Returns a 32-bit signed integer that indicates the relative order of the objects being compared.  The return value
	/// has these meanings:
	/// Less than zero: This instance is less than obj.
	/// Zero: This instance is equal to obj.
	/// Greater than zero: This instance is greater than obj.
	/// </summary>
	/// <param name="obj">An object of type XMLHandler.</param>
	public int CompareTo(object obj)
	{
		// Cast the input object and do the comparison.  The actual comparison is done by the CompareTo method
		// of the string.
		// Ensure we have an object of this type.
		if (obj is XmlHandler handler)
		{
			return _elementName.CompareTo(handler._elementName);
		}
		else
		{
			if (obj is string)
			{
				return _elementName.CompareTo((string)obj);
			}
			else
			{
				throw new ArgumentException("Object is not a XMLHandler.");
			}
		}
	}

	/// <summary>
	/// Equivalent function used as a predicate to determine if this FileExtension is equivalent to a second.
	/// </summary>
	/// <param name="obj">XMLHandler to compare to.</param>
	override public bool Equals(object obj)
	{
		if (obj is XmlHandler)
		{
			return _elementName == ((XmlHandler)obj)._elementName;
		}

		return false;
	}

	/// <summary>
	/// Get a hash code.
	/// </summary>
	override public int GetHashCode()
	{
		return _elementName.GetHashCode();
	}

	#endregion

} // End class.
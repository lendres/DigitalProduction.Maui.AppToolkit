namespace DigitalProduction.XML;

/// <summary>
/// A list of handlers.
/// </summary>
public class XmlHandlerList
{
	#region Fields

	private Dictionary<string, XmlHandler>			_handlers;
	private XmlHandler[]							_uniquehandlers;

	#endregion

	#region Construction

	/// <summary>
	/// Constructor.
	/// </summary>
	public XmlHandlerList()
	{
		_handlers		= new Dictionary<string, XmlHandler>();
		_uniquehandlers	= new XmlHandler[(int)HandlerType.Count];
	}

	#endregion

	#region Methods

	/// <summary>
	/// Add an element handler.
	/// </summary>
	/// <param name="elementname">Name of the element to handle.</param>
	/// <param name="elementhandler">Function which handles the element if it is found.</param>
	public void AddHandler(string elementname, XmlHandlerFunction elementhandler)
	{
		XmlHandler handler		= new XmlHandler();
		handler.Type			= HandlerType.Element;
		handler.ElementName		= elementname;
		handler.HandlerFunction	= elementhandler;

		_handlers.Add(handler.ElementName, handler);
	}

	/// <summary>
	/// Add a handler of a specific type.
	/// </summary>
	/// <param name="type">HandlerType added.</param>
	/// <param name="elementhandler">Function which handles the element if its found.</param>
	public void AddHandler(HandlerType type, XmlHandlerFunction elementhandler)
	{
		switch (type)
		{
			case HandlerType.Default:
			case HandlerType.Text:
			{
				_uniquehandlers[(int)type] = new XmlHandler(type, elementhandler);
				break;
			}

			default:
			{
				throw new ArgumentException("The handler type specified cannot be added through this function.\n\nHandler type specified: " + type.ToString());
			}
		}
	}

	/// <summary>
	/// Add a list of handlers to this handler list.
	/// </summary>
	/// <param name="xmlhandlerlist">XMLHandlerList to add handlers from.</param>
	public void AddHandlers(XmlHandlerList xmlhandlerlist)
	{
		foreach (KeyValuePair<string, XmlHandler> handlerpair in xmlhandlerlist._handlers)
		{
			_handlers.Add(handlerpair.Key, handlerpair.Value);
		}
	}

	/// <summary>
	/// See if the element has an associated handler.  If it does call the handler.
	/// </summary>
	/// <param name="elementname">Name of the element to look for.</param>
	/// <param name="xmlprocessor">XML processor that is doing the processing.</param>
	/// <param name="data">Optional data passed to the handler.</param>
	public void ProcessElement(string elementname, XmlTextProcessor xmlprocessor, object data)
	{
		// Try to find the element name.
		if (_handlers.ContainsKey(elementname))
		{
			_handlers[elementname].HandlerFunction(xmlprocessor, data);
			return;
		}

		Process(HandlerType.Default, xmlprocessor, data);
	}

	/// <summary>
	/// See if the element has an associated handler.  If it does call the handler.
	/// </summary>
	/// <param name="handler">The HandlerType to look for.</param>
	/// <param name="xmlprocessor">XML processor that is doing the processing.</param>
	/// <param name="data">Optional data passed to the handler.</param>
	public void Process(HandlerType handler, XmlTextProcessor xmlprocessor, object data)
	{
		if (_uniquehandlers[(int)handler] != null)
		{
			_uniquehandlers[(int)handler].HandlerFunction(xmlprocessor, data);
		}
	}

	#endregion

} // End class.
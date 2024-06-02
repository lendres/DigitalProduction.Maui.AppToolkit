using System.Xml;

namespace DigitalProduction.XML;

/// <summary>
/// Summary description for XMLTextProcessor.
/// </summary>
public class XmlTextProcessor
{
	#region Fields

	/// <summary>Base stream that reads the file.</summary>
	private FileStream					_inputStream;

	/// <summary>XML reader that reads the file.</summary>
	private XmlTextReader				_xmlReader;

	/// <summary>The name of the top element.</summary>
	private string						_topElement;

	/// <summary>Flag to indicate the first call to process.</summary>
	private bool						_firstCall;

	#endregion

	#region Construction

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="file">XML file to process.</param>
	public XmlTextProcessor(string file)
	{
		Open(file);
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="file">XML file to process.</param>
	/// <param name="topElement">Name of the top element in the file.</param>
	public XmlTextProcessor(string file, string topElement)
	{
		Open(file, topElement);
	}

	#endregion

	#region Open and Closing the Input

	/// <summary>
	/// Open the input and read up to the first element.
	/// </summary>
	/// <param name="file">XML file to process.</param>
	private void Open(string file)
	{
		Open(file, "");
	}

	/// <summary>
	/// Open the input and read up to the first element.
	/// </summary>
	/// <param name="file">XML file to process.</param>
	/// <param name="topElement">Name of the top element in the file.</param>
	private void Open(string file, string topElement)
	{
		try
		{
			// File input stream.
			_inputStream = new FileStream(file, FileMode.Open, FileAccess.Read);

			// If the file is empty, we are done with this.
			if (_inputStream.Length == 0)
			{
				_xmlReader = null;
				return;
			}

			// Create the XML reader.
			_xmlReader = new XmlTextReader(_inputStream);
			_xmlReader.WhitespaceHandling = WhitespaceHandling.None;

			// Read the header stuff and get to the first element.
			while (_xmlReader.NodeType != XmlNodeType.Element)
			{
				_xmlReader.Read();
			}
		}
		catch
		{
			Close();
			return;
		}

		// Store the top level node name and reset parameters.
		_topElement	= _xmlReader.LocalName;
		_firstCall	= true;

		// If a top level element was specified we will perform a check to ensure we found what we are looking
		// for, otherwise we will assume we found it or don't care what it is.
		//
		// This is mainly implemented to let calls to the XMLTextProcessor constructor using the old constructor
		// that required the top level element name, a requirement which has since been dropped.  But as long as
		// we have the top element for the old style constructor calls, we might as well perform a check to make
		// sure we are getting what we expect.
		if (topElement != "")
		{
			// Do the check.
			if (_topElement != topElement)
			{
				// Did we open the right file?
				string message	=  "Top level element found in XML file did not match the expected element name.";
				message			+= "\n\nExpected: "	+ topElement;
				message			+= "\nFound: "		+ _topElement;
				throw new Exception(message);
			}
		}
	}

	/// <summary>
	/// Clean up in case of any errors.
	/// </summary>
	public void Close()
	{
		if (_inputStream != null)
		{
			try
			{
				_inputStream.Close();
				_inputStream.Dispose();
			}
			catch
			{
			}
			_inputStream = null;
		}

		if (_xmlReader != null)
		{
			try
			{
				_xmlReader.Close();
			}
			catch
			{
			}
			_xmlReader = null;
		}
	}

	#endregion

	#region Properties

	/// <summary>
	/// The base input stream used.  Read only.
	/// </summary>
	public FileStream FileStream
	{
		get
		{
			return _inputStream;
		}
	}

	/// <summary>
	/// The text reader used.  Read only.
	/// </summary>
	public XmlTextReader XmlTextReader
	{
		get
		{
			return _xmlReader;
		}
	}

	/// <summary>
	/// Is the XML file open for reading?
	/// </summary>
	public bool IsFileOpen
	{
		get
		{
			return _xmlReader != null;
		}
	}

	/// <summary>
	/// Returns the information necessary to display a message to the user so that they can figure out what
	/// went wrong with their input file.  Read only.
	/// </summary>
	public string ErrorInformation
	{
		get
		{
			string message = "\n\nFile: " + _inputStream.Name;
			message += "\n\nLine: " + _xmlReader.LineNumber;
			message += "\n\nPosition: " + _xmlReader.LinePosition;
			return message;
		}
	}

	#endregion

	#region Process

	#region Public Access and Cleaning Up

	/// <summary>
	/// Process the body of the current element.
	/// </summary>
	/// <param name="handlers">
	/// An instance of XMLHandlerList which has the handlers for elements that this element contains.
	/// </param>
	public void Process(XmlHandlerList handlers)
	{
		Process(handlers, null);
	}

	/// <summary>
	/// Process the body of the current element.
	/// </summary>
	/// <param name="handlers">
	/// An instance of XMLHandlerList which has the handlers for elements that this element contains.
	/// </param>
	/// <param name="data">Optional data passed to the handler.</param>
	/// <remarks>
	/// This function is really just a wrapper around the RunProcess which does the real work.  We just use this function
	/// to do the error handling.
	/// </remarks>
	public void Process(XmlHandlerList handlers, object data)
	{
		try
		{
			RunProcess(handlers, data);
		}
		catch (Exception exception)
		{
			// Try to clean up.
			Close();
			throw exception;
		}
	}

	#endregion

	#region Run Process

	/// <summary>
	/// Process the body of the current element.
	/// </summary>
	/// <param name="handlers">
	/// An instance of XMLHandlerList which has the handlers for elements that this element contains.
	/// </param>
	/// <param name="data">Optional data passed to the handler.</param>
	private void RunProcess(XmlHandlerList handlers, object data)
	{
		if (_xmlReader == null)
		{
			return;
		}

		// Don't want to read threw empty elements and into the next element.
		if (_xmlReader.IsEmptyElement)
		{
			return;
		}

		string topElement = _xmlReader.LocalName;

		if (_firstCall)
		{
			topElement = _topElement;
			_firstCall = false;
		}

		_xmlReader.Read();

		while (_xmlReader.LocalName != topElement && !_xmlReader.EOF)
		{
			switch (_xmlReader.NodeType)
			{
				case XmlNodeType.Element:
				{
					// Ensure that there is something to read here.
					handlers.ProcessElement(_xmlReader.LocalName, this, data);
					break;
				}

				case XmlNodeType.Text:
				{
					// Handle the text.
					handlers.Process(HandlerType.Text, this, data);

					// If the handler processed the next we have read into something else (like an element)
					// and we don't want to call read again or we will read over it.
					if (_xmlReader.NodeType != XmlNodeType.Text)
					{
						continue;
					}

					break;
				}

				case XmlNodeType.EndElement:
				{
					while (_xmlReader.NodeType == XmlNodeType.EndElement)
					{
						_xmlReader.Read();
					}
					continue;
				}

				case XmlNodeType.Attribute:
				case XmlNodeType.CDATA:
				case XmlNodeType.Comment:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.DocumentType:
				case XmlNodeType.Entity:
				case XmlNodeType.EndEntity:
				case XmlNodeType.EntityReference:
				case XmlNodeType.None:
				case XmlNodeType.Notation:
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.SignificantWhitespace:
				case XmlNodeType.Whitespace:
				case XmlNodeType.XmlDeclaration:
				default:
				{
					break;
				}

			} // End switch.

			_xmlReader.Read();

		} // End while.
	}

	#endregion

	#endregion

	#region Attribute Reading

	/// <summary>
	/// Read an attribute from the file and convert it to the indicated data type.  Returns the attribute
	/// converted to the indicated data type if possible, otherwise the default value.
	/// </summary>
	/// <param name="name">Name of the attribute to read.</param>
	/// <param name="defaultValue">Value to return if nothing is found or an error occurs.</param>
	public int GetAttribute(string name, int defaultValue)
	{
		string val	= _xmlReader.GetAttribute(name);
		int convval	= defaultValue;
		try
		{
			convval = Convert.ToInt32(val);
		}
		catch
		{
		}

		return convval;
	}

	/// <summary>
	/// Read an attribute from the file and convert it to the indicated data type.  Returns the attribute
	/// converted to the indicated data type if possible, otherwise the default value.
	/// </summary>
	/// <param name="name">Name of the attribute to read.</param>
	/// <param name="defaultValue">Value to return if nothing is found or an error occurs.</param>
	public double GetAttribute(string name, double defaultValue)
	{
		string val		= _xmlReader.GetAttribute(name);
		double convval	= defaultValue;
		try
		{
			convval = Convert.ToDouble(val);
		}
		catch
		{
		}

		return convval;
	}

	/// <summary>
	/// Read an attribute from the file and convert it to the indicated data type.  Returns the attribute
	/// converted to the indicated data type if possible, otherwise the default value.
	/// </summary>
	/// <param name="name">Name of the attribute to read.</param>
	/// <param name="defaultValue">Value to return if nothing is found or an error occurs.</param>
	public bool GetAttribute(string name, bool defaultValue)
	{
		string val		= _xmlReader.GetAttribute(name);
		bool convval	= defaultValue;
		try
		{
			convval = Convert.ToBoolean(val);
		}
		catch
		{
		}

		return convval;
	}

	/// <summary>
	/// Read an attribute from the file and convert it to the indicated data type.  Returns the attribute
	/// converted to the indicated data type if possible, otherwise the default value.
	/// </summary>
	/// <param name="name">Name of the attribute to read.</param>
	/// <param name="defaultValue">Value to return if nothing is found or an error occurs.</param>
	public string GetAttribute(string name, string defaultValue)
	{
		string val = _xmlReader.GetAttribute(name);

		if (val == null)
		{
			return defaultValue;
		}
		else
		{
			return val;
		}
	}

	/// <summary>
	/// Read an attribute from the file and convert it to the indicated data type.  Returns the attribute
	/// converted to the indicated data type if possible, otherwise the default value.
	/// </summary>
	/// <param name="name">Name of the attribute to read.</param>
	/// <param name="defaultValue">Value to return if nothing is found or an error occurs.</param>
	public object GetAttribute(string name, object defaultValue)
	{
		object val = _xmlReader.GetAttribute(name);

		if (val == null)
		{
			return defaultValue;
		}
		else
		{
			return val;
		}
	}

	/// <summary>
	/// Extracts all the attributes as a name, value pair and moves to the next element.
	/// </summary>
	/// <remarks>
	/// This function moves to the next elements so if you want to do additional work with the attributes, do it first.
	/// </remarks>
	public AttributeList GetAttributes()
	{
		AttributeList attributes = new AttributeList();

		if (_xmlReader.HasAttributes)
		{
			_xmlReader.MoveToFirstAttribute();
			attributes.Add(new Attribute(_xmlReader.Name, _xmlReader.Value));

			while (_xmlReader.MoveToNextAttribute())
			{
				attributes.Add(new Attribute(_xmlReader.Name, _xmlReader.Value));
			} // End while attribute.

			// Move the reader back to the beginning of the element so it is back were we started
			// and the main processing loop doesn't have a hissy fit.
			_xmlReader.MoveToElement();

		} // End if attributes.

		return attributes;
	}

	#endregion

	#region Get Element Data

	/// <summary>
	/// Read the element data as a string.
	/// </summary>
	public string GetElementString(string defaultValue)
	{
		string val = _xmlReader.ReadString();

		if (val == null)
		{
			return defaultValue;
		}
		else
		{
			return val;
		}
	}

	/// <summary>
	/// Read the element data as the indicated type.
	/// </summary>
	public int GetElementString(int defaultValue)
	{
		string val	= _xmlReader.ReadString();
		int convval	= defaultValue;
		try
		{
			convval = Convert.ToInt32(val);
		}
		catch
		{
		}

		return convval;
	}

	/// <summary>
	/// Read the element data as the indicated type.
	/// </summary>
	public double GetElementString(double defaultValue)
	{
		string val		= _xmlReader.ReadString();
		double convval	= defaultValue;
		try
		{
			convval = Convert.ToDouble(val);
		}
		catch
		{
		}

		return convval;
	}

	/// <summary>
	/// Read the element data as the indicated type.
	/// </summary>
	public bool GetElementString(bool defaultValue)
	{
		string val		= _xmlReader.ReadString();
		bool convval	= defaultValue;
		try
		{
			convval = Convert.ToBoolean(val);
		}
		catch
		{
		}

		return convval;
	}

	#endregion

} // End class.
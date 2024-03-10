using System.Text;
using System.Xml;

namespace DigitalProduction.XML.Serialization;

/// <summary>
/// Summary not provided for the class SerializationSettings.
/// </summary>
public class SerializationSettings
{
	#region Fields

	/// <summary>Type of the object to serialize.</summary>
	private Type					_serializeType;

	/// <summary>Object to serialize.</summary>
	private object					_serializeObject;

	/// <summary>Name of the output file.</summary>
	private string					_outputFile			= "";
	
	/// <summary>Xml writer settings.</summary>
	private XmlWriterSettings		_xmlSettings		= new();

	#endregion

	#region Construction

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="objectToSerialize">Object to serialize.</param>
	/// <param name="outputFile">Output file.</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public SerializationSettings(object objectToSerialize, string outputFile)
	{
		this.SerializeObject		= objectToSerialize;
		_outputFile					= outputFile;
		CreateXmlWriterSettings();
	}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	/// <summary>
	/// Creates the default XmlWriterSettings.
	/// </summary>
	private void CreateXmlWriterSettings()
	{
		this.XmlSettings.Indent					= true;
		this.XmlSettings.NewLineOnAttributes	= true;
		this.XmlSettings.IndentChars			= "    ";
		this.XmlSettings.Encoding				= Encoding.ASCII;
	}

	#endregion

	#region Properties

	/// <summary>
	/// Type of the object to serialize.
	/// </summary>
	public Type SerializeType
	{
		get
		{
			return _serializeType;
		}
	}

	/// <summary>
	/// Object to serialize.
	/// </summary>
	public object SerializeObject
	{
		get
		{
			return _serializeObject;
		}

		set
		{
			_serializeObject	= value;
			_serializeType		= _serializeObject.GetType();
		}
	}

	/// <summary>
	/// Name of the output file.
	/// </summary>
	public string OutputFile
	{
		get
		{
			return _outputFile;
		}

		set
		{
			_outputFile = value;
		}
	}

	/// <summary>
	/// Xml writer settings.
	/// </summary>
	public XmlWriterSettings XmlSettings
	{
		get
		{
			return _xmlSettings;
		}

		set
		{
			_xmlSettings = value;
		}
	}

	#endregion

} // End class.
using System.Xml;
using System.Xml.Serialization;

namespace DigitalProduction.XML.Serialization;

/// <summary>
/// Summary not provided for the class Serialization.
/// </summary>
public static partial class Serialization
{
	#region Serialize

	/// <summary>
	/// Serialize an object.
	/// </summary>
	/// <param name="settings">SerializationSettings to use for writing.</param>
	public static void SerializeObject(SerializationSettings settings)
	{
		XmlSerializer serializer	= new(settings.SerializeType);
		XmlWriter xmlwriter			= XmlWriter.Create(settings.OutputFile, settings.XmlSettings);

		serializer.Serialize(xmlwriter, settings.SerializeObject);
		xmlwriter.Close();
	}

	/// <summary>
	/// Serialize an object.
	/// </summary>
	/// <param name="objectToSerialize">Object to serialize.</param>
	/// <param name="outputFile">Output file.</param>
	public static void SerializeObject(object objectToSerialize, string outputFile)
	{
		SerializationSettings settings	= new(objectToSerialize, outputFile);
		SerializeObject(settings);
	}

	/// <summary>
	/// Serialize an object using full end element closing tags.
	/// </summary>
	/// <param name="settings">SerializationSettings to use for writing.</param>
	public static void SerializeObjectFullEndElement(SerializationSettings settings)
	{
		XmlSerializer serializer					= new(settings.SerializeType);

		XmlTextWriterFullEndElement textwriter		= new(settings.OutputFile, settings.XmlSettings);
		XmlWriter xmlwriter							= XmlTextWriterFullEndElement.Create(textwriter, settings.XmlSettings);

		serializer.Serialize(xmlwriter, settings.SerializeObject);
		xmlwriter.Close();
	}

	/// <summary>
	/// Serialize an object.
	/// </summary>
	/// <param name="objectToSerialize">Object to serialize.</param>
	/// <param name="outputFile">Output file.</param>
	public static void SerializeObjectFullEndElement(object objectToSerialize, string outputFile)
	{
		SerializationSettings settings	= new(objectToSerialize, outputFile);
		SerializeObjectFullEndElement(settings);
	}

	#endregion

} // End class.
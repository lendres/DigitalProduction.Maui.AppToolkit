using GotDotNet.XInclude;
using System.Xml.Serialization;

namespace DigitalProduction.XML.Serialization;

/// <summary>
/// Summary not provided for the class Serialization.
/// </summary>
public static partial class Serialization
{
	#region Deserialize

	/// <summary>
	/// Deserialize an object from a file.
	/// </summary>
	/// <typeparam name="T">Type of object to deserialize.</typeparam>
	/// <param name="file">File to deserialize from.</param>
	public static T? DeserializeObject<T>(string file) where T : class
	{
		XmlSerializer serializer			= new(typeof(T));

		XIncludingReader xmlincludingreader	= new(file);
		T? deserializedobject				= serializer.Deserialize(xmlincludingreader) as T;
		xmlincludingreader.Close();

		return deserializedobject;
	}

	#endregion

} // End class.
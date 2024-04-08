using GotDotNet.XInclude;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace DigitalProduction.XML;

/// <summary>
/// 
/// </summary>
public static partial class XsltTools
{
	#region Fields

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	static XsltTools()
	{
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Perform the transformation.
	/// </summary>
	/// <param name="inputFile">Input (XML) file.</param>
	/// <param name="xsltFile">Transformation (XSLT) file.</param>
	/// <param name="outputFile">Output file.</param>
	public static void Transform(string inputFile, string xsltFile, string outputFile)
	{
		XIncludingReader xIncludingReader   = new(inputFile);
		XPathDocument xPathDocument         = new(xIncludingReader);

		XslCompiledTransform xslTransform   = new(true);
		xslTransform.Load(xsltFile);

		System.IO.StreamWriter streamWriter = new(outputFile, false, System.Text.Encoding.ASCII);
		xslTransform.Transform(xPathDocument, new(), streamWriter);

		xIncludingReader.Close();
		streamWriter.Close();
	}

	#endregion

} // End class.
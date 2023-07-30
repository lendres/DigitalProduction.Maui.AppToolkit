using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace DigitalProduction.XML.Serialization
{
	/// <summary>
	/// 
	/// </summary>
	public class XmlTextWriterFullEndElement : XmlTextWriter
	{
		#region Fields

		#endregion

		#region Construction

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="textWriter">TextWriter.</param>
		public XmlTextWriterFullEndElement(TextWriter textWriter) :
			base(textWriter)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="filename">File (path) to write to.</param>
		/// <param name="settings">Settings to use for writing.</param>
		public XmlTextWriterFullEndElement(string filename, XmlWriterSettings settings) :
			base(filename, settings.Encoding)
		{
			if (settings.Indent)
			{
				this.Formatting = Formatting.Indented;
			}
			else
			{
				this.Formatting = Formatting.None;
			}
		}

		#endregion

		#region Properties

		#endregion

		#region Methods

		/// <summary>
		/// Override the writing of the end element to use the full end element (&lt;element&gt;&lt;/element&gt;) instead
		/// of the short method (&lt;element/&gt;);
		/// </summary>
		public override void WriteEndElement()
		{
			base.WriteFullEndElement();
		}

		#endregion

	} // End class.
} // End namespace.
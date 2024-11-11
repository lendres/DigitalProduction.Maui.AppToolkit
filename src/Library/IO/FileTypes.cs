namespace DigitalProduction.IO;

public static class FileTypes
{
	/// <summary>CSV (comma separated value) file.</summary>
	public static FilePickerFileType CommaSeparatedValue
	{
		get
		{
			return new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
			{
				{
					DevicePlatform.iOS, new[]
					{
						"public.comma-separated-values-text",
						"public.plain-text",
						"public.text"
					}
				},
				{
					DevicePlatform.macOS, new[]
					{
						"public.comma-separated-values-text",
						"public.plain-text",
						"public.text"
					}
				},
				{
					DevicePlatform.Android, new[]
					{
						"text/csv",
						"text/plain"
					}
				},
				{
					DevicePlatform.WinUI, new[]
					{
						".csv",
						".txt",
						".text"
						//"*/*"
					}
				},
			});
		}
	}

	/// <summary>Word document file types.</summary>
	public static FilePickerFileType WordDoc
	{
		get
		{
			return new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
			{
				{
					DevicePlatform.iOS, new[]
					{
						"com.microsoft.word.doc",
						"org.openxmlformats.wordprocessingml.document"
					}
				},
				{
					DevicePlatform.macOS, new[]
					{
						"com.microsoft.word.doc",
						"org.openxmlformats.wordprocessingml.document"
					}
				},
				{
					DevicePlatform.Android, new[]
					{
						"application/msword",
						"application/vnd.openxmlformats-officedocument.wordprocessingml.document"
					}
				},
				{
					DevicePlatform.WinUI, new[]
					{
						"doc","docx",
					}
				},
			});
		}
	}

	/// <summary>XML file types.</summary>
	public static FilePickerFileType Xml
	{
		get
		{
			return new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
			{
				{
					DevicePlatform.iOS, new[]
					{
						"public.xml",
						"public.plain-text",
						"public.text"
					}
				},
				{
					DevicePlatform.macOS, new[]
					{
						"public.xml",
						"public.plain-text",
						"public.text"
					}
				},
				{
					DevicePlatform.Android, new[]
					{
						"text/xml",
						"text/plain"
					}
				},
				{
					DevicePlatform.WinUI, new[]
					{
						".xml",
						".txt",
						".text"
						//"*/*"
					}
				},
			});
		}
	}

	/// <summary>XSLT file types.</summary>
	public static FilePickerFileType Xslt
	{
		get
		{
			return new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
			{
				{
					DevicePlatform.iOS, new[]
					{
						"public.xslt",
						"public.xsl",
						"public.plain-text",
						"public.text"
					}
				},
				{
					DevicePlatform.macOS, new[]
					{
						"public.xslt",
						"public.xsl",
						"public.plain-text",
						"public.text"
					}
				},
				{
					DevicePlatform.Android, new[]
					{
						"text/xslt",
						"text/xsl",
						"text/plain"
					}
				},
				{
					DevicePlatform.WinUI, new[]
					{
						".xslt",
						".xsl",
						".txt",
						".text"
					}
				},
			});
		}
	}
}
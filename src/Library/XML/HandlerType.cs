namespace DigitalProduction.XML;

/// <summary>
/// Type of handler.
/// </summary>
public enum HandlerType : int
{
	/// <summary>First element in enumeration.  Used in loops to provide access to first element without hard coding element name.</summary>
	Start,

	/// <summary>Handles a specific element.</summary>
	Element		= Start,

	/// <summary>If a specific element handler is not specified for the read element, the default handler will be used.</summary>
	Default,

	/// <summary>Handles reading text between an element's start and end tags.</summary>
	Text,

	/// <summary>Default.</summary>
	None,

	/// <summary>One past the last element in this enumeration list.</summary>
	End,

	/// <summary>The number of enumerations in this enumeration list.</summary>
	Count		= End
}
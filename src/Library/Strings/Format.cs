using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DigitalProduction.Strings;

/// <summary>
/// A class for formatting strings.
/// </summary>
public static class Format
{
	#region Fields

	// To properly work, this list needs to be culture specific as well.
	private static HashSet<string> _lowerCaseWords = new HashSet<string>() { "a", "an", "and", "as", "at", "but", "by", "for", "from", "if", "in", "into", "nor", "of", "off", "on", "or", "out", "per", "so", "till", "the", "to", "up", "with", "via", "yet" };

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	static Format()
	{
	}

	#endregion

	#region Properties

	#endregion

	#region Methods


	/// <summary>
	/// Removes a substring from the beginning of a string.
	/// </summary>
	/// <param name="target">Current string.</param>
	/// <param name="trimString">The string to remove from the current string.</param>
	public static string TrimStart(string target, string trimString)
	{
		if (string.IsNullOrEmpty(trimString)) return target;

		string result = target;
		while (result.StartsWith(trimString))
		{
			result = result.Substring(trimString.Length);
		}

		return result;
	}

	/// <summary>
	/// Removes a substring from the end of a string.
	/// </summary>
	/// <param name="target">Current string.</param>
	/// <param name="trimString">The string to remove from the current string.</param>
	public static string TrimEnd(string target, string trimString)
	{
		if (string.IsNullOrEmpty(trimString)) return target;

		string result = target;
		while (result.EndsWith(trimString))
		{
			result = result.Substring(0, result.Length - trimString.Length);
		}

		return result;
	}


	/// <summary>
	/// Calcuate how similar two strings are using the Levenshtein Distance algorithm.
	/// </summary>
	/// <param name="string1">First string for comparison.</param>
	/// <param name="string2">Second string for comparison.</param>
	public static double Similarity(string string1, string string2)
	{
		int maxLength = Math.Max(string1.Length, string2.Length);

		// If both strings are empty, they are identical.
		if (maxLength == 0)
		{
			return 1.0;
		}

		int distance = LevenshteinDistance(string1, string2);
		return 1.0 - (double)distance / maxLength;
	}

	/// <summary>
	/// Levenshtein Distance algorithm, which calculates the minimum number of single-character edits (insertions, deletions, or substitutions) required to change one string into the other
	/// </summary>
	/// <param name="string1">First string for comparison.</param>
	/// <param name="string2">Second string for comparison.</param>
	private static int LevenshteinDistance(string string1, string string2)
	{
		int[,] distance = new int[string1.Length + 1, string2.Length + 1];

		for (int i = 0; i <= string1.Length; i++)
		{
			distance[i, 0] = i;
		}

		for (int j = 0; j <= string2.Length; j++)
		{
			distance[0, j] = j;
		}

		for (int i = 1; i <= string1.Length; i++)
		{
			for (int j = 1; j <= string2.Length; j++)
			{
				int cost   = (string1[i - 1] == string2[j - 1]) ? 0 : 1;

				int value1 = distance[i-1, j] + 1;
				int value2 = distance[i, j-1] + 1;
				int value3 = distance[i-1, j-1] + cost;

				distance[i, j] = Math.Min(Math.Min(value1, value2), value3);
			}
		}

		return distance[string1.Length, string2.Length];
	}

	/// <summary>
	/// Removes white spaces from the specified string.
	/// </summary>
	/// <param name="input">The input string.</param>
	public static string RemoveWhiteSpace(string input)
	{
		int index		= 0;
		int inputlen	= input.Length;
		char[] newarr	= new char[inputlen];

		for (int i = 0; i < inputlen; i++)
		{
			char tmp = input[i];

			if (!char.IsWhiteSpace(tmp))
			{
				newarr[index] = tmp;
				index++;
			}
		}

		return new string(newarr, 0, index);
	}

	/// <summary>
	/// Determines whether the specified string has any white space characters.
	/// </summary>
	/// <param name="input">The input string.</param>
	public static bool HasWhiteSpace(string input)
	{
		for (int i = 0; i < input.Length; i++)
		{
			if (char.IsWhiteSpace(input[i]))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Returns the DateTime as a formatted string with factions of seconds included.
	/// </summary>
	/// <param name="dateTime">DateTime to format.</param>
	public static string DateTimeWithPreciseSeconds(DateTime dateTime)
	{
		return dateTime.ToString("M/dd/yyyy h:mm:ss.fffffff");
	}

	/// <summary>
	/// Extracts just the major and minor version numbers from a software version number.
	/// </summary>
	/// <param name="softwareVersion">A string that is a software version in the form of X.X.XX.XX</param>
	/// <example>
	/// If the version number is "1.2.3.4" this will return "1.2" as a string.
	/// </example>
	public static string MajorMinorVersionNumber(string softwareVersion)
	{
		string[] splitVersion = softwareVersion.Split('.');
		return splitVersion[0] + "." + splitVersion[1];
	}

	/// <summary>
	/// Single function to set any case.
	/// 
	/// The use case is when you want to be able to have optional string cases provided by a user, et cetera.
	/// </summary>
	/// <param name="input">String to change.</param>
	/// <param name="toCase">Case to change the string to.</param>
	/// <param name="culture">Culture to use when converting.</param>
	public static string ChangeCase(string input, StringCase toCase, string culture = "en-US")
	{
		switch (toCase)
		{
			case StringCase.None:
				return input;

			case StringCase.LowerCase:
				return input.ToLower();

			case StringCase.UpperCase:
				return input.ToUpper();

			case StringCase.TitleCase:
				return ToTitleCase(input, culture);

			default:
				throw new ArgumentException("The StringCase specified is invalid.");
		}
	}

	/// <summary>
	/// Convert a string to title case in a manner that does not capitalize small words (like "a") when it is not necessary.
	/// </summary>
	/// <param name="input">Input string to convert.</param>
	/// <param name="culture">Culture to use when converting.</param>
	public static string ToTitleCase(string input, string culture = "en-US")
	{
		// TextInfo will do most of the work, but there is two catches.  The first is that it will not convert all capital works, so we have have
		// to convert to lower case before using it.  The second is that is capitalizes the first letter of every single word, therefore, we have
		// to check for cases like "the" used in the middle of a sentance that should not be capitalized.
		TextInfo textInfo		= new CultureInfo(culture, false).TextInfo;
		string replacement		= textInfo.ToTitleCase(input.ToLower());

		// Split the string at punctuation and spaces.  The regular expression is used so that the spaces and punctuation are retained
		// and we can add them back as we build the string.  Regular string.Split() does not return the delimeters.
		string delimeters       = @"\p{P}\s";
		string[] splitText      = Regex.Split(replacement, @"(?=[" + delimeters + "])|(?<=[" + delimeters + "])");

		// Skip the first word, that needs to stay capital.
		StringBuilder output	= new StringBuilder(splitText[0]);

		// Skip the first word, that needs to stay capital.
		for (int i = 1; i < splitText.Length; i++)
		{
			if (_lowerCaseWords.Contains(splitText[i].ToLower()))
			{
				output.Append(splitText[i].ToLower());
			}
			else
			{
				output.Append(splitText[i]);
			}
		}

		return output.ToString();
	}

	#endregion

} // End class.
using DigitalProduction.ComponentModel;
using System.ComponentModel;
using System.Reflection;

namespace DigitalProduction.Reflection;

/// <summary>
/// Get attributes.  Provide convenient access for common attribute properties.
///
///	Originally based on code written by skot:
///	http://www.codeproject.com/useritems/EnumDescriptionAttribute.asp
/// </summary>
public static class Attributes
{
	#region Display Name

	/// <summary>
	/// Gets display name of an object.
	/// </summary>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	public static string GetDisplayName(Type type)
	{
		return GetDisplayName(type, string.Empty);
	}

	/// <summary>
	/// Gets display name of an object.
	/// </summary>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	/// <param name="defaultValue">Default value to use if the attribute is not found.</param>
	public static string GetDisplayName(Type type, string defaultValue)
	{
		string name = defaultValue;

		DisplayNameAttribute? attribute = GetAttribute<DisplayNameAttribute>(type);
		if (attribute != null)
		{
			name = attribute.DisplayName;
		}
		return name;
	}

	/// <summary>
	/// Gets display name of an object.
	/// </summary>
	/// <param name="instance">Instance of the object type to retrieve the Attribute from.</param>
	public static string GetDisplayName(object instance)
	{
		return GetDisplayName(instance, string.Empty);
	}

	/// <summary>
	/// Gets display name of an object.
	/// </summary>
	/// <param name="instance">Instance of the object type to retrieve the Attribute from.</param>
	/// <param name="defaultValue">Default value to use if the attribute is not found.</param>
	public static string GetDisplayName(object instance, string defaultValue)
	{
		string name = defaultValue;

		DisplayNameAttribute? attribute = GetAttribute<DisplayNameAttribute>(instance);
		if (attribute != null)
		{
			name = attribute.DisplayName;
		}
		return name;
	}

	#endregion

	#region Description

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	public static string GetDescription(Type type)
	{
		return GetDescription(type, string.Empty);
	}

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	/// <param name="defaultValue">Default value to return if description is not found.</param>
	public static string GetDescription(Type type, string defaultValue)
	{
		string description = defaultValue;

		DescriptionAttribute? attribute = GetAttribute<DescriptionAttribute>(type);
		if (attribute != null)
		{
			description = attribute.Description;
		}
		return description;
	}

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="instance">Value of the enumeration.</param>
	public static string GetDescription(object instance)
	{
		return GetDescription(instance, string.Empty);
	}

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="instance">Value of the enumeration.</param>
	/// <param name="defaultValue">Default value to return if description is not found.</param>
	public static string GetDescription(object instance, string defaultValue)
	{
		string description = defaultValue;

		DescriptionAttribute? attribute = GetAttribute<DescriptionAttribute>(instance);
		if (attribute != null)
		{
			description = attribute.Description;
		}
		return description;
	}

	#endregion

	#region Alternate Name

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	/// <param name="nameType">Which name to return.</param>
	public static string GetAlternateName(Type type, AlternateNameType nameType)
	{
		return GetAlternateName(type, nameType, string.Empty);
	}

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	/// <param name="nameType">Which name to return.</param>
	/// <param name="defaultValue">Default value to return if description is not found.</param>
	public static string GetAlternateName(Type type, AlternateNameType nameType, string defaultValue)
	{
		string alternateName = defaultValue;

		AlternateNamesAttribute? attribute = GetAttribute<AlternateNamesAttribute>(type);
		if (attribute != null)
		{
			alternateName = attribute.GetName(nameType);
		}
		return alternateName;
	}

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="instance">Value of the enumeration.</param>
	/// <param name="nameType">Which name to return.</param>
	public static string GetAlternateName(object instance, AlternateNameType nameType)
	{
		return GetAlternateName(instance, nameType, string.Empty);
	}

	/// <summary>
	/// Gets the description attribute of an enumeration.
	/// </summary>
	/// <param name="instance">Value of the enumeration.</param>
	/// <param name="nameType">Which name to return.</param>
	/// <param name="defaultValue">Default value to return if description is not found.</param>
	public static string GetAlternateName(object instance, AlternateNameType nameType, string defaultValue)
	{
		string alternateName = defaultValue;

		AlternateNamesAttribute? attribute = GetAttribute<AlternateNamesAttribute>(instance);
		if (attribute != null)
		{
			alternateName = attribute.GetName(nameType);
		}
		return alternateName;
	}


	#endregion

	#region Aliases

	/// <summary>
	/// Gets a list of names provided by the Alias attribute.
	/// </summary>
	/// <param name="instance">Instance of the object to retrieve the aliases from.</param>
	public static List<string> GetAliases(object instance)
	{
		return GetAliases(instance.GetType());
	}

	/// <summary>
	/// Gets a list of names provided by the Alias attribute.
	/// </summary>
	/// <param name="type">Type of object to retrieve the aliases from.</param>
	public static List<string> GetAliases(Type type)
	{
		List<string> aliases = new();

		List<AliasAttribute> attributes = GetAllAttributes<AliasAttribute>(type);

		foreach (AliasAttribute attribute in attributes)
		{
			aliases.Add(attribute.Alias);
		}

		return aliases;
	}

	#endregion

	#region Generic

	/// <summary>
	/// Get the first Attribute of type "T" for the Type that the provided object is.
	/// </summary>
	/// <typeparam name="T">Type of attribute to get (not type of the object).</typeparam>
	/// <param name="instance">Instance of the object type to retrieve the Attribute from.</param>
	public static T? GetAttribute<T>(object instance) where T : Attribute
	{
		Type type		= instance.GetType();
		T? attribute	= default;

		if (type.IsEnum)
		{
			// An "instance" of an enum is one of the members of the enumerator list.  They have to be
			// handled differently.
			string instanceString = instance.ToString() ?? "";
			FieldInfo? fieldinfo = instance.GetType().GetField(instanceString);

			if (fieldinfo != null)
			{
				object[] attributes = fieldinfo.GetCustomAttributes(typeof(T), true);
				if (attributes != null && attributes.Length > 0)
				{
					attribute = ((T)attributes[0]);
				}
			}
		}
		else
		{
			attribute = GetAttribute<T>(instance.GetType());
		}

		return attribute;
	}

	/// <summary>
	/// Get the first Attribute of type "T" for the Type that the provided object is.
	/// </summary>
	/// <typeparam name="T">Type of attribute to get (not type of the object).</typeparam>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	public static T? GetAttribute<T>(System.Type type) where T : Attribute
	{
		T? attribute = default;

		Attribute[] attributes = Attribute.GetCustomAttributes(type);

		foreach (Attribute attr in attributes)
		{
			if (attr is T)
			{
				attribute = (T)attr;
			}
		}

		return attribute;
	}

	/// <summary>
	/// Gets a list of Attributes of type "T" for the Type that the provided object is.
	/// </summary>
	/// <typeparam name="T">Type of attribute to get (not type of the object).</typeparam>
	/// <param name="instance">Instance of the object type to retrieve the Attribute from.</param>
	public static List<T> GetAllAttributes<T>(object instance) where T : Attribute
	{
		return GetAllAttributes<T>(instance.GetType());
	}

	/// <summary>
	/// Get a list of Attributes of the specified Type.
	/// </summary>
	/// <typeparam name="T">Type of attribute to get (not type of the object).</typeparam>
	/// <param name="type">Type of the object to retrieve the Attribute from.</param>
	public static List<T> GetAllAttributes<T>(Type type) where T : Attribute
	{
		List<T> attribute = new();

		Attribute[] attributes = Attribute.GetCustomAttributes(type);

		foreach (System.Attribute attr in attributes)
		{
			if (attr is T)
			{
				attribute.Add((T)attr);
			}
		}

		return attribute;
	}

	#endregion

} // End class.
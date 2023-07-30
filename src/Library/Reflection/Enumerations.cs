using System;
using System.Collections.Generic;

namespace DigitalProduction.Reflection
{
	/// <summary>
	/// Enumeration utilities.
	/// </summary>
	public static class Enumerations
	{
		#region Methods

		/// <summary>
		/// Gets the number of items defined within an enumeration type.
		/// </summary>
		/// <typeparam name="T">Enumeration type that items are defined in.</typeparam>
		public static int NumberOfDefinedItems<T>()
		{
			return Enum.GetNames(typeof(T)).Length;
		}

		/// <summary>
		/// Gets all the Description attributes for an enumeration type.
		/// </summary>
		/// <typeparam name="T">Enum type.</typeparam>
		public static List<string> GetAllDescriptionAttributesForType<T>() where T : struct
		{
			int length = NumberOfDefinedItems<T>();

			List<string> descriptions = new List<string>(length);

			Array enumValueArray = Enum.GetValues(typeof(T));

			foreach (T value in enumValueArray)
			{
				descriptions.Add(Attributes.GetDescription(value));
			}

			return descriptions;
		}

		/// <summary>
		/// Gets all the Description attributes for an enumeration type.
		/// </summary>
		/// <typeparam name="T">Enum type.</typeparam>
		public static string[] GetAllDescriptionAttributesForTypeAsArray<T>() where T : struct
		{
			return GetAllDescriptionAttributesForType<T>().ToArray();
		}

		/// <summary>
		/// Searches and returns the instance/enum value with the corresponding Description Attribute.
		/// </summary>
		/// <typeparam name="T">Type of the enum.</typeparam>
		/// <param name="description">Description string to search for.</param>
		public static T GetInstanceFromDescription<T>(string description) where T : struct
		{
			int length = NumberOfDefinedItems<T>();

			Array enumValueArray = Enum.GetValues(typeof(T));

			foreach (T value in enumValueArray)
			{
				if (description == Attributes.GetDescription(value))
				{
					return value;
				}
			}

			return default(T);
		}

		#endregion

	} // End class.
} // End namespace.
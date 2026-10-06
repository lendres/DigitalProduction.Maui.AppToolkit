using System.Globalization;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace DigitalProduction.Maui.UI;

/// <summary>
/// Accesses the existing unpackaged Windows preferences file under a file lock.
/// Reloads before every update so independent processes cannot save stale caches.
/// </summary>
public sealed class FilePreferences : IPreferences
{
	private readonly string _filePath;

	public FilePreferences(string filePath)
	{
		_filePath = Path.GetFullPath(filePath);
	}

	public bool ContainsKey(string key, string? sharedName = null)
	{
		return Read().TryGetValue(sharedName ?? "", out Dictionary<string, string>? values) && values.ContainsKey(key);
	}

	public T Get<T>(string key, T defaultValue, string? sharedName = null)
	{
		if (Read().TryGetValue(sharedName ?? "", out Dictionary<string, string>? values) && values.TryGetValue(key, out string? value))
		{
			if (typeof(T) == typeof(DateTime))
			{
				if (long.TryParse(value, CultureInfo.InvariantCulture, out long binary))
				{
					return (T)(object)DateTime.FromBinary(binary);
				}
				return (T)(object)DateTime.Parse(value, CultureInfo.InvariantCulture);
			}
			if (typeof(T) == typeof(DateTimeOffset))
			{
				return (T)(object)DateTimeOffset.Parse(value, CultureInfo.InvariantCulture);
			}
			return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
		}
		return defaultValue;
	}

	public void Set<T>(string key, T value, string? sharedName = null)
	{
		Type type = typeof(T);
		if (type != typeof(string) && type != typeof(bool) && type != typeof(int) && type != typeof(long) &&
			type != typeof(float) && type != typeof(double) && type != typeof(DateTime) && type != typeof(DateTimeOffset))
		{
			throw new NotSupportedException($"Preferences using {type} are not supported.");
		}
		Update(preferences =>
		{
			string name = sharedName ?? "";
			if (!preferences.TryGetValue(name, out Dictionary<string, string>? values))
			{
				values = new();
				preferences[name] = values;
			}
			if (value is null)
			{
				values.Remove(key);
			}
			else
			{
				string serializedValue;
				if (value is DateTime dateTime)
				{
					serializedValue = dateTime.ToBinary().ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					if (value is DateTimeOffset dateTimeOffset)
					{
						serializedValue = dateTimeOffset.ToString("O", CultureInfo.InvariantCulture);
					}
					else
					{
						serializedValue = Convert.ToString(value, CultureInfo.InvariantCulture)!;
					}
				}
				values[key] = serializedValue;
			}
		});
	}

	public void Remove(string key, string? sharedName = null)
	{
		Update(preferences =>
		{
			if (preferences.TryGetValue(sharedName ?? "", out Dictionary<string, string>? values))
			{
				values.Remove(key);
			}
		});
	}

	public void Clear(string? sharedName = null)
	{
		Update(preferences => preferences.Remove(sharedName ?? ""));
	}

	private Dictionary<string, Dictionary<string, string>> Read()
	{
		try
		{
			using FileStream stream = Open(false);
			return ReadFile(stream);
		}
		catch (FileNotFoundException)
		{
			return new();
		}
		catch (DirectoryNotFoundException)
		{
			return new();
		}
	}

	private void Update(Action<Dictionary<string, Dictionary<string, string>>> update)
	{
		Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
		using FileStream stream = Open(true);
		Dictionary<string, Dictionary<string, string>> preferences = ReadFile(stream);
		update(preferences);
		// Serialize before touching the existing data. Keep the same file locked
		// through reload, modification, write and flush; do not use a second file.
		using MemoryStream buffer = new();
		using (Utf8JsonWriter writer = new(buffer))
		{
			writer.WriteStartObject();
			foreach (KeyValuePair<string, Dictionary<string, string>> container in preferences)
			{
				writer.WriteStartObject(container.Key);
				foreach (KeyValuePair<string, string> entry in container.Value)
				{
					writer.WriteString(entry.Key, entry.Value);
				}
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
		}
		stream.Position = 0;
		buffer.Position = 0;
		buffer.CopyTo(stream);
		stream.SetLength(buffer.Length);
		stream.Flush(true);
	}

	private FileStream Open(bool writable)
	{
		for (int attempt = 0; ; attempt++)
		{
			try
			{
				return new FileStream(_filePath, writable ? FileMode.OpenOrCreate : FileMode.Open,
					writable ? FileAccess.ReadWrite : FileAccess.Read, writable ? FileShare.None : FileShare.Read);
			}
			catch (IOException exception) when (attempt < 20 && (exception.HResult & 0xFFFF) is 32 or 33)
			{
				Thread.Sleep(25);
			}
		}
	}

	private static Dictionary<string, Dictionary<string, string>> ReadFile(Stream stream)
	{
		Dictionary<string, Dictionary<string, string>> preferences = new();
		if (stream.Length == 0)
		{
			return preferences;
		}
		using JsonDocument document = JsonDocument.Parse(stream);
		foreach (JsonProperty container in document.RootElement.EnumerateObject())
		{
			Dictionary<string, string> values = new();
			foreach (JsonProperty entry in container.Value.EnumerateObject())
			{
				values[entry.Name] = entry.Value.GetString()!;
			}
			preferences[container.Name] = values;
		}
		return preferences;
	}
}

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DigitalProduction.Interface;

public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
{
	#region Fields

	public event		PropertyChangedEventHandler?	PropertyChanged;
	private readonly	Dictionary<string, object?>		_properties			= [];

	#endregion

	#region Construction
	#endregion

	#region Methods

	protected bool SetValue(object? value, [CallerMemberName] string propertyName = null!)
	{
		if (_properties.TryGetValue(propertyName!, out var item) && item == value)
		{
			return false;
		}

		_properties[propertyName!] = value;
		OnPropertyChanged(propertyName);

		return true;
	}

	protected T GetValueOrDefault<T>(T defaultValue, [CallerMemberName] string propertyName = null!)
	{
		if (_properties.TryGetValue(propertyName!, out var value))
		{
			if (value != null)
			{
				return (T)value;
			}
		}

		return defaultValue;
	}

	protected T? GetValue<T>([CallerMemberName] string propertyName = null!)
	{
		if (_properties.TryGetValue(propertyName!, out var value))
		{
			return (T?)value;
		}

		return default;
	}

	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	#endregion
}

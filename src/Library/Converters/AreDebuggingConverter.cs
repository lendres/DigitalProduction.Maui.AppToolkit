using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace DigitalProduction.Maui.Converters;

/// <summary>
/// Returns true if debugging, false otherwise.  Useful to have debugging controls, but have them hidden in production code.
/// 
/// E.g.: IsVisible="{Binding ., Converter={StaticResource AreDebuggingConverter}}"
/// </summary>
public class AreDebuggingConverter : IValueConverter
{
	private Assembly Assembly { get => Assembly.GetEntryAssembly()!; }

	public List<DebuggableAttribute> DebuggableAttributes { get => new(Assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>()); }

	public bool IsJustInTimeTrackingEnabled { get => DebuggableAttributes.FirstOrDefault()!.IsJITTrackingEnabled; }

	public string AssemblyName { get => Assembly.GetName().Name!; }

	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return IsJustInTimeTrackingEnabled;
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
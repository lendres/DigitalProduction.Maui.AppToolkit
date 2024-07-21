using System.ComponentModel;

namespace DigitalProduction.Delegates;

#region Delegates

/// <summary>
/// General call back delegate.  Can be used to update the progress bar, close the form, et cetera via a call back function from another thread.
/// </summary>
public delegate void CallBack();

///// <summary>
///// Delegate for a message callback function.
///// </summary>
///// <param name="message">Text to display in the message box.</param>
///// <param name="caption">Text to display in the title bar of the message box.</param>
///// <param name="icon">One of the System.Windows.Forms.MessagBoxIcon that specifies which icon to display in the message box.</param>
//public delegate void DisplayMessageDelegate(string message, string caption, MessageBoxIcon icon);

/// <summary>
/// A generic delegate for events that do not have arguments.
/// </summary>
public delegate void NoArgumentsEventHandler();

/// <summary>
/// A delegate for when errors occur and notification is required.
/// </summary>
public delegate void OnErrorEventHandler(string message);

#endregion
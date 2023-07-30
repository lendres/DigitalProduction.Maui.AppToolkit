using System.ComponentModel;
//using System.Windows.Forms;

namespace DigitalProduction.Delegates
{
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
	/// Delegate template for install functions.
	/// </summary>
	public delegate void InstallEventHandler();

	/// <summary>
	/// A generic delegate for events that do not have arguments.
	/// </summary>
	public delegate void NoArgumentsEventHandler();

	/// <summary>
	/// Update the progress bar via a call back function from another thread.
	/// </summary>
	/// <param name="value">Value of the progress bar as an integer from 0-100.</param>
	public delegate void UpdateProgressCallBack(int value);

	/// <summary>
	/// Update the text (caption) on the form.
	/// </summary>
	/// <param name="caption">Text to display.</param>
	public delegate void UpdateCaptionCallBack(string caption);

	/// <summary>
	/// A generic delegate for validating events.
	/// </summary>
	/// <param name="cancelEventArgs">Cancel event arguments.</param>
	public delegate void ValidateEventHandler(CancelEventArgs cancelEventArgs);

	#endregion

} // End namespace.
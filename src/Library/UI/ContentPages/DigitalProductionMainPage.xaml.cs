﻿namespace DigitalProduction.Controls;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using System.Windows.Input;

#if WINDOWS
using Microsoft.UI.Windowing;
#endif

public partial class DigitalProductionMainPage : ContentPage
{
	#region Fields

	#endregion

	#region Construction

	public DigitalProductionMainPage()
	{
		InitializeComponent();
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Exit command.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="eventArgs">Event arguments.</param>
	protected void OnExit(object sender, EventArgs eventArgs)
	{
		Application.Current.Quit();
	}

	#endregion
}
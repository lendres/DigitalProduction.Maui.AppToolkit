namespace DigitalProduction.Maui.Services;

using Microsoft.Maui.Controls;

public class PageProvider : IPageProvider
{
    public Page? CurrentPage { get; set; }
}
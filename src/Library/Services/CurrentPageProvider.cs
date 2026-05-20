namespace DigitalProduction.Maui.Services;

using Microsoft.Maui.Controls;

public class CurrentPageProvider : IPageProvider
{
    public Page? Page { get => GetCurrentPage(); set { } }

	private static Page? GetCurrentPage()
	{
		Page? page = Application.Current?.Windows[0].Page;

		while (page is Shell shell)
		{
			page = shell.CurrentPage;
		}

		while (page is NavigationPage navigationPage)
		{
			page = navigationPage.CurrentPage;
		}

		while (page is TabbedPage tabbedPage)
		{
			page = tabbedPage.CurrentPage;
		}

		return page;
	}
}
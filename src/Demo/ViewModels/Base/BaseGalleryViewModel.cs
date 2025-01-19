using DigitalProduction.Maui.ViewModels;
using DPMauiDemo.Models;
using System.Diagnostics.CodeAnalysis;

namespace DPMauiDemo.ViewModels;

public abstract class BaseGalleryViewModel : BaseViewModel
{
	protected BaseGalleryViewModel(SectionModel[] items)
	{
		if (DoesItemsArrayContainDuplicates(items, out var duplicatedSectionModels))
		{
			throw new InvalidOperationException($"Duplicate {nameof(SectionModel)}.{nameof(SectionModel.ViewModelType)} found for {duplicatedSectionModels[0].ViewModelType}");
		}

		Items = [.. items.OrderBy(x => x.Title)];
	}

	public IReadOnlyList<SectionModel> Items { get; }

	static bool DoesItemsArrayContainDuplicates(in SectionModel[] items, [NotNullWhen(true)] out IReadOnlyList<SectionModel>? duplicatedSectionModels)
	{
		List<SectionModel> discoveredDuplicatedSectionModels = [];

		var itemsGroupedByViewModelType = items.GroupBy(x => x.ViewModelType);
		foreach (var duplicatedItemsGroups in itemsGroupedByViewModelType.Where(x => x.Count() > 1))
		{
			discoveredDuplicatedSectionModels.AddRange(duplicatedItemsGroups);
		}

		if (discoveredDuplicatedSectionModels.Count!=0)
		{
			duplicatedSectionModels = discoveredDuplicatedSectionModels;
			return true;
		}
		else
		{
			duplicatedSectionModels = null;
			return false;
		}
	}
}
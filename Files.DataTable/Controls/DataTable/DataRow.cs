// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Files.DataTable;

public partial class DataRow : Panel
{
	private DataTable? _parentTable;

	public DataRow()
	{
		Unloaded += (sender, e) => { _parentTable = null; };
	}

	private void GetParentDataTable()
	{
		if (this.FindAscendant<ItemsPresenter>() is ItemsPresenter itemsPresenter &&
			itemsPresenter.Header is DataTable dataTable)
		{
			_parentTable = dataTable;
		}
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		if (_parentTable is null)
			GetParentDataTable();

		double maxHeight = 0;

		if (Children.Count > 0)
		{
			// Handle DataTable Parent
			if (_parentTable != null && _parentTable.Children.Count == Children.Count)
			{
				// Measure all children since we need to determine the row's height at minimum
				for (int i = 0; i < Children.Count; i++)
				{
					if (_parentTable.Children[i] is DataColumn { CurrentWidth.GridUnitType: GridUnitType.Auto } col)
					{
						Children[i].Measure(availableSize);

						var prev = col.MaxChildDesiredWidth;

						col.MaxChildDesiredWidth = Math.Max(col.MaxChildDesiredWidth, Children[i].DesiredSize.Width);

						// If our measure has changed, then we have to invalidate the arrange of the DataTable
						if (col.MaxChildDesiredWidth != prev)
							_parentTable.ArrangeColumnsAndRows();
					}
					else if (_parentTable.Children[i] is DataColumn { CurrentWidth.GridUnitType: GridUnitType.Pixel } pixel)
					{
						Children[i].Measure(new(pixel.DesiredWidth.Value, availableSize.Height));
					}
					else
					{
						Children[i].Measure(availableSize);
					}

					maxHeight = Math.Max(maxHeight, Children[i].DesiredSize.Height);
				}
			}
		}

		// Otherwise, return our parent's size as the desired size.
		return new(_parentTable?.DesiredSize.Width ?? availableSize.Width, maxHeight);
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		if (_parentTable is null)
			return finalSize;

		double x = 0;
		double width = 0;
		int index = 0;

		foreach (FrameworkElement child in Children.Where(e => e.Visibility == Visibility.Visible).Cast<FrameworkElement>())
		{
			if (_parentTable.Children[index] is DataColumn dataColumn)
			{
				width = dataColumn.ActualWidth;

				if (!dataColumn.CanResize)
				{
					// Add resizer width virtually
					width += 8;
				}
				else
				{
					// Avoid using the area of the next column
					child.MaxWidth = width;
					child.Margin = new(0, 0, 12, 0);
				}

				child.Arrange(new(x, 0, width, finalSize.Height));
			}
			else
			{
				child.Arrange(new(x, 0, width, finalSize.Height));
			}

			x += width;
			index++;
		}

		return new Size(x, finalSize.Height);
	}
}

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
		Unloaded += DataRow_Unloaded;
	}

	private void DataRow_Unloaded(object sender, RoutedEventArgs e)
	{
		Unloaded -= DataRow_Unloaded;
		_parentTable?.Rows.Remove(this);
		_parentTable = null;
	}

	private DataTable? InitializeParentHeaderConnection()
	{
		DataTable? panel = null;

		if (this.FindAscendant<ItemsPresenter>() is ItemsPresenter itemsPresenter &&
			itemsPresenter.Header is Grid grid &&
			grid.FindDescendant<DataTable>() is DataTable dataTable)
			panel = dataTable;

		if (panel is DataTable table)
		{
			_parentTable = table;
			_parentTable.Rows.Add(this);
		}

		return panel;
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		_parentTable ??= InitializeParentHeaderConnection();

		double maxHeight = 0;

		if (_parentTable is not null && Children.Count == _parentTable.Children.Count)
		{
			for (int i = 0; i < Children.Count; i++)
			{
				if (_parentTable.Children[i] is DataColumn { CurrentWidth.GridUnitType: GridUnitType.Pixel } pixel)
					Children[i].Measure(new(pixel.DesiredWidth.Value, availableSize.Height));

				maxHeight = Math.Max(maxHeight, Children[i].DesiredSize.Height);
			}
		}

		return new(_parentTable?.DesiredSize.Width ?? availableSize.Width, maxHeight);
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		int index = 0;
		double x = 0;
		double width = 0;

		if (_parentTable != null)
		{
			foreach (FrameworkElement child in Children.Where(static e => e.Visibility == Visibility.Visible).Cast<FrameworkElement>())
			{
				if (_parentTable.Children[index] is DataColumn dataColumn)
				{
					width = dataColumn.ActualWidth;

					if (dataColumn.CanResize)
					{
						// Avoid using the area of the next column
						child.MaxWidth = width - 12;
						child.Margin = new(0, 0, 12, 0);
					}

					child.Arrange(new(x, 0, width, finalSize.Height));
				}

				x += width;
				index++;
			}

			return new Size(x, finalSize.Height);
		}

		return finalSize;
	}
}

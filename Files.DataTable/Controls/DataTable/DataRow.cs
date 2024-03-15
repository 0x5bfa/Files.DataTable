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
				{
					Children[i].Measure(new(pixel.DesiredWidth.Value, availableSize.Height));
				}

				maxHeight = Math.Max(maxHeight, Children[i].DesiredSize.Height);
			}
		}

		// Otherwise, return our parent's size as the desired size.
		return new(_parentTable?.DesiredSize.Width ?? availableSize.Width, maxHeight);
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		int column = 0;
		double x = 0;

		double spacing = 0;

		double width = 0;

		if (_parentTable != null)
		{
			int i = 0;

			foreach (UIElement child in Children.Where(static e => e.Visibility == Visibility.Visible))
			{
				// TODO: Need to check Column visibility here as well...
				if (column < _parentTable.Children.Count)
				{
					// TODO: This is messy...
					width = (_parentTable.Children[column++] as DataColumn)?.ActualWidth ?? 0;
				}

				// Note: For Auto, since we measured our children and bubbled that up to the DataTable layout, then the DataColumn size we grab above should account for the largest of our children.
				if (i == 0)
				{
					child.Arrange(new Rect(x, 0, width, finalSize.Height));
				}
				else
				{
					// If we're in a tree, remove the indentation from the layout of columns beyond the first.
					child.Arrange(new Rect(x, 0, width, finalSize.Height));
				}

				x += width + spacing;
				i++;
			}

			return new Size(x - spacing, finalSize.Height);
		}

		return finalSize;
	}
}

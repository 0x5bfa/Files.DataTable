// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Files.DataTable;

public partial class DataTable : Panel
{
	public HashSet<DataRow> Rows { get; private set; } = new();

	public void ColumnResized()
	{
		InvalidateArrange();

		foreach (var row in Rows)
			row.InvalidateArrange();
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		double maxHeight = 0;

		var elements = Children.Where(static e => e.Visibility == Visibility.Visible && e is DataColumn).Cast<DataColumn>();

		foreach (DataColumn column in elements)
		{
			if (column.CurrentWidth.IsAbsolute)
				column.Measure(new Size(column.CurrentWidth.Value, availableSize.Height));

			maxHeight = Math.Max(maxHeight, column.DesiredSize.Height);
		}

		return new Size(availableSize.Width, maxHeight);
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		var elements = Children.Where(static e => e.Visibility == Visibility.Visible && e is DataColumn).Cast<DataColumn>();

		double x = 0;

		foreach (DataColumn column in elements)
		{
			if (column.CurrentWidth.IsAbsolute)
				column.Arrange(new Rect(x, 0, column.CurrentWidth.Value, finalSize.Height));

			x += column.CurrentWidth.Value + ColumnSpacing;
		}

		return finalSize;
	}
}

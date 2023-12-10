// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Sample_WinUI3_DataTable;

public partial class DataTable : Panel
{
    public bool SizingColumnToFit { get; set; }

    internal bool IsAnyColumnAuto
        => Children.Any(e => e is DataTableColumn { CurrentWidth.GridUnitType: GridUnitType.Auto });

    internal void ArrangeColumnsAndRows()
    {
        InvalidateArrange();

        if (this.FindAscendant<ListView>() is ListView listView)
        {
            foreach (var item in listView.Items)
            {
                var container = listView.ContainerFromItem(item);

                if (container is ListViewItem listViewItem &&
                    listViewItem.ContentTemplateRoot is DataTableRow row)
                    row.InvalidateArrange();
            }
        }
    }

    internal void MeasureAndArrangeColumnsAndRowsToFit()
    {
        SizingColumnToFit = true;

        InvalidateArrange();

        if (this.FindAscendant<ListView>() is ListView listView)
        {
            foreach (var item in listView.Items)
            {
                var container = listView.ContainerFromItem(item);

                if (container is ListViewItem listViewItem &&
                    listViewItem.ContentTemplateRoot is DataTableRow row)
                    row.InvalidateArrange();
            }
        }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        double fixedWidth = 0;
        double autoSized = 0;
        double maxHeight = 0;

        var elements =
            Children
                .Where(x =>
                    x is DataTableColumn dataColumn &&
                    dataColumn.Visibility == Visibility.Visible)
                .Cast<DataTableColumn>();

        foreach (DataTableColumn column in elements)
            fixedWidth += column.DesiredWidth.Value;

        foreach (DataTableColumn column in elements)
        {
            if (column.CurrentWidth.IsAbsolute)
            {
                column.Measure(new Size(column.CurrentWidth.Value, availableSize.Height));
            }
            else
            {
                column.Measure(new Size(availableSize.Width - fixedWidth - autoSized, availableSize.Height));

                autoSized += Math.Max(column.DesiredSize.Width, column.MaxChildDesiredWidth);
            }

            maxHeight = Math.Max(maxHeight, column.DesiredSize.Height);
        }

        return new Size(availableSize.Width, maxHeight);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        double width = 0;
        double x = 0;

        var elements =
            Children
                .Where(x =>
                    x is DataTableColumn dataColumn &&
                    dataColumn.Visibility == Visibility.Visible)
                .Cast<DataTableColumn>();

        foreach (DataTableColumn column in elements)
        {
            if (column.CurrentWidth.IsAbsolute)
            {
                width = column.CurrentWidth.Value;
                column.Arrange(new(x, 0, width, finalSize.Height));
            }
            else
            {
                width = Math.Max(column.DesiredSize.Width, column.MaxChildDesiredWidth);
                column.Arrange(new(x, 0, width, finalSize.Height));
            }

            x += width;
        }

        return finalSize;
    }
}

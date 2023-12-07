// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using System.Collections.Specialized;
using Windows.ApplicationModel.DataTransfer;
using System.Runtime.CompilerServices;
using Windows.Foundation;

namespace Sample_WinUI3_DataTable;

/// <summary>
/// A <see cref="DataTable"/> is a <see cref="Panel"/> which lays out <see cref="DataTableColumn"/>s based on
/// their configured properties (akin to <see cref="ColumnDefinition"/>); similar to a <see cref="Grid"/> with a single row.
/// </summary>
public partial class DataTable : Panel
{
    internal bool IsAnyColumnAuto => Children.Any(static e => e is DataTableColumn { CurrentWidth.GridUnitType: GridUnitType.Auto });

    internal HashSet<DataTableRow> Rows { get; private set; } = new();

    internal void ColumnResized()
    {
        InvalidateArrange();

        foreach (var row in Rows)
            row.InvalidateArrange();
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        double fixedWidth = 0;
        double autoSized = 0;
        double maxHeight = 0;

        var elements = Children.Where(e => e is DataTableColumn dataColumn && dataColumn.Visibility == Visibility.Visible).Cast<DataTableColumn>();

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

                // Keep track of already 'allotted' space, use either the maximum child size (if we know it) or the header content
                autoSized += Math.Max(column.DesiredSize.Width, column.MaxChildDesiredWidth);
            }

            maxHeight = Math.Max(maxHeight, column.DesiredSize.Height);
        }

        return new Size(availableSize.Width, maxHeight);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        double fixedWidth = 0;
        double autoSized = 0;

        var elements = Children.Where(e => e is DataTableColumn dataColumn && dataColumn.Visibility == Visibility.Visible).Cast<DataTableColumn>();

        // We only need to measure elements that are visible
        foreach (DataTableColumn column in elements)
        {
            if (column.CurrentWidth.IsAbsolute)
            {
                fixedWidth += column.CurrentWidth.Value;
            }
            else
            {
                autoSized += Math.Max(column.DesiredSize.Width, column.MaxChildDesiredWidth);
            }
        }

        double width = 0;
        double x = 0;

        foreach (DataTableColumn column in elements)
        {
            if (column.CurrentWidth.IsAbsolute)
            {
                width = column.CurrentWidth.Value;
                column.Arrange(new Rect(x, 0, width, finalSize.Height));
            }
            else
            {
                // TODO: We use the comparison of sizes a lot, should we cache in the DataColumn itself?
                width = Math.Max(column.DesiredSize.Width, column.MaxChildDesiredWidth);
                column.Arrange(new Rect(x, 0, width, finalSize.Height));
            }

            x += width;
        }

        return finalSize;
    }
}

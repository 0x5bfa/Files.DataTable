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
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;

namespace Sample_WinUI3_DataTable;

public partial class DataTableRow : Panel
{
    private DataTable _parentTable;

    public DataTableRow()
    {
        Unloaded += DataRow_Unloaded;
    }

    private void DataRow_Unloaded(object sender, RoutedEventArgs e)
    {
        // Remove our references on unloaded
        _parentTable?.Rows.Remove(this);
        _parentTable = null;
    }

    private void GetParentDataTable()
    {
        if (this.FindAscendant<ItemsPresenter>() is ItemsPresenter itemsPresenter)
        {
            if (itemsPresenter.Header is DataTable dataTable)
            {
                _parentTable = dataTable;
                _parentTable.Rows.Add(this);
            }
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
                    if (_parentTable.Children[i] is DataTableColumn { CurrentWidth.GridUnitType: GridUnitType.Auto } col)
                    {
                        Children[i].Measure(availableSize);

                        var prev = col.MaxChildDesiredWidth;

                        col.MaxChildDesiredWidth = Math.Max(col.MaxChildDesiredWidth, Children[i].DesiredSize.Width);

                        // If our measure has changed, then we have to invalidate the arrange of the DataTable
                        if (col.MaxChildDesiredWidth != prev)
                            _parentTable.ColumnResized();
                    }
                    else if (_parentTable.Children[i] is DataTableColumn { CurrentWidth.GridUnitType: GridUnitType.Pixel } pixel)
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
        int column = 0;
        double x = 0;
        double spacing = _parentTable.ColumnSpacing;
        double width = 0;

        int i = 0;
        foreach (UIElement child in Children.Where(static e => e.Visibility == Visibility.Visible))
        {
            if (column < _parentTable.Children.Count)
            {
                width = (_parentTable.Children[column++] as DataTableColumn)?.ActualWidth ?? 0;
            }

            child.Arrange(new Rect(x, 0, width, finalSize.Height));

            x += width + spacing;
            i++;
        }

        return new Size(x - spacing, finalSize.Height);
    }
}

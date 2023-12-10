// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;

namespace Sample_WinUI3_DataTable;

[TemplatePart(Name = nameof(PART_ColumnSizer), Type = typeof(ContentSizer))]
public partial class DataTableColumn : ButtonBase
{
    private ContentSizer? PART_ColumnSizer;
    private WeakReference<DataTable>? _parent;

    public DataTableColumn()
    {
        DefaultStyleKey = typeof(DataTableColumn);
    }

    protected override void OnApplyTemplate()
    {
        if (PART_ColumnSizer != null)
        {
            PART_ColumnSizer.TargetControl = null;
            PART_ColumnSizer.ManipulationDelta -= PART_ColumnSizer_ManipulationDelta;
            PART_ColumnSizer.ManipulationCompleted -= PART_ColumnSizer_ManipulationCompleted;
        }

        PART_ColumnSizer = GetTemplateChild(nameof(PART_ColumnSizer)) as ContentSizer;

        if (PART_ColumnSizer != null)
        {
            PART_ColumnSizer.TargetControl = this;
            PART_ColumnSizer.ManipulationDelta += PART_ColumnSizer_ManipulationDelta;
            PART_ColumnSizer.ManipulationCompleted += PART_ColumnSizer_ManipulationCompleted;
            PART_ColumnSizer.DoubleTapped += PART_ColumnSizer_DoubleTapped;
        }

        // Get DataTable parent weak reference for when we manipulate columns.
        var parent = this.FindAscendant<DataTable>();
        if (parent != null)
        {
            _parent = new(parent);
        }

        base.OnApplyTemplate();
    }

    private void PART_ColumnSizer_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
        ResizeColumnByUserSizer();
    }

    private void PART_ColumnSizer_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
    {
        ResizeColumnByUserSizer();
    }

    private void PART_ColumnSizer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        SizeColumnToFit();
    }

    private void ResizeColumnByUserSizer()
    {
        // Update our internal representation to be our size now as a fixed value.
        CurrentWidth = new(this.ActualWidth);

        // Notify the rest of the table to update
        if (_parent?.TryGetTarget(out DataTable? parent) == true &&
            parent != null)
        {
            parent.NotifyColumnChangeToRows();
        }
    }

    private void SizeColumnToFit()
    {
        // Update our internal representation to be our size now as a fixed value.
        CurrentWidth = new(this.ActualWidth);

        // Notify the rest of the table to update
        if (_parent?.TryGetTarget(out DataTable? parent) == true &&
            parent != null)
        {
            parent.NotifyColumnSizedToFit();
        }
    }
}

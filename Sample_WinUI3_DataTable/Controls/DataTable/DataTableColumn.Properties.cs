// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Sample_WinUI3_DataTable;

public partial class DataTableColumn : ButtonBase
{
    internal double MaxChildDesiredWidth { get; set; }

    internal GridLength CurrentWidth { get; private set; }

    public bool CanResize
    {
        get => (bool)GetValue(CanResizeProperty);
        set => SetValue(CanResizeProperty, value);
    }

    public static readonly DependencyProperty CanResizeProperty =
        DependencyProperty.Register(
            nameof(CanResize),
            typeof(bool),
            typeof(DataTableColumn),
            new PropertyMetadata(true));

    public GridLength DesiredWidth
    {
        get => (GridLength)GetValue(DesiredWidthProperty);
        set => SetValue(DesiredWidthProperty, value);
    }

    public static readonly DependencyProperty DesiredWidthProperty =
        DependencyProperty.Register(
            nameof(DesiredWidth),
            typeof(GridLength),
            typeof(DataTableColumn),
            new PropertyMetadata(GridLength.Auto, DesiredWidth_PropertyChanged));

    private static void DesiredWidth_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DataTableColumn col)
            col.CurrentWidth = col.DesiredWidth;
    }
}

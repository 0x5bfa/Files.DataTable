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

namespace Sample_WinUI3_DataTable;

public partial class DataTableColumn : ButtonBase
{
    /// <summary>
    /// Gets or sets the width of the largest child contained within the visible <see cref="DataTableRow"/>s of the <see cref="DataTable"/>.
    /// </summary>
    internal double MaxChildDesiredWidth { get; set; }

    /// <summary>
    /// Gets or sets the internal copy of the <see cref="DesiredWidth"/> property to be used in calculations, this gets manipulated in Auto-Size mode.
    /// </summary>
    internal GridLength CurrentWidth { get; private set; }

    /// <summary>
    /// Gets or sets whether the column can be resized by the user.
    /// </summary>
    public bool CanResize
    {
        get { return (bool)GetValue(CanResizeProperty); }
        set { SetValue(CanResizeProperty, value); }
    }

    /// <summary>
    /// Identifies the <see cref="CanResize"/> property.
    /// </summary>
    public static readonly DependencyProperty CanResizeProperty =
        DependencyProperty.Register("CanResize", typeof(bool), typeof(DataTableColumn), new PropertyMetadata(true));

    /// <summary>
    /// Gets or sets the desired width of the column upon initialization. Defaults to a <see cref="GridLength"/> of 1 <see cref="GridUnitType.Star"/>.
    /// </summary>
    public GridLength DesiredWidth
    {
        get { return (GridLength)GetValue(DesiredWidthProperty); }
        set { SetValue(DesiredWidthProperty, value); }
    }

    /// <summary>
    /// Identifies the <see cref="DesiredWidth"/> property.
    /// </summary>
    public static readonly DependencyProperty DesiredWidthProperty =
        DependencyProperty.Register(nameof(DesiredWidth), typeof(GridLength), typeof(DataTableColumn), new PropertyMetadata(GridLength.Auto, DesiredWidth_PropertyChanged));

    private static void DesiredWidth_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // If the developer updates the size of the column, update our internal copy
        if (d is DataTableColumn col)
        {
            col.CurrentWidth = col.DesiredWidth;
        }
    }
}

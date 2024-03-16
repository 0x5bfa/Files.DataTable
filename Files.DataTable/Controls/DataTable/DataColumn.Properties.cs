// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Files.DataTable;

public partial class DataColumn : ButtonBase
{
	internal GridLength CurrentWidth { get; private set; }

	public bool CanResize
	{
		get => (bool)GetValue(CanResizeProperty);
		set => SetValue(CanResizeProperty, value);
	}

	public GridLength DesiredWidth
	{
		get => (GridLength)GetValue(DesiredWidthProperty);
		set => SetValue(DesiredWidthProperty, value);
	}

	public string Header
	{
		get => (string)GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}

	public SortDirection? ColumnSortOption
	{
		get => (SortDirection?)GetValue(ColumnSortOptionProperty);
		set => SetValue(ColumnSortOptionProperty, value);
	}

	public static readonly DependencyProperty CanResizeProperty =
		DependencyProperty.Register(
			nameof(CanResize),
			typeof(bool),
			typeof(DataColumn),
			new PropertyMetadata(true));

	public static readonly DependencyProperty DesiredWidthProperty =
		DependencyProperty.Register(
			nameof(DesiredWidth),
			typeof(GridLength),
			typeof(DataColumn),
			new PropertyMetadata(GridLength.Auto, DesiredWidth_PropertyChanged));

	public static readonly DependencyProperty HeaderProperty =
		DependencyProperty.Register(
			nameof(Header),
			typeof(string),
			typeof(DataColumn),
			new PropertyMetadata(null, (d, e) => ((DataColumn)d).OnHeaderPropertyChanged()));

	public static readonly DependencyProperty ColumnSortOptionProperty =
		DependencyProperty.Register(
			nameof(ColumnSortOption),
			typeof(SortDirection?),
			typeof(DataColumn),
			new PropertyMetadata(null, (d, e) => ((DataColumn)d).OnColumnSortOptionPropertyChanged()));

	private static void DesiredWidth_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DataColumn col)
			col.CurrentWidth = col.DesiredWidth;
	}
}

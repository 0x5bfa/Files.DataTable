// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace Files.DataTable;

public partial class DataTable : Panel
{
	/// <summary>
	/// Gets or sets the amount of space to place between columns within the table.
	/// </summary>
	public double ColumnSpacing
	{
		get => (double)GetValue(ColumnSpacingProperty);
		set => SetValue(ColumnSpacingProperty, value);
	}

	/// <summary>
	/// Gets the <see cref="ColumnSpacing"/> <see cref="DependencyProperty"/>.
	/// </summary>
	public static readonly DependencyProperty ColumnSpacingProperty =
		DependencyProperty.Register(
			nameof(ColumnSpacing),
			typeof(double),
			typeof(DataTable),
			new PropertyMetadata(0d));
}

// Copyright (c) 2023 Files Community
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;

namespace Files.DataTable;

[TemplatePart(Name = nameof(PART_ColumnSizer), Type = typeof(ContentSizer))]
public partial class DataColumn : ButtonBase
{
	private ContentSizer? PART_ColumnSizer;

	private DataTable? _parent;

	public DataColumn()
	{
		DefaultStyleKey = typeof(DataColumn);
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
		}

		_parent = this.FindAscendant<DataTable>();

		base.OnApplyTemplate();
	}

	private void PART_ColumnSizer_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
	{
		ColumnResizedByUserSizer();
	}

	private void PART_ColumnSizer_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
	{
		ColumnResizedByUserSizer();
	}

	private void ColumnResizedByUserSizer()
	{
		CurrentWidth = new(ActualWidth);

		if (_parent != null)
			_parent?.ColumnResized();
	}
}

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;

namespace Files.DataTable
{
	public class ColumnItem
	{
		public string? Name { get; set; }

		public GridLength DesiredWidth { get; set; }

		public int MinWidth { get; set; } = 60;

		public bool CanResize { get; set; } = true;
	}
}

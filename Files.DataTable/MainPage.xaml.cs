using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

namespace Files.DataTable
{
	public sealed partial class MainPage : Page
	{
		public ObservableCollection<StorageItem> Items { get; set; } = new()
		{
			new()
			{
				ThumbnailPath = "ms-appx:///Assets/FolderIcon.png",
				Name = "File name here",
				DateModified = "3/14/2024 12:00 PM",
				Type = "TXT file",
				Size = "23 MB",
			},
			new()
			{
				ThumbnailPath = "ms-appx:///Assets/FolderIcon.png",
				Name = "File name here asdf ds fsdf ",
				DateModified = "3/14/2024 12:00 PM",
				Type = "TXT file",
				Size = "1.4 TB",
			},
			new()
			{
				ThumbnailPath = "ms-appx:///Assets/FolderIcon.png",
				Name = "File name here df ewserfsdfa ds a sd adsf",
				DateModified = "3/14/2024 12:00 PM",
				Type = "TXT file",
				Size = "64 kB",
			},
			new()
			{
				ThumbnailPath = "ms-appx:///Assets/FolderIcon.png",
				Name = "File name here dsf dssdf",
				DateModified = "3/14/2024 12:00 PM",
				Type = "TXT file",
				Size = "1 B",
			},
		};

		public MainPage()
		{
			InitializeComponent();
		}
	}
}

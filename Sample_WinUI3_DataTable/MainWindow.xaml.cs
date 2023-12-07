using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUIEx;

namespace Sample_WinUI3_DataTable
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : WindowEx
    {
		public MainWindow()
		{
			this.InitializeComponent();
		}

		public void NavigateToMainPage()
		{
            AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            MainFrame.Navigate(typeof(MainPage));
		}
	}
}

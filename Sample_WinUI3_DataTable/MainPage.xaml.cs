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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Sample_WinUI3_DataTable
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public ObservableCollection<StorageItem> InventoryItems { get; set; } = new()
        {
            new()
            {
                Icon = "ms-appx:///Assets/FolderIcon.png",
                Name = "getting in blow nothing I.png",
                Tags = "Home, Workspace",
                DateModified = "5/12/2023 3:00 PM",
            },
            new()
            {
                Icon = "ms-appx:///Assets/FolderIcon.png",
                Name = "secret old it you come plans Let's.jpg",
                Tags = "Home, Workspace",
                DateModified = "5/12/2023 3:00 PM",
            },
            new()
            {
                Icon = "ms-appx:///Assets/FolderIcon.png",
                Name = "to your it your.bat",
                Tags = "Home, Workspace",
                DateModified = "5/12/2023 3:00 PM",
            },
            new()
            {
                Icon = "ms-appx:///Assets/FolderIcon.png",
                Name = "Besides, sort and in.pdf",
                Tags = "Study",
                DateModified = "5/12/2023 3:00 PM",
            },
        };

        public MainPage()
        {
            InitializeComponent();
        }
    }
}

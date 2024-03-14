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

namespace Files.DataTable
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<InventoryItem> InventoryItems { get; set; } = new()
        {
            new()
            {
                Id = 1002,
                Name = "Hydra",
                Description = "Multiple Launch Rocket System-2 Hydra",
                Quantity = 1,
            },
            new()
            {
                Id = 3456,
                Name = "MA40 AR",
                Description = "Regular assault rifle - updated version of MA5B or MA37 AR",
                Quantity = 4,
            },
            new()
            {
                Id = 5698,
                Name = "Needler",
                Description = "Alien weapon well-known for its iconic design with pink crystals",
                Quantity = 2,
            },
            new()
            {
                Id = 7043,
                Name = "Ravager",
                Description = "An incendiary plasma launcher",
                Quantity = 1,
            },
            // TODO: Add more items, maybe abstract these to a helper for other samples?
        };

        public MainPage()
        {
            InitializeComponent();
        }
    }
}

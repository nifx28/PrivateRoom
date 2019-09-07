using PrivateRoom.Models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace PrivateRoom.Pages
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public string Header { get; private set; } = "設定";

        public ObservableCollection<SettingsTab> Items { get; private set; } = new ObservableCollection<SettingsTab>();

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Items.Add(new SettingsTab
            {
                TabName = "分頁１",
            });
            Items.Add(new SettingsTab
            {
                TabName = "分頁２",
            });
            Items.Add(new SettingsTab
            {
                TabName = "分頁３",
            });
            Items.Add(new SettingsTab
            {
                TabName = "分頁４",
            });
            Items.Add(new SettingsTab
            {
                TabName = "分頁５",
            });
        }
    }
}

using Windows.UI.Xaml.Controls;

namespace PrivateRoom.Pages
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public string Header { get; private set; } = "設定";

        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}

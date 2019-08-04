using Windows.UI.Xaml.Controls;

namespace PrivateRoom.SubPages
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class HomeActivatePage : Page
    {
        public string Header { get; private set; } = "啟動";

        public HomeActivatePage()
        {
            InitializeComponent();
        }
    }
}

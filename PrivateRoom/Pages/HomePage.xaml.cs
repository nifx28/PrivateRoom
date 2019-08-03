using Windows.UI.Xaml.Controls;

namespace PrivateRoom.Pages
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public string Header { get; private set; } = "首頁";

        public HomePage()
        {
            InitializeComponent();
        }
    }
}

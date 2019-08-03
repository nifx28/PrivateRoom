using Windows.UI.Xaml.Controls;

namespace PrivateRoom.Pages
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class LinksPage : Page
    {
        public string Header { get; private set; } = "連結";

        public LinksPage()
        {
            InitializeComponent();
        }
    }
}

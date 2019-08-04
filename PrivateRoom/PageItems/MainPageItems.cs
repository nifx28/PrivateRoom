using PrivateRoom.Pages;
using PrivateRoom.SubPages;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using PageItems = System.Collections.Generic.List<(
    string Tag,
    Windows.UI.Xaml.Controls.Symbol Icon,
    string Content,
    System.Type Page,
    System.Collections.Generic.List<(string Tag, Windows.UI.Xaml.Controls.Symbol Icon, string Content, System.Type Page)> SubPages)>;

using SubPageItems = System.Collections.Generic.List<(string Tag, Windows.UI.Xaml.Controls.Symbol Icon, string Content, System.Type Page)>;

namespace PrivateRoom
{
    public sealed partial class MainPage : Page
    {
        private readonly PageItems _pages = new PageItems
        {
            ("home",  Symbol.Home, "首頁", typeof(HomePage), new SubPageItems {
                ("home_activate",  Symbol.GoToStart, "啟動", typeof(HomeActivatePage)),
            }),
            ("links", Symbol.Link, "連結", typeof(LinksPage), null),
        };

        private void CreateNavMenus()
        {
            foreach (var page in _pages)
            {
                NavView.MenuItems.Add(new NavigationViewItem
                {
                    Tag = page.Tag,
                    Icon = new SymbolIcon(page.Icon),
                    Content = page.Content,
                    FocusVisualPrimaryThickness = new Thickness(.0),
                });
            }

            NavView.MenuItems.Add(new NavigationViewItemSeparator());
            MenuCount = NavView.MenuItems.Count;
        }

        private void CreateNavSubMenus(string tag = null)
        {
            for (var i = MenuCount; i < NavView.MenuItems.Count; i++)
            {
                NavView.MenuItems.RemoveAt(i);
            }

            if (tag == null)
            {
                return;
            }

            var page = _pages.Where(p => p.Tag == tag).FirstOrDefault();

            if (page.SubPages == null)
            {
                return;
            }

            foreach (var subPage in page.SubPages)
            {
                NavView.MenuItems.Add(new NavigationViewItem
                {
                    Tag = subPage.Tag,
                    Icon = new SymbolIcon(subPage.Icon),
                    Content = subPage.Content,
                    FocusVisualPrimaryThickness = new Thickness(.0),
                });
            }
        }
    }
}

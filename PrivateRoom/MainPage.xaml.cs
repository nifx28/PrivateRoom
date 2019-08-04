using PrivateRoom.Pages;
using System;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace PrivateRoom
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public int MenuCount { get; private set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            CreateNavMenus();

            ContentFrame.Navigated += OnNavigated;
            NavView_Navigate("home", new EntranceNavigationTransitionInfo());

            var goBack = new KeyboardAccelerator { Key = VirtualKey.GoBack };
            goBack.Invoked += OnBackInvoked;
            KeyboardAccelerators.Add(goBack);

            var altLeft = new KeyboardAccelerator { Key = VirtualKey.Left, Modifiers = VirtualKeyModifiers.Menu };
            altLeft.Invoked += OnBackInvoked;
            KeyboardAccelerators.Add(altLeft);
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                NavView_Navigate(args.InvokedItemContainer.Tag as string, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            OnBackRequested();
        }

        private void NavView_Navigate(string tag, NavigationTransitionInfo info)
        {
            Type _page = null;

            if (tag == "settings")
            {
                _page = typeof(SettingsPage);
            }
            else
            {
                var subTag = tag.Split('_');

                if (subTag.Length > 1)
                {
                    var item = _pages.FirstOrDefault(p => p.Tag.Equals(subTag[0]));

                    if (!(item.Page is null) && item.SubPages != null)
                    {
                        var subItem = item.SubPages.FirstOrDefault(p => p.Tag.Equals(tag));
                        _page = subItem.Page;
                    }
                }
                else
                {
                    _page = _pages.FirstOrDefault(p => p.Tag.Equals(tag)).Page;
                }
            }

            if (!(_page is null) && !Type.Equals(ContentFrame.CurrentSourcePageType, _page))
            {
                ContentFrame.Navigate(_page, null, info);
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                CreateNavSubMenus();

                NavView.SelectedItem = NavView.SettingsItem as NavigationViewItem;
                Header.Content = (ContentFrame.Content as SettingsPage)?.Header;
                Header.Margin = new Thickness(.0, .0, 12.0, .0);
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                if (item.Page is null)
                {
                    var _subPages = _pages.Where(p => p.SubPages != null);

                    foreach (var subItem in _subPages)
                    {
                        var subPage = subItem.SubPages.FirstOrDefault(p => p.Page == e.SourcePageType);

                        if (!(subPage.Page is null))
                        {
                            item.Tag = subPage.Tag;
                            break;
                        }
                    }
                }

                CreateNavSubMenus(item.Tag.Split('_')[0]);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                Header.Content = null;
                Header.Margin = new Thickness();
            }
        }

        private void OnBackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            OnBackRequested();
            args.Handled = true;
        }

        private bool OnBackRequested()
        {
            if (!ContentFrame.CanGoBack || (
                NavView.IsPaneOpen && (
                NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                NavView.DisplayMode == NavigationViewDisplayMode.Minimal)))
            {
                return false;
            }

            ContentFrame.GoBack();
            return true;
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("無法載入頁面 " + e.SourcePageType.FullName);
        }
    }
}

using PrivateRoom.Pages;
using System;
using System.Collections.Generic;
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
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("settings", typeof(SettingsPage)),
            ("home",     typeof(HomePage)),
            ("links",    typeof(LinksPage)),
        };

        public MainPage()
        {
            InitializeComponent();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transInfo)
        {
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            Type _page = item.Page;

            if (!(_page is null) && !Type.Equals(ContentFrame.CurrentSourcePageType, _page))
            {
                ContentFrame.Navigate(_page, null, transInfo);
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavView.SelectedItem = NavView.SettingsItem as NavigationViewItem;
                Header.Content = (ContentFrame.Content as SettingsPage)?.Header;
                Header.Margin = new Thickness(.0, .0, 12.0, .0);
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);
                NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First(n => n.Tag.Equals(item.Tag));
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
    }
}

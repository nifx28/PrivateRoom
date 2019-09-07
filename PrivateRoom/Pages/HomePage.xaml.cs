using PrivateRoom.Models;
using System;
using System.Collections.ObjectModel;
using Windows.System;
using Windows.System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PrivateRoom.Pages
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public string Header { get; private set; } = "歡迎使用";

        public ObservableCollection<AppInfo> Items { get; private set; } = new ObservableCollection<AppInfo>();
        public ObservableCollection<ProcessInfo> ProcessItems { get; private set; } = new ObservableCollection<ProcessInfo>();

        public HomePage()
        {
            InitializeComponent();
        }

        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {
            foreach (var info in await AppDiagnosticInfo.RequestInfoAsync())
            {
                /*Items.Add(new AppInfo
                {
                    Id = info.AppInfo.Id,
                    PackageFamilyName = info.AppInfo.PackageFamilyName,
                    Description = info.AppInfo.DisplayInfo.Description,
                    DisplayName = info.AppInfo.DisplayInfo.DisplayName,
                    AppUserModelId = info.AppInfo.AppUserModelId,
                });*/
            }

            foreach (var info in ProcessDiagnosticInfo.GetForProcesses())
            {
                /*ProcessItems.Add(new ProcessInfo
                {
                    ExecutableFileName = info.ExecutableFileName,
                    ProcessId = $"{info.ProcessId}",
                    CpuUsage = $"{info.CpuUsage.GetReport().UserTime:hh':'mm':'ss}",
                    MemoryUsage = $"{info.MemoryUsage.GetReport().WorkingSetSizeInBytes:N0}",
                });*/
            }
        }
    }
}

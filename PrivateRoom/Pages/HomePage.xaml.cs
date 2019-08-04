using PrivateRoom.Models;
using System.Collections.ObjectModel;
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

        public ObservableCollection<ProcessInfo> Items { get; private set; } = new ObservableCollection<ProcessInfo>();

        public HomePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var info in ProcessDiagnosticInfo.GetForProcesses())
            {
                Items.Add(new ProcessInfo
                {
                    ExecutableFileName = info.ExecutableFileName,
                    ProcessId = $"{info.ProcessId}",
                    CpuUsage = $"{info.CpuUsage.GetReport().UserTime:hh':'mm':'ss}",
                    MemoryUsage = $"{info.MemoryUsage.GetReport().WorkingSetSizeInBytes:N0}",
                });
            }
        }
    }
}

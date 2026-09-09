using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;

namespace qgisbox.ViewModels;

public partial class ToolViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task PickFilesAsync()
    {
        var dialog = new FAContentDialog
        {
            Title = "选择文件",
            Content = "文件选择对话框为占位实现,后续将替换为真实的 StorageProvider 调用。",
            CloseButtonText = "确定"
        };
        await dialog.ShowAsync();
    }

    [RelayCommand]
    private async Task RunTaskAsync()
    {
        var dialog = new FAContentDialog
        {
            Title = "批量处理",
            Content = "任务执行入口为占位实现,后续可接入后台任务与进度显示。",
            PrimaryButtonText = "开始",
            CloseButtonText = "取消"
        };
        await dialog.ShowAsync();
    }
}

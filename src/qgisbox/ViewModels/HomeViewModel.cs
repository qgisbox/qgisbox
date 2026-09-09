using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;

namespace qgisbox.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task ShowDemoAsync()
    {
        var dialog = new FAContentDialog
        {
            Title = "Demo",
            Content = "你点击了首页的 Demo 按钮。后续可在此接入真实功能。",
            CloseButtonText = "确定"
        };
        await dialog.ShowAsync();
    }

    [RelayCommand]
    private async Task ShowSecondDemoAsync()
    {
        var dialog = new FAContentDialog
        {
            Title = "Demo 2",
            Content = "这是第二个演示按钮,用于验证命令绑定是否正常工作。",
            PrimaryButtonText = "知道了",
            CloseButtonText = "关闭"
        };
        await dialog.ShowAsync();
    }
}

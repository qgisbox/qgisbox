using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace qgisbox.ViewModels;

public partial class AboutViewModel : ViewModelBase
{
    // TODO: 替换为实际仓库与发行版地址
    public string GitHubUrl { get; } = "https://github.com/qgisbox/qgisbox";
    public string DownloadUrl { get; } = "https://github.com/qgisbox/qgisbox/releases";

    public IReadOnlyList<string> Features { get; } =
    [
        "🪟 基于 Avalonia 12 与 FluentAvaloniaUI,原生 Fluent Design 体验",
        "🧩 模块化工具箱,新增页面即插即用",
        "🎨 支持亮/暗主题与系统强调色",
        "🖥️ Windows / Linux / macOS 跨平台",
        "⚡ 基于 .NET 10,性能出色",
        "🛠️ 更多工具持续集成中",
    ];

    [RelayCommand]
    private async Task OpenGitHubAsync() => await OpenUrlAsync(GitHubUrl);

    [RelayCommand]
    private async Task OpenDownloadAsync() => await OpenUrlAsync(DownloadUrl);

    private static async Task OpenUrlAsync(string url)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } window })
        {
            var topLevel = TopLevel.GetTopLevel(window);
            if (topLevel is not null)
                await topLevel.Launcher.LaunchUriAsync(new System.Uri(url));
        }
    }
}

using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace qgisbox.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public const string AppName = "QGISBox 工具箱";
    public const string Version = "v0.1.0";

    public string Title => $"{AppName} {Version}";

    /// <summary>返回栈:进入新页面前把当前页压栈。</summary>
    private readonly Stack<(string Tag, ViewModelBase Page)> _backStack = new();

    [ObservableProperty]
    private ViewModelBase? _currentPage;

    /// <summary>是否有可回退的页面,控制标题栏返回按钮是否可用。</summary>
    [ObservableProperty]
    private bool _canGoBack;

    /// <summary>当前页面对应的导航 Tag,返回时用于同步导航栏选中项。</summary>
    public string? CurrentTag { get; private set; }

    public MainViewModel()
    {
        Navigate("home");
    }

    /// <summary>
    /// 切换左侧导航对应的页面,并把当前页压入返回栈。新增页面时在此注册即可。
    /// </summary>
    public void Navigate(string tag)
    {
        if (tag == CurrentTag)
            return;

        ViewModelBase? next = tag switch
        {
            "home" => new HomeViewModel(),
            "tool" => new ToolViewModel(),
            "about" => new AboutViewModel(),
            _ => null
        };
        if (next is null)
            return;

        if (CurrentPage is not null && CurrentTag is not null)
            _backStack.Push((CurrentTag, CurrentPage));

        CurrentPage = next;
        CurrentTag = tag;
        CanGoBack = _backStack.Count > 0;
    }

    /// <summary>
    /// 返回上一页。返回恢复后的导航 Tag(无可回退页面时返回 null)。
    /// </summary>
    public string? GoBack()
    {
        if (_backStack.Count == 0)
            return null;

        var (tag, page) = _backStack.Pop();
        CurrentPage = page;
        CurrentTag = tag;
        CanGoBack = _backStack.Count > 0;
        return tag;
    }

    /// <summary>
    /// 切换明/暗主题(左下角月亮图标)。
    /// RequestedThemeVariant 为 Default(跟随系统)时,按窗口实际生效的主题取反,
    /// 保证第一次点击一定有可见变化。
    /// </summary>
    [RelayCommand]
    private void ToggleTheme()
    {
        if (Application.Current is not { } app)
            return;

        var current = app.RequestedThemeVariant;
        if (current == ThemeVariant.Default &&
            app.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } window })
        {
            current = window.ActualThemeVariant;
        }

        app.RequestedThemeVariant = current == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
    }
}

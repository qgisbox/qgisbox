using CommunityToolkit.Mvvm.ComponentModel;

namespace qgisbox.ViewModels;

public partial class ToolViewModel : ViewModelBase
{
    /// <summary>从首页卡片进入时携带的工具名;导航菜单直接进入时为 null。</summary>
    [ObservableProperty]
    private string? _toolName;

    /// <summary>占位页标题:携带工具名时显示工具名,否则显示通用标题。</summary>
    public string DisplayTitle => ToolName ?? "工具";
}

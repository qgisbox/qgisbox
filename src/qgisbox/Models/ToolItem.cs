using FluentAvalonia.UI.Controls;

namespace qgisbox.Models;

/// <summary>首页工具卡片的数据模型。</summary>
/// <param name="Name">工具名称。</param>
/// <param name="Description">一句话描述(卡片上最多显示两行)。</param>
/// <param name="Icon">卡片图标,为空时回退到默认 Code 图标。</param>
/// <param name="Badge">角标(无/HOT/NEW)。</param>
public record ToolItem(string Name, string Description, FASymbol? Icon, ToolBadge Badge = ToolBadge.None)
{
    public FASymbol EffectiveIcon => Icon ?? FASymbol.Code;

    public bool HasBadge => Badge != ToolBadge.None;
    public bool IsHot => Badge == ToolBadge.Hot;
    public bool IsNew => Badge == ToolBadge.New;
    public string BadgeText => Badge switch
    {
        ToolBadge.Hot => "HOT",
        ToolBadge.New => "NEW",
        _ => string.Empty,
    };
}

public enum ToolBadge
{
    None,
    Hot,
    New,
}

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using qgisbox.Models;

namespace qgisbox.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly MainViewModel _main;

    public ObservableCollection<ToolItem> Tools { get; } = new()
    {
        new ToolItem("JSON 格式化", "格式化、压缩、校验 JSON 数据,支持语法高亮", FASymbol.Tag, ToolBadge.Hot),
        new ToolItem("正则表达式测试", "AI 辅助生成与解释,30+ 模板,多语言代码", FASymbol.Highlight),
        new ToolItem("Base64 编解码", "Base64 文本/图片编码与解码", FASymbol.Folder, ToolBadge.New),
        new ToolItem("Hash 生成", "生成 MD5、SHA1、SHA256、SHA512 哈希值", FASymbol.Character),
        new ToolItem("URL 编解码", "URL 编码/解码,支持 encodeURIComponent", FASymbol.Link),
        new ToolItem("进制转换", "二/八/十/十六进制实时互转,同步显示", FASymbol.Calculator),
        new ToolItem("加密解密", "全算法工作台:AES/SM4/ChaCha20/RSA 等", FASymbol.Permissions, ToolBadge.New),
        new ToolItem("Cron 解析", "解析 Cron 表达式,查看下次执行时间", FASymbol.Clock),
        new ToolItem("文本 Diff 对比", "字符级精确高亮差异,并排/内联视图", FASymbol.Switch),
        new ToolItem("二维码生成", "文本/网址一键生成二维码,支持自定义尺寸", FASymbol.Scan),
        new ToolItem("JWT 解码", "解码 JWT Token,查看 Header/Payload", FASymbol.Contact, ToolBadge.New),
        new ToolItem("UUID 生成", "批量生成 v4/v7 UUID,支持自定义数量", FASymbol.Document),
    };

    public HomeViewModel(MainViewModel main)
    {
        _main = main;
    }

    [RelayCommand]
    private void OpenTool(ToolItem? item)
    {
        if (item is not null)
            _main.NavigateToTool(item);
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using qgisbox.ViewModels;

namespace qgisbox;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);

        try
        {
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }
        catch (Exception ex)
        {
            // 视图加载失败时直接显示错误,避免被绑定系统静默吞掉
            return new TextBlock { Text = $"View load failed: {name}\n{ex}", TextWrapping = Avalonia.Media.TextWrapping.Wrap };
        }
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}

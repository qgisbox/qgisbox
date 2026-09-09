using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using qgisbox.Views;

namespace qgisbox;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // MainWindow 构造函数内已创建并设置 MainViewModel,这里不要再 new 第二个实例
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();

        // 默认跟随系统主题;FA 初始化完成后设置,避免被启动流程覆盖
        RequestedThemeVariant = ThemeVariant.Default;
    }
}
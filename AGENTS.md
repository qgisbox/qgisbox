# QGISBox 工具箱 — 项目说明(AI 编程助手必读)

基于 Avalonia 12 + FluentAvaloniaUI 3.1 的跨平台桌面工具框架,仿 qfluentwidgets FluentWindow 风格。目标框架 **net10.0**(FluentAvaloniaUI 3.x 仅支持 net10.0)。

## 常用命令

```powershell
# 构建(在仓库根目录)
dotnet build

# 运行
dotnet run --project src/qgisbox

# 测试
dotnet test

# 发布 Windows 单文件
dotnet publish src/qgisbox -c Release -r win-x64 --self-contained
```

## 目录结构

```
src/qgisbox/          主程序(WinExe)
  App.axaml(.cs)      应用入口;主题初始化为 Default(跟随系统)
  ViewLocator.cs      ViewModel → View 约定式定位(XxxViewModel → XxxView)
  Views/              MainWindow + Home/Tool/About 页面
  ViewModels/         对应 ViewModel;MainViewModel 维护导航返回栈
tests/qgisbox.Tests/  xUnit 单元测试
```

## 新增页面步骤

1. `Views/XxxView.axaml` + `ViewModels/XxxViewModel.cs`(命名必须成对,ViewLocator 按名字解析)
2. `MainViewModel.Navigate` 的 switch 注册:`"xxx" => new XxxViewModel()`
3. `MainWindow.axaml` 的 `FANavigationView.MenuItems` 加项,`Tag` 与注册 tag 一致

## 关键约定与坑

- **MainViewModel 只有一个实例**:在 `MainWindow` 构造函数中创建并赋 DataContext;`App.axaml.cs` 里**不要**再 `new`(曾因此导致事件与绑定操作两个实例,导航失灵)
- 页面导航走 `NavView.ItemInvoked`(Tag 分发;"theme" 是左下角主题切换,不是页面)
- 主题:`App.axaml.cs` 启动时 `RequestedThemeVariant = Default`(跟随系统);`ToggleTheme` 用 `MainWindow.ActualThemeVariant` 取实际生效主题再取反,保证 Default 下首次点击必有变化
- 标题栏:`FAAppWindow` + `TitleBar.ExtendsContentIntoTitleBar = true`;标题按钮由系统 drawn decorations 渲染,**不要**自绘(本机曾误诊过一次)
- 返回按钮:`IsEnabled="{Binding CanGoBack}"`,`GoBack()` 返回恢复页面的 tag,由 `MainWindow` 同步导航栏选中项
- 代码风格见根目录 `.editorconfig`;公共构建属性在 `Directory.Build.props`
- 关于页 GitHub/下载链接在 `AboutViewModel`,发布前确认

## 本机验证环境的注意事项

- 用 PowerShell `CopyFromScreen`/`mouse_event` 做截图点击验证时,脚本必须先 `SetProcessDpiAwareness(2)`,且缩放比例每次现取(详见用户全局 AGENTS.md)
- 自动化点击前注意:首次点击可能被窗口激活吞掉;模态对话框(FAContentDialog)会吞掉背后的点击

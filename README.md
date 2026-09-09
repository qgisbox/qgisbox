# QGISBox 工具箱

一个基于 Avalonia 的跨平台桌面工具框架,界面仿照 qfluentwidgets 的 FluentWindow 风格(自定义标题栏 + 左侧图标导航 + 卡片式内容区),便于快速集成各类桌面小工具。

## 功能特性

- **Fluent Design 风格界面**:基于 FluentAvaloniaUI,支持亮/暗主题与系统强调色
- **自定义标题栏**:返回键 + 应用图标 + 标题,系统自绘最小化/最大化/关闭按钮,支持拖拽与双击最大化
- **图标导航栏**:折叠态仅显示图标,可扩展菜单项;左下角明/暗主题一键切换(默认跟随系统)
- **导航返回栈**:标题栏返回键基于返回栈实现,无可回退页面时自动禁用
- **MVVM 架构**:CommunityToolkit.Mvvm + ViewLocator 约定式导航,新增页面只需加一个 ViewModel/View 并在 `MainViewModel.Navigate` 注册
- **跨平台**:Windows / Linux / macOS

## 技术栈

- .NET 10
- Avalonia UI 12
- FluentAvaloniaUI 3.1
- CommunityToolkit.Mvvm 8

## 运行要求

- .NET 10 SDK

## 构建与运行

```powershell
dotnet build
dotnet run --project src/qgisbox
```

或直接发布:

```powershell
dotnet publish src/qgisbox -c Release -r win-x64 --self-contained
```

## 发布

推送 `v*` 标签即触发 CI 自动构建 Windows / Linux / macOS 单文件并创建 GitHub Release:

```powershell
git tag v0.1.0
git push origin v0.1.0
```

## 项目结构

```
src/qgisbox/
├── App.axaml(.cs)          # 应用入口、主题、全局资源
├── ViewLocator.cs          # ViewModel → View 约定式定位
├── Views/
│   ├── MainWindow.axaml    # 主窗口(标题栏 + 导航)
│   ├── HomeView.axaml      # 首页
│   ├── ToolView.axaml      # 工具页
│   └── AboutView.axaml     # 关于页
└── ViewModels/             # 各页面对应的 ViewModel
```

## 新增页面

1. 在 `Views/` 新建 `XxxView.axaml`,`ViewModels/` 新建 `XxxViewModel`
2. 在 `MainViewModel.Navigate` 的 switch 中注册 `"tag" => new XxxViewModel()`
3. 在 `MainWindow.axaml` 的 `FANavigationView.MenuItems` 中加一项(`Tag` 与注册 tag 一致)

## 许可证

[MIT](LICENSE)

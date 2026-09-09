using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
using qgisbox.ViewModels;

namespace qgisbox.Views;

public partial class MainWindow : FAAppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        // 启用自定义标题栏(仅 Windows 生效)
        TitleBar.ExtendsContentIntoTitleBar = true;

        // 唯一的 MainViewModel 实例:事件处理器和 DataContext 共用
        var vm = new MainViewModel();
        DataContext = vm;

        NavView.ItemInvoked += (_, e) =>
        {
            var item = e.InvokedItemContainer as FANavigationViewItem
                       ?? e.InvokedItem as FANavigationViewItem;
            if (item?.Tag is not string tag)
                return;

            if (tag == "theme")
                vm.ToggleThemeCommand.Execute(null);
            else
                vm.Navigate(tag);
        };

        // 启动时把导航栏选中项同步到初始页面(首页)
        if (vm.CurrentTag is { } initialTag)
            SelectNavItem(initialTag);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not MainViewModel vm)
            return;

        // 返回上一页,并把导航栏选中项同步回目标页面
        if (vm.GoBack() is { } tag)
            SelectNavItem(tag);
    }

    private void SelectNavItem(string tag)
    {
        foreach (var item in NavView.MenuItems.OfType<FANavigationViewItem>())
        {
            if (item.Tag as string == tag)
            {
                NavView.SelectedItem = item;
                return;
            }
        }
    }
}

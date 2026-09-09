using qgisbox.ViewModels;
using Xunit;

namespace qgisbox.Tests;

/// <summary>
/// MainViewModel 导航栈(返回按钮)的单元测试。
/// </summary>
public class MainViewModelNavigationTests
{
    [Fact]
    public void Ctor_DefaultsToHome_AndCannotGoBack()
    {
        var vm = new MainViewModel();

        Assert.IsType<HomeViewModel>(vm.CurrentPage);
        Assert.Equal("home", vm.CurrentTag);
        Assert.False(vm.CanGoBack);
    }

    [Fact]
    public void Navigate_ToTool_PushesHome_AndCanGoBack()
    {
        var vm = new MainViewModel();

        vm.Navigate("tool");

        Assert.IsType<ToolViewModel>(vm.CurrentPage);
        Assert.Equal("tool", vm.CurrentTag);
        Assert.True(vm.CanGoBack);
    }

    [Fact]
    public void GoBack_ReturnsPreviousPage_Tag_AndUpdatesState()
    {
        var vm = new MainViewModel();
        vm.Navigate("about");

        var tag = vm.GoBack();

        Assert.Equal("home", tag);
        Assert.IsType<HomeViewModel>(vm.CurrentPage);
        Assert.Equal("home", vm.CurrentTag);
        Assert.False(vm.CanGoBack);
    }

    [Fact]
    public void GoBack_MultiplePages_WalksStackInOrder()
    {
        var vm = new MainViewModel();
        vm.Navigate("tool");
        vm.Navigate("about");

        Assert.Equal("tool", vm.GoBack());
        Assert.Equal("home", vm.GoBack());
        Assert.False(vm.CanGoBack);
    }

    [Fact]
    public void GoBack_OnEmptyStack_ReturnsNull()
    {
        var vm = new MainViewModel();

        Assert.Null(vm.GoBack());
        Assert.False(vm.CanGoBack);
    }

    [Fact]
    public void Navigate_SameTag_IsNoOp()
    {
        var vm = new MainViewModel();
        var page = vm.CurrentPage;

        vm.Navigate("home");

        Assert.Same(page, vm.CurrentPage);
        Assert.False(vm.CanGoBack);
    }

    [Fact]
    public void Navigate_UnknownTag_IsNoOp()
    {
        var vm = new MainViewModel();
        var page = vm.CurrentPage;

        vm.Navigate("no-such-page");

        Assert.Same(page, vm.CurrentPage);
        Assert.Equal("home", vm.CurrentTag);
        Assert.False(vm.CanGoBack);
    }
}

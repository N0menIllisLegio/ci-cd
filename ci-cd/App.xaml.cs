using System.Windows;
using ci_cd.ViewModels;
using ci_cd.Views;
using Prism.Ioc;
using Prism.Unity;

namespace ci_cd
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : PrismApplication
  {
    protected override Window CreateShell()
    {
      return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
      containerRegistry.RegisterForNavigation<WishList, WishListViewModel>();
    }
  }
}

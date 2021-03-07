using System.Windows;
using AutoMapper;
using ci_cd.Interfaces.Repositories;
using ci_cd.Interfaces.Services;
using ci_cd.Repositories;
using ci_cd.Services;
using ci_cd.Utils;
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
      containerRegistry.Register<ISteamRepository, SteamRepository>();
      containerRegistry.Register<ISteamService, SteamService>();

      containerRegistry.RegisterInstance(typeof(IMapper),
        new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper());

      containerRegistry.RegisterDialog<ErrorDialog, ErrorDialogViewModel>();

      containerRegistry.RegisterForNavigation<WishList, WishListViewModel>();
    }
  }
}

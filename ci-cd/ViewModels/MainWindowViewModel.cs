using ci_cd.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ci_cd.ViewModels
{
  class MainWindowViewModel : BindableBase
  {
    private readonly IRegionManager _regionManager;

    public MainWindowViewModel(IRegionManager regionManager)
    {
      _regionManager = regionManager;
      _regionManager.RegisterViewWithRegion("MainRegion", typeof(WishList));
    }

    public string Title { get; set; } = "Steam Whishlist";
  }
}

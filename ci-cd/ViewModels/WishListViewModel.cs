using System.Collections.ObjectModel;
using ci_cd.Interfaces.Services;
using ci_cd.Models;
using Prism.Mvvm;

namespace ci_cd.ViewModels
{
  public class WishListViewModel : BindableBase
  {
    private readonly ISteamService _steamService;
    private ObservableCollection<WishlistGameModel> _games;

    public WishListViewModel(ISteamService steamService)
    {
      _steamService = steamService;

      _steamService.GetGameModelsAsync("")
        .ContinueWith(async (result) => Games = new ObservableCollection<WishlistGameModel>(await result));
    }

    public ObservableCollection<WishlistGameModel> Games
    {
      get => _games;
      set => SetProperty(ref _games, value);
    }
  }
}

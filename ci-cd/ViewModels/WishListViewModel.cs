using System.Collections.ObjectModel;
using System.Windows.Controls;
using ci_cd.Interfaces.Services;
using ci_cd.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace ci_cd.ViewModels
{
  public class WishListViewModel : BindableBase
  {
    private readonly ISteamService _steamService;
    private ObservableCollection<WishlistGameModel> _games;
    private bool _buttonLoadingIndicatorVisible;
    private DelegateCommand _loadWishlistCommand;
    private string _steamID;

    public WishListViewModel(ISteamService steamService)
    {
      _steamService = steamService;
      SteamID = "76561198092532909";
    }

    public ObservableCollection<WishlistGameModel> Games
    {
      get => _games;
      set => SetProperty(ref _games, value);
    }

    public bool ButtonLoadingIndicatorVisible
    {
      get => _buttonLoadingIndicatorVisible;
      set => SetProperty(ref _buttonLoadingIndicatorVisible, value);
    }

    public DelegateCommand LoadWishlistCommand =>
        _loadWishlistCommand ?? (_loadWishlistCommand = new DelegateCommand(ExecuteLoadWishlistCommand));

    public string SteamID
    {
      get => _steamID;
      set => SetProperty(ref _steamID, value);
    }

    private async void ExecuteLoadWishlistCommand()
    {
      ButtonLoadingIndicatorVisible = true;

      var gamesList = await _steamService.GetGameModelsAsync(SteamID);

      if (gamesList is not null)
      {
        Games = new ObservableCollection<WishlistGameModel>(gamesList);
      }

      ButtonLoadingIndicatorVisible = false;
    }
  }
}

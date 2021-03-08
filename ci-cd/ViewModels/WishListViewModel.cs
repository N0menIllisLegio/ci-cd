using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ci_cd.Interfaces.Services;
using ci_cd.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ci_cd.ViewModels
{
  public class WishListViewModel : BindableBase
  {
    private readonly ISteamService _steamService;
    private readonly IDialogService _dialogService;
    private ObservableCollection<WishlistGameModel> _games;
    private bool _buttonLoadingIndicatorVisible;
    private DelegateCommand _loadWishlistCommand;
    private string _steamID;

    public WishListViewModel(ISteamService steamService, IDialogService dialogService)
    {
      _steamService = steamService;
      _dialogService = dialogService;
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

      try
      {
        var gamesList = await _steamService.GetGameModelsAsync(SteamID);
        Games = new ObservableCollection<WishlistGameModel>(gamesList);
      }
      catch(Exception ex)
      {
        _dialogService.ShowDialog("ErrorDialog", new DialogParameters($"message={ex.Message}"), _ => { });
      }

      ButtonLoadingIndicatorVisible = false;
    }
  }
}

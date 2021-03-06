using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ci_cd.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace ci_cd.ViewModels
{
  public class WishListViewModel : BindableBase
  {
    private ObservableCollection<GameModel> _games;

    public WishListViewModel()
    {
      Games = new ObservableCollection<GameModel>
      {
        new GameModel {Title = "XCOM", Price = 3.99m},
        new GameModel {Title = "King's Bounty: The Legend", Price = 3.99m},
      };
    }

    public ObservableCollection<GameModel> Games
    {
      get => _games;
      set => SetProperty(ref _games, value);
    }
  }
}

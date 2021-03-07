using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ci_cd.DTOs;
using ci_cd.Interfaces.Repositories;
using ci_cd.Interfaces.Services;
using ci_cd.Models;
using Newtonsoft.Json;
using Prism.Services.Dialogs;

namespace ci_cd.Services
{
  internal class SteamService : ISteamService
  {
    private readonly ISteamRepository _steamRepository;
    private readonly IMapper _mapper;
    private readonly IDialogService _dialogService;

    public SteamService(ISteamRepository steamRepository, IMapper mapper, IDialogService dialogService)
    {
      _steamRepository = steamRepository;
      _mapper = mapper;
      _dialogService = dialogService;
    }

    public async Task<List<WishlistGameModel>> GetGameModelsAsync(string steamID)
    {
      string receivedData = String.Empty;

      try
      {
        receivedData = await _steamRepository.GetUserWishlistData(steamID);
      }
      catch (Exception ex)
      {
        _dialogService.ShowDialog("ErrorDialog", new DialogParameters($"message={ex.Message}"), _ => { });

        return null;
      }

      var jsonSerializerSettings = new JsonSerializerSettings();
      jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

      Dictionary<string, WishlistGameDto> wishlistDictionary;

      try
      {
        wishlistDictionary = JsonConvert.DeserializeObject<Dictionary<string, WishlistGameDto>>(receivedData, jsonSerializerSettings);
      }
      catch
      {
        _dialogService.ShowDialog("ErrorDialog", new DialogParameters($"message=Oops can't parse data received from server..."), _ => { });

        return null;
      }

      return wishlistDictionary.Values.Select(_mapper.Map<WishlistGameModel>).ToList();
    }
  }
}

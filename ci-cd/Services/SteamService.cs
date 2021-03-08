using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ci_cd.DTOs;
using ci_cd.Interfaces.Repositories;
using ci_cd.Interfaces.Services;
using ci_cd.Models;
using Newtonsoft.Json;

namespace ci_cd.Services
{
  public class SteamService : ISteamService
  {
    private readonly ISteamRepository _steamRepository;
    private readonly IMapper _mapper;

    public SteamService(ISteamRepository steamRepository, IMapper mapper)
    {
      _steamRepository = steamRepository;
      _mapper = mapper;
    }

    public async Task<List<WishlistGameModel>> GetGameModelsAsync(string steamID)
    {
      var receivedData = await _steamRepository.GetUserWishlistData(steamID);

      var jsonSerializerSettings = new JsonSerializerSettings();
      jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

      Dictionary<string, WishlistGameDto> wishlistDictionary;

      wishlistDictionary = JsonConvert.DeserializeObject<Dictionary<string, WishlistGameDto>>(receivedData, jsonSerializerSettings);

      return wishlistDictionary.Values.Select(_mapper.Map<WishlistGameModel>).ToList();
    }
  }
}

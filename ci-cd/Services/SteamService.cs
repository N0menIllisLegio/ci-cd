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
  internal class SteamService : ISteamService
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
      string receivedData = await _steamRepository.GetUserWishlistData(steamID);

      var jsonSerializerSettings = new JsonSerializerSettings();
      jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

      var wishlistDto = JsonConvert.DeserializeObject<Dictionary<string, WishlistGameDto>>(receivedData, jsonSerializerSettings);

      return wishlistDto.Values.Select(_mapper.Map<WishlistGameModel>).ToList();
    }
  }
}

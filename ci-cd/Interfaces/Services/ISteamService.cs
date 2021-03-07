using System.Collections.Generic;
using System.Threading.Tasks;
using ci_cd.Models;

namespace ci_cd.Interfaces.Services
{
  public interface ISteamService
  {
    Task<List<WishlistGameModel>> GetGameModelsAsync(string steamID);
  }
}

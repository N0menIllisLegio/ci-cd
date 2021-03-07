using System.Threading.Tasks;

namespace ci_cd.Interfaces.Repositories
{
  internal interface ISteamRepository
  {
    Task<string> GetUserWishlistData(string steamID);
  }
}

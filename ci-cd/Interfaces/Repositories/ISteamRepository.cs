using System.Threading.Tasks;

namespace ci_cd.Interfaces.Repositories
{
  public interface ISteamRepository
  {
    Task<string> GetUserWishlistData(string steamID);
  }
}

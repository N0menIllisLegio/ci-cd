using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ci_cd.Exceptions;
using ci_cd.Interfaces.Repositories;

namespace ci_cd.Repositories
{
  class SteamRepository : ISteamRepository
  {
    public async Task<string> GetUserWishlistData(string steamID)
    {
      string wishlistUrl = String.Format("https://store.steampowered.com/wishlist/profiles/{0}/wishlistdata/", steamID);

      var httpClient = new HttpClient();
      httpClient.Timeout = TimeSpan.FromSeconds(10);
      var response = await httpClient.GetAsync(wishlistUrl);

      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();
      }
      else
      {
        throw new AppHttpException(response.StatusCode, response.ReasonPhrase);
      }
    }
  }
}

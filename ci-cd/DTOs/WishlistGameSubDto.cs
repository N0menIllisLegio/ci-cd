using Newtonsoft.Json;

namespace ci_cd.DTOs
{
  class WishlistGameSubDto
  {
    [JsonProperty("id")]
    public int ID { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }
  }
}

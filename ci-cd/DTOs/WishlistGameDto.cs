using Newtonsoft.Json;

namespace ci_cd.DTOs
{
  class WishlistGameDto
  {
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("capsule")]
    public string Banner { get; set; }

    [JsonProperty("review_score")]
    public int ReviewScore { get; set; }

    [JsonProperty("review_desc")]
    public string ReviewDescription { get; set; }

    [JsonProperty("release_string")]
    public string ReleaseDate { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("background")]
    public string BackgroundUrl { get; set; }

    [JsonProperty("is_free_game")]
    public bool Free { get; set; }

    [JsonProperty("tags")]
    public string[] Tags { get; set; }

    [JsonProperty("subs")]
    public WishlistGameSubDto[] Subs { get; set; }
  }
}

namespace ci_cd.Models
{
  public class WishlistGameModel
  {
    public string Name { get; set; }
    public string Type { get; set; }
    public string Banner { get; set; }
    public int ReviewScore { get; set; }
    public string ReviewDescription { get; set; }
    public string ReleaseDate { get; set; }
    public bool Free { get; set; }
    public string[] Tags { get; set; }
    public decimal Price { get; set; }
  }
}

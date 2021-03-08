using System.Linq;

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

    public override bool Equals(object obj)
    {
      if (obj is WishlistGameModel compareWith)
      {
        return Name.Equals(compareWith.Name)
          && Price.Equals(compareWith.Price)
          && Free.Equals(compareWith.Free)
          && ReleaseDate.Equals(compareWith.ReleaseDate)
          && ReviewDescription.Equals(compareWith.ReviewDescription)
          && ReviewScore.Equals(compareWith.ReviewScore)
          && Tags.All(tag => compareWith.Tags.Contains(tag))
          && Type.Equals(compareWith.Type)
          && Banner.Equals(compareWith.Banner);
      }

      return false;
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int hash = 17;
        hash = hash * 23 + Name.GetHashCode();
        hash = hash * 23 + Type.GetHashCode();
        hash = hash * 23 + Banner.GetHashCode();
        hash = hash * 23 + Price.GetHashCode();
        return hash;
      }
    }
  }
}

using System;
using System.Globalization;
using ci_cd.Interfaces.Repositories;
using ci_cd.Repositories;
using ci_cd.Utils;
using Xunit;

namespace ci_cd.Tests
{
  public class WishListTests
  {
    private const string _steamID = "76561198092532909";

    private readonly ISteamRepository _steamRepository;
    private readonly Random _randomizer;

    public WishListTests()
    {
      _steamRepository = new SteamRepository();
      _randomizer = new Random();
    }

    [Fact]
    public async void RequestWishlist()
    {
      var requstedData = await _steamRepository.GetUserWishlistData(_steamID);

      Assert.NotNull(requstedData);
    }

    [Fact]
    public void TagsConverter()
    {
      var arrayToStringConverter = new ArrayToStringConverter();
      string[] tags = new string[]
      {
        "Turn-Based Strategy",
        "Tactical",
        "Strategy",
        "Sci-fi",
        "Turn-Based"
      };

      Assert.Equal("Turn-Based Strategy, Tactical, Strategy, Sci-fi, Turn-Based",
        arrayToStringConverter.Convert(tags, typeof(string), null, CultureInfo.InvariantCulture));
    }

    [Fact]
    public void BoolConverter()
    {
      var boolConverter = new BoolConverter();
      var trueResult = boolConverter.Convert(true, typeof(string), null, CultureInfo.InvariantCulture);
      var falseResult = boolConverter.Convert(false, typeof(string), null, CultureInfo.InvariantCulture);

      Assert.Equal("Yes", trueResult);
      Assert.Equal("No", falseResult);
    }

    [Fact]
    public void WishlistPriceConverter()
    {
      var wishlistPriceConverter = new WishlistPriceConverter();
      decimal value = (decimal)_randomizer.NextDouble();

      Assert.Equal($"${value}", wishlistPriceConverter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture));
    }

    [Fact]
    public void SteamIDValidation()
    {
      var steamIDValidationRule = new SteamIDValidationRule();

      Assert.True(steamIDValidationRule.Validate(_steamID, CultureInfo.InvariantCulture).IsValid);
      Assert.False(steamIDValidationRule.Validate("123", CultureInfo.InvariantCulture).IsValid);
      Assert.False(steamIDValidationRule.Validate("7656119809253290D", CultureInfo.InvariantCulture).IsValid);
      Assert.False(steamIDValidationRule.Validate(String.Empty, CultureInfo.InvariantCulture).IsValid);
    }
  }
}

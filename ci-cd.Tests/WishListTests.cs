using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using ci_cd.Interfaces.Repositories;
using ci_cd.Models;
using ci_cd.Repositories;
using ci_cd.Services;
using ci_cd.Utils;
using Moq;
using Xunit;

namespace ci_cd.Tests
{
  public class WishListTests
  {
    private const string _steamID = "76561198092532909";

    private readonly ISteamRepository _steamRepository;
    private readonly IMapper _mapper;
    private readonly Random _randomizer;

    public WishListTests()
    {
      _steamRepository = new SteamRepository();
      _randomizer = new Random();
      _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
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

    [Fact]
    public async void SteamServiceGetGameModels()
    {
      var mock = new Mock<ISteamRepository>();
      mock.Setup(repo => repo.GetUserWishlistData(_steamID).Result).Returns(GetUserWishlistData());
      var steamService = new SteamService(mock.Object, _mapper);

      var actualGameModels = await steamService.GetGameModelsAsync(_steamID);

      var expectedGameModels = new List<WishlistGameModel>
      {
        new WishlistGameModel
        {
          Name = "STAR WARS™ Jedi Knight - Jedi Academy™",
          Type = "Game",
          Price = 6.29m,
          ReleaseDate = "16 Sep, 2009",
          ReviewDescription = "Overwhelmingly Positive",
          ReviewScore = 9,
          Free = false,
          Banner = "https://cdn.akamai.steamstatic.com/steam/apps/6020/header_292x136.jpg?t=1586462966",
          Tags = new string[]
          {
            "Action",
            "Sci-fi",
            "Third Person",
            "Multiplayer",
            "Classic"
          }
        },

        new WishlistGameModel
        {
          Name = "Dreamfall: The Longest Journey",
          Type = "Game",
          Price = 12.59m,
          ReleaseDate = "12 Jan, 2007",
          ReviewDescription = "Very Positive",
          ReviewScore = 8,
          Free = false,
          Banner = "https://cdn.akamai.steamstatic.com/steam/apps/6300/header_292x136.jpg?t=1592476034",
          Tags = new string[]
          {
            "Adventure",
            "Female Protagonist",
            "Story Rich",
            "Fantasy",
            "Singleplayer"
          }
        },

        new WishlistGameModel
        {
          Name = "The Longest Journey",
          Type = "Game",
          Price = 6.99m,
          ReleaseDate = "1 May, 2007",
          ReviewDescription = "Very Positive",
          ReviewScore = 8,
          Free = false,
          Banner = "https://cdn.akamai.steamstatic.com/steam/apps/6310/header_292x136.jpg?t=1592476451",
          Tags = new string[]
          {
            "Adventure",
            "Point & Click",
            "Story Rich",
            "Female Protagonist",
            "Classic"
          }
        }
      };

      Assert.Equal(expectedGameModels, actualGameModels);
    }


    private string GetUserWishlistData()
    {
      return "{\"6020\":{\"name\":\"STAR WARS\\u2122 Jedi Knight - Jedi Academy\\u2122\",\"capsule\":\"https:\\/\\/cdn.akamai.steamstatic.com\\" +
        "/steam\\/apps\\/6020\\/header_292x136.jpg?t=1586462966\",\"review_score\":9,\"review_desc\":\"Overwhelmingly Positive\",\"reviews_total\"" +
        ":\"8,409\",\"reviews_percent\":96,\"release_date\":\"1253124960\",\"release_string\":\"16 Sep, 2009\",\"platform_icons\":\"<span class=\\\"" +
        "platform_img win\\\"><\\/span><span class=\\\"platform_img mac\\\"><\\/span>\",\"subs\":[{\"id\":2117,\"discount_block\":\"<div class=\\\"discount_block " +
        "discount_block_large no_discount\\\" data-price-final=\\\"629\\\"><div class=\\\"discount_prices\\\"><div class=\\\"discount_final_price\\\">$6.29 " +
        "USD<\\/div><\\/div><\\/div>\",\"discount_pct\":0,\"price\":629}],\"type\":\"Game\",\"screenshots\":[\"ss_55fce6453329fd1283a4329b1f6bd9de280c602d.j" +
        "pg\",\"ss_f0056ec9fa4f24f7b18ee90e919c71ea5424bb91.jpg\",\"ss_b025e54e7be0c0374a70cc7cda53d94e3eaf245c.jpg\",\"ss_a760fa150ea3be09f438923933f85d63ccc" +
        "9f943.jpg\"],\"review_css\":\"positive\",\"priority\":1,\"added\":1441995420,\"background\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/6" +
        "020\\/page_bg_generated_v6b.jpg?t=1586462966\",\"rank\":953,\"tags\":[\"Action\",\"Sci-fi\",\"Third Person\",\"Multiplayer\",\"Classic\"],\"is_free_gam" +
        "e\":false,\"win\":1,\"mac\":1},\"6300\":{\"name\":\"Dreamfall: The Longest Journey\",\"capsule\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/app" +
        "s\\/6300\\/header_292x136.jpg?t=1592476034\",\"review_score\":8,\"review_desc\":\"Very Positive\",\"reviews_total\":\"764\",\"reviews_percent\":85,\"re" +
        "lease_date\":\"1168628400\",\"release_string\":\"12 Jan, 2007\",\"platform_icons\":\"<span class=\\\"platform_img win\\\"><\\/span>\",\"subs\":[{\"id\":24" +
        "7,\"discount_block\":\"<div class=\\\"discount_block discount_block_large no_discount\\\" data-price-final=\\\"1259\\\"><div class=\\\"discount_prices\\\"" +
        "><div class=\\\"discount_final_price\\\">$12.59 USD<\\/div><\\/div><\\/div>\",\"discount_pct\":0,\"price\":1259},{\"id\":320,\"discount_block\":\"<div cla" +
        "ss=\\\"discount_block discount_block_large no_discount\\\" data-price-final=\\\"1499\\\"><div class=\\\"discount_prices\\\"><div class=\\\"discount_final_pr" +
        "ice\\\">$14.99 USD<\\/div><\\/div><\\/div>\",\"discount_pct\":0,\"price\":1499}],\"type\":\"Game\",\"screenshots\":[\"0000001130.jpg\",\"0000001131.jpg\",\"0" +
        "000001132.jpg\",\"0000001133.jpg\"],\"review_css\":\"positive\",\"priority\":10,\"added\":1467739570,\"background\":\"https:\\/\\/cdn.akamai.steamstatic.com" +
        "\\/steam\\/apps\\/6020\\/page_bg_generated_v6b.jpg?t=1586462966\",\"rank\":979,\"tags\":[\"Adventure\",\"Female Protagonist\",\"Story Rich\",\"Fantasy\",\"Si" +
        "ngleplayer\"],\"is_free_game\":false,\"win\":1},\"6310\":{\"name\":\"The Longest Journey\",\"capsule\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps" +
        "\\/6310\\/header_292x136.jpg?t=1592476451\",\"review_score\":8,\"review_desc\":\"Very Positive\",\"reviews_total\":\"1,335\",\"reviews_percent\":91,\"release_" +
        "date\":\"1178057280\",\"release_string\":\"1 May, 2007\",\"platform_icons\":\"<span class=\\\"platform_img win\\\"><\\/span>\",\"subs\":[{\"id\":319,\"discoun" +
        "t_block\":\"<div class=\\\"discount_block discount_block_large no_discount\\\" data-price-final=\\\"699\\\"><div class=\\\"discount_prices\\\"><div class=\\\"d" +
        "iscount_final_price\\\">$6.99 USD<\\/div><\\/div><\\/div>\",\"discount_pct\":0,\"price\":699},{\"id\":320,\"discount_block\":\"<div class=\\\"discount_block di" +
        "scount_block_large no_discount\\\" data-price-final=\\\"1499\\\"><div class=\\\"discount_prices\\\"><div class=\\\"discount_final_price\\\">$14.99 USD<\\/div><" +
        "\\/div><\\/div>\",\"discount_pct\":0,\"price\":1499}],\"type\":\"Game\",\"screenshots\":[\"0000001816.jpg\",\"0000001817.jpg\",\"0000001818.jpg\",\"0000001819." +
        "jpg\"],\"review_css\":\"positive\",\"priority\":12,\"added\":1467739586,\"background\":\"https:\\/\\/cdn.akamai.steamstatic.com\\/steam\\/apps\\/6020\\/page_bg" +
        "_generated_v6b.jpg?t=1586462966\",\"rank\":989,\"tags\":[\"Adventure\",\"Point & Click\",\"Story Rich\",\"Female Protagonist\",\"Classic\"],\"is_free_game\":fal" +
        "se,\"win\":1}}";
    }
  }
}

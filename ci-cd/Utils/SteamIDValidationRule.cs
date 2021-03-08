using System;
using System.Globalization;
using System.Windows.Controls;

namespace ci_cd.Utils
{
  public class SteamIDValidationRule : ValidationRule
  {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      string steamID = value as string;

      if (steamID?.Length != 17)
      {
        return new ValidationResult(false, "SteamID should consists of 17 digits");
      }

      if (!Int64.TryParse(steamID, out _))
      {
        return new ValidationResult(false, "SteamID should contain only digits");
      }

      return new ValidationResult(true, null);
    }
  }
}

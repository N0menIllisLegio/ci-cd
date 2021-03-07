using System;
using System.Globalization;
using System.Windows.Data;

namespace ci_cd.Utils
{
  class WishlistPriceConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      decimal price = (decimal)value;

      return price == 0 ? "—" : $"${price}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}

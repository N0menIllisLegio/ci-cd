using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ci_cd.Utils
{
  class ArrayToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string result = null;

      if (value is Array array)
      {
        var resultString = new StringBuilder();

        foreach (var arrayValue in array)
        {
          resultString.Append(arrayValue.ToString()).Append(", ");
        }

        result = resultString.Remove(resultString.Length - 2, 2).ToString();
      }

      return result;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}

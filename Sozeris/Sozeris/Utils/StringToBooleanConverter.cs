using System.Globalization;

namespace Sozeris.Utils;

public class StringToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string strValue)
        {
            return !string.IsNullOrEmpty(strValue) && strValue.Equals("true", StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "true" : "false";
        }
        return "false";
    }
}
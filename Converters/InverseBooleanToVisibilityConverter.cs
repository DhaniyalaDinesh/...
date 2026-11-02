using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUI.Converters;

/// <summary>
/// Converts a boolean value to its inverted Visibility (False -> Visible, True -> Collapsed).
/// </summary>
public class InverseBooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isTrue = value is true;
        return isTrue ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility.Collapsed or Visibility.Hidden;
    }
}

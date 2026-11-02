using System.Globalization;
using System.Windows.Data;

namespace WPFUI.Converters;

/// <summary>
/// Inverts a boolean value (true -> false, false -> true).
/// </summary>
public class BooleanToInverseBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not true;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not true;
    }
}

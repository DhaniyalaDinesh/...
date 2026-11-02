using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUI.Converters;

/// <summary>
/// Converts null or empty strings/collections to Visibility.Collapsed (or Visible when inverted).
/// </summary>
public class NullOrEmptyToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool isInverted = parameter as string == "Inverse" || parameter as string == "Inverted";

        bool isEmpty = value switch
        {
            null => true,
            string s => string.IsNullOrWhiteSpace(s),
            ICollection collection => collection.Count == 0,
            IEnumerable enumerable => !enumerable.GetEnumerator().MoveNext(),
            _ => false
        };

        if (isInverted)
        {
            return isEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        return isEmpty ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

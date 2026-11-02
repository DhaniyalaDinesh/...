using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using IWSEndpointCompanion.Models;

namespace WPFUI.Converters;

/// <summary>
/// Converts an AlertSeverity to its corresponding theme brush for badge and border rendering.
/// </summary>
public class SeverityToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is AlertSeverity severity)
        {
            var key = severity switch
            {
                AlertSeverity.Critical => "Brush_StatusError",
                AlertSeverity.Error => "Brush_StatusError",
                AlertSeverity.Warning => "Brush_StatusWarning",
                AlertSeverity.Information => "Brush_StatusInfo",
                _ => "Brush_StatusInfo"
            };

            if (parameter as string == "Background")
            {
                key = severity switch
                {
                    AlertSeverity.Critical => "Brush_StatusErrorBg",
                    AlertSeverity.Error => "Brush_StatusErrorBg",
                    AlertSeverity.Warning => "Brush_StatusWarningBg",
                    AlertSeverity.Information => "Brush_StatusInfoBg",
                    _ => "Brush_StatusInfoBg"
                };
            }

            if (Application.Current?.TryFindResource(key) is Brush brush)
            {
                return brush;
            }
        }

        return Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw freshNotSupported();

    private static NotSupportedException freshNotSupported() => new();
}

using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using IWSEndpointCompanion.Models;

namespace WPFUI.Converters;

/// <summary>
/// Converts an ActionRunStatus or health status level string into a semantic theme brush.
/// </summary>
public class StatusToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string key = "Brush_StatusInfo";
        bool isBg = parameter as string == "Background";

        if (value is ActionRunStatus status)
        {
            key = status switch
            {
                ActionRunStatus.Completed => isBg ? "Brush_StatusGoodBg" : "Brush_StatusGood",
                ActionRunStatus.Triggered => isBg ? "Brush_AccentSubtleBg" : "Brush_Accent",
                ActionRunStatus.NotStarted => isBg ? "Brush_StatusInfoBg" : "Brush_StatusInfo",
                ActionRunStatus.CompletedWithError => isBg ? "Brush_StatusWarningBg" : "Brush_StatusWarning",
                ActionRunStatus.Error => isBg ? "Brush_StatusErrorBg" : "Brush_StatusError",
                _ => isBg ? "Brush_StatusInfoBg" : "Brush_StatusInfo"
            };
        }
        else if (value is string str)
        {
            key = str.ToLowerInvariant() switch
            {
                "good" or "healthy" or "optimal" or "protected" or "encrypted" or "uptodate" or "connected" =>
                    isBg ? "Brush_StatusGoodBg" : "Brush_StatusGood",
                "warning" or "attention" =>
                    isBg ? "Brush_StatusWarningBg" : "Brush_StatusWarning",
                "error" or "critical" or "disconnected" =>
                    isBg ? "Brush_StatusErrorBg" : "Brush_StatusError",
                _ => isBg ? "Brush_StatusInfoBg" : "Brush_StatusInfo"
            };
        }

        if (Application.Current?.TryFindResource(key) is Brush brush)
        {
            return brush;
        }

        return Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

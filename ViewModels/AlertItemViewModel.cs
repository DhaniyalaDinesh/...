using System.Windows.Input;
using IWSEndpointCompanion.Models;

namespace WPFUI.ViewModels;

/// <summary>
/// Presentation wrapper for an alert item displayed on the Home dashboard.
/// Wraps an Alert from the Phase 1 domain models.
/// </summary>
public class AlertItemViewModel : ViewModelBase
{
    private bool _isDismissed;

    public AlertItemViewModel(
        Alert alert,
        string timeAgoText,
        string? suggestedActionName = null,
        ICommand? dismissCommand = null,
        ICommand? fixCommand = null)
    {
        Alert = alert;
        TimeAgoText = timeAgoText;
        SuggestedActionName = suggestedActionName;
        _isDismissed = alert.IsDismissed;
        DismissCommand = dismissCommand;
        FixCommand = fixCommand;
    }

    public Alert Alert { get; }
    public string Title => Alert.Title;
    public string Description => Alert.Description;
    public AlertSeverity Severity => Alert.Severity;
    public string TimeAgoText { get; }
    public string? SuggestedActionName { get; }

    public bool HasSuggestedAction => !string.IsNullOrWhiteSpace(SuggestedActionName);

    public string SeverityText => Severity switch
    {
        AlertSeverity.Critical => "Critical",
        AlertSeverity.Error => "Error",
        AlertSeverity.Warning => "Warning",
        AlertSeverity.Information => "Information",
        _ => "Alert"
    };

    public bool IsDismissed
    {
        get => _isDismissed;
        set
        {
            if (SetProperty(ref _isDismissed, value))
            {
                Alert.IsDismissed = value;
            }
        }
    }

    public ICommand? DismissCommand { get; }
    public ICommand? FixCommand { get; }
}

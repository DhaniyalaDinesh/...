using IWSEndpointCompanion.Models;

namespace WPFUI.ViewModels;

/// <summary>
/// Presentation wrapper for a recent execution record in the activity table on the Home dashboard.
/// Wraps an ActionRun from the Phase 1 domain models.
/// </summary>
public class RecentActivityItemViewModel : ViewModelBase
{
    public RecentActivityItemViewModel(ActionRun run, string formattedDateTime)
    {
        Run = run;
        FormattedDateTime = formattedDateTime;
    }

    public ActionRun Run { get; }
    public string FormattedDateTime { get; }

    public string ActionName => Run.ActionDefinition?.Name ?? Run.ActionId;
    public string Category => Run.ActionDefinition?.Category ?? "System";
    public ActionRunStatus Status => Run.Status;
    public string DisplayStatus => Run.DisplayStatus;

    public string FriendlyStatusText => Status switch
    {
        ActionRunStatus.Completed => "Completed",
        ActionRunStatus.Triggered => "Running",
        ActionRunStatus.NotStarted => "Queued",
        ActionRunStatus.CompletedWithError => "Completed with problems",
        ActionRunStatus.Error => "Couldn't run",
        ActionRunStatus.TimedOut => "Timed out",
        ActionRunStatus.Cancelled => "Cancelled",
        _ => !string.IsNullOrWhiteSpace(Run.OriginalStatus) ? Run.OriginalStatus : "Unknown"
    };

    public string DurationText => Run.Duration.HasValue
        ? $"{Run.Duration.Value.Minutes}m {Run.Duration.Value.Seconds}s"
        : (Status == ActionRunStatus.Triggered ? "In progress..." : (Status == ActionRunStatus.NotStarted ? "Pending" : "-"));
}

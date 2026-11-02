namespace IWSEndpointCompanion.Models;

/// <summary>
/// Represents an execution schedule for a specific action on the endpoint device.
/// </summary>
public class Schedule
{
    private string _scheduleId = string.Empty;
    private string _actionId = string.Empty;

    public Schedule()
    {
        ScheduledTime = DateTimeOffset.UtcNow;
    }

    public Schedule(
        string scheduleId,
        string actionId,
        DateTimeOffset scheduledTime,
        RecurrenceInfo? recurrence = null,
        DateTimeOffset? nextRunTime = null,
        bool isEnabled = true,
        bool skipNextRun = false)
    {
        ScheduleId = scheduleId;
        ActionId = actionId;
        ScheduledTime = scheduledTime;
        Recurrence = recurrence;
        NextRunTime = nextRunTime;
        IsEnabled = isEnabled;
        SkipNextRun = skipNextRun;
    }

    /// <summary>
    /// Unique schedule identifier.
    /// </summary>
    public string ScheduleId
    {
        get => _scheduleId;
        set => _scheduleId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Schedule ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Identifier of the action scheduled for execution.
    /// </summary>
    public string ActionId
    {
        get => _actionId;
        set => _actionId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Action ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Scheduled execution timestamp (or anchor start time for recurring schedules).
    /// </summary>
    public DateTimeOffset ScheduledTime { get; set; }

    /// <summary>
    /// Recurrence configuration, or null for one-time schedules.
    /// </summary>
    public RecurrenceInfo? Recurrence { get; set; }

    /// <summary>
    /// Helper property indicating whether the schedule recurs on a cadence.
    /// </summary>
    public bool IsRecurring => Recurrence != null && Recurrence.Pattern != RecurrencePattern.None;

    /// <summary>
    /// Next calculated timestamp when the action is scheduled to run.
    /// </summary>
    public DateTimeOffset? NextRunTime { get; set; }

    /// <summary>
    /// Indicates whether the schedule is active and eligible for execution.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Indicates whether the next scheduled execution occurrence should be skipped.
    /// </summary>
    public bool SkipNextRun { get; set; }
}

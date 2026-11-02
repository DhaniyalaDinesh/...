namespace IWSEndpointCompanion.Models;

/// <summary>
/// Strongly typed recurrence configuration for an action schedule.
/// </summary>
public class RecurrenceInfo
{
    private int _interval = 1;

    public RecurrenceInfo()
    {
    }

    public RecurrenceInfo(
        RecurrencePattern pattern,
        int interval = 1,
        IReadOnlyList<DayOfWeek>? daysOfWeek = null,
        TimeSpan? timeOfDay = null,
        DateTimeOffset? endDate = null)
    {
        Pattern = pattern;
        Interval = interval;
        DaysOfWeek = daysOfWeek;
        TimeOfDay = timeOfDay;
        EndDate = endDate;
    }

    /// <summary>
    /// Cadence pattern (e.g. Daily, Weekly, Monthly, Interval).
    /// </summary>
    public RecurrencePattern Pattern { get; init; } = RecurrencePattern.None;

    /// <summary>
    /// Interval count for the pattern (e.g., every 2 weeks). Must be >= 1.
    /// </summary>
    public int Interval
    {
        get => _interval;
        init => _interval = value > 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Interval must be greater than zero.");
    }

    /// <summary>
    /// Selected days of the week for weekly recurrence.
    /// </summary>
    public IReadOnlyList<DayOfWeek>? DaysOfWeek { get; init; }

    /// <summary>
    /// Time of day at which execution should trigger.
    /// </summary>
    public TimeSpan? TimeOfDay { get; init; }

    /// <summary>
    /// Optional expiration timestamp after which the recurrence stops.
    /// </summary>
    public DateTimeOffset? EndDate { get; init; }
}

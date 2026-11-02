namespace IWSEndpointCompanion.Models;

/// <summary>
/// Represents an execution record/history of an action on the endpoint device.
/// Preserves the raw status string from the backend or agent so unknown future statuses can be displayed.
/// </summary>
public class ActionRun
{
    private string _runId = string.Empty;
    private string _actionId = string.Empty;
    private string _originalStatus = string.Empty;
    private ActionRunStatus _status = ActionRunStatus.Unknown;

    public ActionRun()
    {
        CreatedTime = DateTimeOffset.UtcNow;
        LastChangedTime = DateTimeOffset.UtcNow;
    }

    public ActionRun(
        string runId,
        string actionId,
        string status,
        DateTimeOffset createdTime,
        DateTimeOffset lastChangedTime,
        ActionDefinition? actionDefinition = null,
        DateTimeOffset? scheduledTime = null,
        DateTimeOffset? triggeredTime = null,
        string? resultMessage = null,
        TimeSpan? duration = null)
    {
        RunId = runId;
        ActionId = actionId;
        CreatedTime = createdTime;
        LastChangedTime = lastChangedTime;
        ActionDefinition = actionDefinition;
        ScheduledTime = scheduledTime;
        TriggeredTime = triggeredTime;
        ResultMessage = resultMessage;
        Duration = duration;
        OriginalStatus = status;
    }

    /// <summary>
    /// Unique execution run identifier.
    /// </summary>
    public string RunId
    {
        get => _runId;
        set => _runId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Run ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Identifier of the executed action.
    /// </summary>
    public string ActionId
    {
        get => _actionId;
        set => _actionId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Action ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Associated catalogue action definition information, when available.
    /// </summary>
    public ActionDefinition? ActionDefinition { get; set; }

    /// <summary>
    /// Preserves the original raw status string exactly as supplied by the backend/agent,
    /// ensuring that unknown future statuses can always be displayed accurately in the UI.
    /// </summary>
    public string OriginalStatus
    {
        get => _originalStatus;
        set
        {
            _originalStatus = value ?? string.Empty;
            _status = ParseStatus(_originalStatus);
        }
    }

    /// <summary>
    /// Strongly typed action execution status. Returns ActionRunStatus.Unknown if
    /// OriginalStatus represents an unrecognized or future status.
    /// </summary>
    public ActionRunStatus Status
    {
        get => _status;
        set
        {
            _status = value;
            if (string.IsNullOrWhiteSpace(_originalStatus) || _status != ActionRunStatus.Unknown)
            {
                _originalStatus = value.ToString();
            }
        }
    }

    /// <summary>
    /// User-facing status string: returns OriginalStatus if populated, otherwise the enum name.
    /// </summary>
    public string DisplayStatus => !string.IsNullOrWhiteSpace(OriginalStatus)
        ? OriginalStatus
        : Status.ToString();

    /// <summary>
    /// Timestamp when this run record was created.
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// Scheduled execution timestamp, if applicable.
    /// </summary>
    public DateTimeOffset? ScheduledTime { get; set; }

    /// <summary>
    /// Timestamp when the action was triggered or started, if applicable.
    /// </summary>
    public DateTimeOffset? TriggeredTime { get; set; }

    /// <summary>
    /// Timestamp when this run record or status last changed.
    /// </summary>
    public DateTimeOffset LastChangedTime { get; set; }

    /// <summary>
    /// Execution output message, remediation details, or error message.
    /// </summary>
    public string? ResultMessage { get; set; }

    /// <summary>
    /// Execution duration, where available.
    /// </summary>
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// Parses a raw status string to a known ActionRunStatus enum value, returning Unknown if unrecognized.
    /// </summary>
    public static ActionRunStatus ParseStatus(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return ActionRunStatus.Unknown;
        }

        if (Enum.TryParse<ActionRunStatus>(status.Trim(), ignoreCase: true, out var parsed))
        {
            return parsed;
        }

        return status.Trim().ToLowerInvariant() switch
        {
            "in_progress" or "inprogress" or "executing" or "active" => ActionRunStatus.Running,
            "success" or "completed" or "done" => ActionRunStatus.Succeeded,
            "error" or "failure" or "faulted" => ActionRunStatus.Failed,
            "canceled" or "aborted" or "stopped" => ActionRunStatus.Cancelled,
            "timeout" or "timed_out" => ActionRunStatus.TimedOut,
            "queued" or "waiting" => ActionRunStatus.Pending,
            _ => ActionRunStatus.Unknown
        };
    }
}

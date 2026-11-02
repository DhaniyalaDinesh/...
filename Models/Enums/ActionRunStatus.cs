namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// Execution status states for an action run, matching the endpoint management backend.
/// </summary>
public enum ActionRunStatus
{
    Unknown = 0,
    NotStarted = 1,
    Triggered = 2,
    Completed = 3,
    CompletedWithError = 4,
    Processed = 5,
    Error = 6,
    Cancelled = 7,
    TimedOut = 8,

    // Aliases for compatibility
    Pending = NotStarted,
    Running = Triggered,
    Succeeded = Completed,
    Failed = CompletedWithError
}

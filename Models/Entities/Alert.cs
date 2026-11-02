namespace IWSEndpointCompanion.Models;

/// <summary>
/// Represents a health issue, security warning, or proactive recommendation on the endpoint device.
/// </summary>
public class Alert
{
    private string _alertId = string.Empty;
    private string _title = string.Empty;
    private string _description = string.Empty;

    public Alert()
    {
        CreationTime = DateTimeOffset.UtcNow;
    }

    public Alert(
        string alertId,
        string title,
        string description,
        AlertSeverity severity,
        DateTimeOffset creationTime,
        string? relatedActionId = null,
        bool isDismissed = false)
    {
        AlertId = alertId;
        Title = title;
        Description = description;
        Severity = severity;
        CreationTime = creationTime;
        RelatedActionId = relatedActionId;
        IsDismissed = isDismissed;
    }

    /// <summary>
    /// Unique identifier for the alert.
    /// </summary>
    public string AlertId
    {
        get => _alertId;
        set => _alertId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Alert ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Summary title of the alert.
    /// </summary>
    public string Title
    {
        get => _title;
        set => _title = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Title cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Detailed description and impact of the alert condition.
    /// </summary>
    public string Description
    {
        get => _description;
        set => _description = value ?? string.Empty;
    }

    /// <summary>
    /// Alert severity level (Information, Warning, Error, Critical).
    /// </summary>
    public AlertSeverity Severity { get; set; } = AlertSeverity.Information;

    /// <summary>
    /// Timestamp when the alert was triggered or detected.
    /// </summary>
    public DateTimeOffset CreationTime { get; set; }

    /// <summary>
    /// Optional Action ID for an approved remediation action capable of resolving this alert.
    /// </summary>
    public string? RelatedActionId { get; set; }

    /// <summary>
    /// Indicates whether this alert has been dismissed by the user.
    /// </summary>
    public bool IsDismissed { get; set; }

    /// <summary>
    /// Marks the alert as dismissed.
    /// </summary>
    public void Dismiss()
    {
        IsDismissed = true;
    }
}

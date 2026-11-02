namespace IWSEndpointCompanion.Models;

/// <summary>
/// Defines an approved diagnostic or remediation action available in the catalogue.
/// </summary>
public class ActionDefinition
{
    private string _actionId = string.Empty;
    private string _scriptId = string.Empty;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private string _category = string.Empty;

    public ActionDefinition()
    {
    }

    public ActionDefinition(
        string actionId,
        string scriptId,
        string name,
        string description,
        string category,
        ActionType actionType,
        TimeSpan? estimatedDuration = null,
        string? warningMessage = null,
        ActionAvailability availability = ActionAvailability.Available,
        string? reasonWhenUnavailable = null,
        bool requiresConfirmation = false,
        DateTimeOffset? lastSuccessfulRunTime = null)
    {
        ActionId = actionId;
        ScriptId = scriptId;
        Name = name;
        Description = description;
        Category = category;
        ActionType = actionType;
        EstimatedDuration = estimatedDuration;
        WarningMessage = warningMessage;
        Availability = availability;
        ReasonWhenUnavailable = reasonWhenUnavailable;
        RequiresConfirmation = requiresConfirmation;
        LastSuccessfulRunTime = lastSuccessfulRunTime;
    }

    /// <summary>
    /// Unique identifier for the action definition.
    /// </summary>
    public string ActionId
    {
        get => _actionId;
        set => _actionId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Action ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Script ID, stored exactly as supplied by the catalogue without mutation or normalization.
    /// </summary>
    public string ScriptId
    {
        get => _scriptId;
        set => _scriptId = value ?? throw new ArgumentNullException(nameof(value), "Script ID cannot be null.");
    }

    /// <summary>
    /// Display name of the action.
    /// </summary>
    public string Name
    {
        get => _name;
        set => _name = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Name cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Detailed description of the action purpose and behavior.
    /// </summary>
    public string Description
    {
        get => _description;
        set => _description = value ?? string.Empty;
    }

    /// <summary>
    /// Functional category (e.g., "Disk Space", "Network", "Security", "Maintenance").
    /// </summary>
    public string Category
    {
        get => _category;
        set => _category = value ?? string.Empty;
    }

    /// <summary>
    /// Optional resource key or identifier for an icon representing this action.
    /// </summary>
    public string? IconKey { get; set; }

    /// <summary>
    /// Action classification: Detection or Remediation.
    /// </summary>
    public ActionType ActionType { get; set; } = ActionType.Detection;

    /// <summary>
    /// Estimated duration for execution to complete.
    /// </summary>
    public TimeSpan? EstimatedDuration { get; set; }

    /// <summary>
    /// Warning message to present to the user prior to execution (e.g., app restart notice).
    /// </summary>
    public string? WarningMessage { get; set; }

    /// <summary>
    /// Current availability state of the action on this device.
    /// </summary>
    public ActionAvailability Availability { get; set; } = ActionAvailability.Available;

    /// <summary>
    /// Convenience helper indicating if the action is currently executable.
    /// </summary>
    public bool IsAvailable => Availability == ActionAvailability.Available;

    /// <summary>
    /// Reason explaining why the action is currently unavailable, if applicable.
    /// </summary>
    public string? ReasonWhenUnavailable { get; set; }

    /// <summary>
    /// Indicates whether the user must explicitly confirm execution before the action runs.
    /// </summary>
    public bool RequiresConfirmation { get; set; }

    /// <summary>
    /// Timestamp when this action last completed successfully on this device.
    /// </summary>
    public DateTimeOffset? LastSuccessfulRunTime { get; set; }
}

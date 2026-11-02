namespace IWSEndpointCompanion.Models;

/// <summary>
/// Represents a submission to trigger or schedule an action execution on an endpoint.
/// Note: Action name, action type, context, and scope are omitted because they are catalogue-managed.
/// </summary>
public class ActionRequest
{
    private string _deviceId = string.Empty;
    private string _scriptId = string.Empty;
    private string _source = string.Empty;

    public ActionRequest()
    {
    }

    public ActionRequest(
        string deviceId,
        string scriptId,
        string source,
        DateTimeOffset? scheduledTime = null,
        IReadOnlyDictionary<string, string>? parameters = null,
        ClientInfo? clientInfo = null)
    {
        DeviceId = deviceId;
        ScriptId = scriptId;
        Source = source;
        ScheduledTime = scheduledTime;
        Parameters = parameters;
        ClientInfo = clientInfo;
    }

    /// <summary>
    /// Target endpoint device identifier.
    /// </summary>
    public string DeviceId
    {
        get => _deviceId;
        set => _deviceId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Device ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Script ID stored exactly as supplied by the catalogue.
    /// </summary>
    public string ScriptId
    {
        get => _scriptId;
        set => _scriptId = value ?? throw new ArgumentNullException(nameof(value), "Script ID cannot be null.");
    }

    /// <summary>
    /// Trigger source (e.g., "UserSelfService", "ProactiveAlert", "ScheduledTask", "Administrator").
    /// </summary>
    public string Source
    {
        get => _source;
        set => _source = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Source cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Optional scheduled execution time. When null, requests immediate execution.
    /// </summary>
    public DateTimeOffset? ScheduledTime { get; set; }

    /// <summary>
    /// Optional key-value parameters passed to the script execution.
    /// </summary>
    public IReadOnlyDictionary<string, string>? Parameters { get; set; }

    /// <summary>
    /// Optional client environment and identity metadata.
    /// </summary>
    public ClientInfo? ClientInfo { get; set; }
}

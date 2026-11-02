namespace IWSEndpointCompanion.Models;

/// <summary>
/// Telemetry regarding the endpoint agent's connection state, latency, and heartbeat.
/// </summary>
public class ConnectionStatusInfo
{
    public ConnectionStatusInfo()
    {
    }

    public ConnectionStatusInfo(
        AgentConnectionState state,
        DateTimeOffset? lastHeartbeatTime = null,
        TimeSpan? latency = null,
        string? statusMessage = null)
    {
        State = state;
        LastHeartbeatTime = lastHeartbeatTime;
        Latency = latency;
        StatusMessage = statusMessage;
    }

    /// <summary>
    /// Current connection state.
    /// </summary>
    public AgentConnectionState State { get; init; } = AgentConnectionState.Unknown;

    /// <summary>
    /// Timestamp of the last received heartbeat or ping.
    /// </summary>
    public DateTimeOffset? LastHeartbeatTime { get; init; }

    /// <summary>
    /// Round-trip communication latency with the agent or service.
    /// </summary>
    public TimeSpan? Latency { get; init; }

    /// <summary>
    /// Descriptive diagnostic status message (e.g., "Agent running normally").
    /// </summary>
    public string? StatusMessage { get; init; }

    /// <summary>
    /// Indicates whether the agent connection is healthy and active.
    /// </summary>
    public bool IsConnected => State == AgentConnectionState.Connected;
}

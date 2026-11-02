namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// Connection state between the endpoint companion and the background management agent.
/// </summary>
public enum AgentConnectionState
{
    Unknown = 0,
    Disconnected = 1,
    Connecting = 2,
    Connected = 3,
    Degraded = 4,
    Error = 5
}

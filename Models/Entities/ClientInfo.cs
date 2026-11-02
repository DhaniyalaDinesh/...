namespace IWSEndpointCompanion.Models;

/// <summary>
/// Client environment and identity metadata accompanying an action request.
/// </summary>
public class ClientInfo
{
    public ClientInfo()
    {
    }

    public ClientInfo(
        string? applicationVersion,
        string? clientHostName = null,
        string? userName = null,
        string? ipAddress = null,
        string? operatingSystem = null)
    {
        ApplicationVersion = applicationVersion;
        ClientHostName = clientHostName;
        UserName = userName;
        IpAddress = ipAddress;
        OperatingSystem = operatingSystem;
    }

    /// <summary>
    /// Companion application version string.
    /// </summary>
    public string? ApplicationVersion { get; init; }

    /// <summary>
    /// Hostname of the client machine.
    /// </summary>
    public string? ClientHostName { get; init; }

    /// <summary>
    /// Signed-in user account or UPN.
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// Client IP address.
    /// </summary>
    public string? IpAddress { get; init; }

    /// <summary>
    /// Operating system version and build.
    /// </summary>
    public string? OperatingSystem { get; init; }
}

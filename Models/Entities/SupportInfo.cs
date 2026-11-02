namespace IWSEndpointCompanion.Models;

/// <summary>
/// Encapsulates technical support contact information, ticketing portal access,
/// application and OS versioning, and endpoint agent connection status.
/// </summary>
public class SupportInfo
{
    private string _applicationVersion = string.Empty;
    private string _windowsVersion = string.Empty;

    public SupportInfo()
    {
        SupportContact = new SupportContactDetails();
        AgentConnectionStatus = new ConnectionStatusInfo();
    }

    public SupportInfo(
        SupportContactDetails supportContact,
        Uri? ticketUrl,
        string applicationVersion,
        string windowsVersion,
        ConnectionStatusInfo agentConnectionStatus)
    {
        SupportContact = supportContact ?? new SupportContactDetails();
        TicketUrl = ticketUrl;
        ApplicationVersion = applicationVersion;
        WindowsVersion = windowsVersion;
        AgentConnectionStatus = agentConnectionStatus ?? new ConnectionStatusInfo();
    }

    /// <summary>
    /// Helpdesk and IT support contact details.
    /// </summary>
    public SupportContactDetails SupportContact { get; set; }

    /// <summary>
    /// Direct URL to the IT ticketing / support portal.
    /// </summary>
    public Uri? TicketUrl { get; set; }

    /// <summary>
    /// Version string of the IWSEndpointCompanion application (e.g. "1.0.0.0").
    /// </summary>
    public string ApplicationVersion
    {
        get => _applicationVersion;
        set => _applicationVersion = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Application version cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Operating system edition and build version string (e.g. "Windows 11 Enterprise 23H2 (Build 22631.4317)").
    /// </summary>
    public string WindowsVersion
    {
        get => _windowsVersion;
        set => _windowsVersion = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Windows version cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Current connection status of the endpoint background agent.
    /// </summary>
    public ConnectionStatusInfo AgentConnectionStatus { get; set; }
}

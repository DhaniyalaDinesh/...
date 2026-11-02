namespace IWSEndpointCompanion.Models;

/// <summary>
/// Contact and service desk details for technical support.
/// </summary>
public class SupportContactDetails
{
    public SupportContactDetails()
    {
    }

    public SupportContactDetails(
        string? email,
        string? phoneNumber = null,
        string? teamName = null,
        string? operatingHours = null)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        TeamName = teamName;
        OperatingHours = operatingHours;
    }

    /// <summary>
    /// Support desk email address (e.g. "it.support@company.com").
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Support hotline phone number (e.g. "+1-800-555-0199").
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Name of the responsible support team (e.g. "Global IT Service Desk").
    /// </summary>
    public string? TeamName { get; init; }

    /// <summary>
    /// Available operational hours (e.g. "24/7" or "Mon-Fri 08:00 - 18:00 EST").
    /// </summary>
    public string? OperatingHours { get; init; }
}

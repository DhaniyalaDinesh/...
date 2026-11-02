namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// Network profile types for Windows Firewall.
/// </summary>
[Flags]
public enum FirewallProfiles
{
    None = 0,
    Domain = 1,
    Private = 2,
    Public = 4,
    All = Domain | Private | Public
}

/// <summary>
/// Strongly typed Windows Firewall status across network profiles.
/// </summary>
public class FirewallStatus
{
    public FirewallStatus()
    {
    }

    public FirewallStatus(
        bool isEnabled,
        bool domainProfileEnabled = true,
        bool privateProfileEnabled = true,
        bool publicProfileEnabled = true)
    {
        IsEnabled = isEnabled;
        DomainProfileEnabled = domainProfileEnabled;
        PrivateProfileEnabled = privateProfileEnabled;
        PublicProfileEnabled = publicProfileEnabled;
    }

    /// <summary>
    /// Indicates whether firewall protection is active overall.
    /// </summary>
    public bool IsEnabled { get; init; }

    /// <summary>
    /// Indicates whether firewall is enabled on Domain networks.
    /// </summary>
    public bool DomainProfileEnabled { get; init; }

    /// <summary>
    /// Indicates whether firewall is enabled on Private networks.
    /// </summary>
    public bool PrivateProfileEnabled { get; init; }

    /// <summary>
    /// Indicates whether firewall is enabled on Public networks.
    /// </summary>
    public bool PublicProfileEnabled { get; init; }

    /// <summary>
    /// Bitmask flags of actively protected firewall profiles.
    /// </summary>
    public FirewallProfiles ActiveProfiles
    {
        get
        {
            FirewallProfiles profiles = FirewallProfiles.None;
            if (DomainProfileEnabled) profiles |= FirewallProfiles.Domain;
            if (PrivateProfileEnabled) profiles |= FirewallProfiles.Private;
            if (PublicProfileEnabled) profiles |= FirewallProfiles.Public;
            return profiles;
        }
    }
}

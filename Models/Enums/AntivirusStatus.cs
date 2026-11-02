namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// Strongly typed antivirus protection and signature state.
/// </summary>
public class AntivirusStatus
{
    public AntivirusStatus()
    {
    }

    public AntivirusStatus(
        bool isEnabled,
        bool isUpToDate,
        string? productName = null,
        string? definitionVersion = null,
        DateTimeOffset? definitionsLastUpdated = null,
        bool realTimeProtectionEnabled = true)
    {
        IsEnabled = isEnabled;
        IsUpToDate = isUpToDate;
        ProductName = productName;
        DefinitionVersion = definitionVersion;
        DefinitionsLastUpdated = definitionsLastUpdated;
        RealTimeProtectionEnabled = realTimeProtectionEnabled;
    }

    /// <summary>
    /// Indicates whether antivirus protection is active on the device.
    /// </summary>
    public bool IsEnabled { get; init; }

    /// <summary>
    /// Indicates whether virus signatures and definitions are current.
    /// </summary>
    public bool IsUpToDate { get; init; }

    /// <summary>
    /// Name of the installed antivirus product (e.g. "Microsoft Defender Antivirus").
    /// </summary>
    public string? ProductName { get; init; }

    /// <summary>
    /// Installed antivirus definitions/signature version string.
    /// </summary>
    public string? DefinitionVersion { get; init; }

    /// <summary>
    /// Timestamp when definitions were last updated.
    /// </summary>
    public DateTimeOffset? DefinitionsLastUpdated { get; init; }

    /// <summary>
    /// Indicates whether real-time scanning protection is currently enabled.
    /// </summary>
    public bool RealTimeProtectionEnabled { get; init; } = true;
}

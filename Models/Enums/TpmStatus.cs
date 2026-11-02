namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// Strongly typed Trusted Platform Module (TPM) hardware security status.
/// </summary>
public class TpmStatus
{
    public TpmStatus()
    {
    }

    public TpmStatus(
        bool isPresent,
        bool isEnabled,
        bool isActivated,
        string? specificationVersion = "2.0",
        string? manufacturer = null)
    {
        IsPresent = isPresent;
        IsEnabled = isEnabled;
        IsActivated = isActivated;
        SpecificationVersion = specificationVersion;
        Manufacturer = manufacturer;
    }

    /// <summary>
    /// Indicates whether a TPM chip is physically present or recognized by firmware.
    /// </summary>
    public bool IsPresent { get; init; }

    /// <summary>
    /// Indicates whether the TPM is enabled in BIOS/UEFI.
    /// </summary>
    public bool IsEnabled { get; init; }

    /// <summary>
    /// Indicates whether the TPM has been activated and owned by the OS.
    /// </summary>
    public bool IsActivated { get; init; }

    /// <summary>
    /// TPM specification version (e.g. "2.0", "1.2").
    /// </summary>
    public string? SpecificationVersion { get; init; } = "2.0";

    /// <summary>
    /// TPM manufacturer identifier (e.g. "IFX", "AMD", "INTC").
    /// </summary>
    public string? Manufacturer { get; init; }

    /// <summary>
    /// Indicates whether the TPM is present, enabled, and activated for cryptographic operations.
    /// </summary>
    public bool IsReady => IsPresent && IsEnabled && IsActivated;
}

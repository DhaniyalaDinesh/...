namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// BitLocker volume encryption and protection states.
/// </summary>
public enum BitLockerProtectionState
{
    Unknown = 0,
    Protected = 1,
    Unprotected = 2,
    Encrypting = 3,
    Decrypting = 4,
    Suspended = 5
}

/// <summary>
/// Strongly typed BitLocker volume encryption and protection status.
/// </summary>
public class BitLockerStatus
{
    private double? _encryptionPercentage;

    public BitLockerStatus()
    {
    }

    public BitLockerStatus(
        bool isProtectionOn,
        BitLockerProtectionState protectionState = BitLockerProtectionState.Unknown,
        double? encryptionPercentage = null,
        string? driveLetter = "C:")
    {
        IsProtectionOn = isProtectionOn;
        ProtectionState = protectionState;
        EncryptionPercentage = encryptionPercentage;
        DriveLetter = driveLetter;
    }

    /// <summary>
    /// Indicates whether BitLocker protection is active on the volume.
    /// </summary>
    public bool IsProtectionOn { get; init; }

    /// <summary>
    /// Specific protection state of the drive.
    /// </summary>
    public BitLockerProtectionState ProtectionState { get; init; } = BitLockerProtectionState.Unknown;

    /// <summary>
    /// Percentage of the drive volume encrypted (0.0 to 100.0).
    /// </summary>
    public double? EncryptionPercentage
    {
        get => _encryptionPercentage;
        init => _encryptionPercentage = value is < 0.0 or > 100.0
            ? throw new ArgumentOutOfRangeException(nameof(value), "Encryption percentage must be between 0 and 100.")
            : value;
    }

    /// <summary>
    /// Protected drive letter (e.g. "C:").
    /// </summary>
    public string? DriveLetter { get; init; } = "C:";

    /// <summary>
    /// Helper property indicating whether the volume is fully protected and encrypted.
    /// </summary>
    public bool IsEncrypted => IsProtectionOn &&
        (ProtectionState == BitLockerProtectionState.Protected || EncryptionPercentage >= 100.0);
}

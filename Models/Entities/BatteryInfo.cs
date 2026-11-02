namespace IWSEndpointCompanion.Models;

/// <summary>
/// Status of the endpoint battery.
/// </summary>
public enum BatteryStatus
{
    Unknown = 0,
    Charging = 1,
    Discharging = 2,
    Full = 3,
    NotPresent = 4
}

/// <summary>
/// Strongly typed battery telemetry and health metrics.
/// </summary>
public class BatteryInfo
{
    private int? _chargePercentage;
    private int? _healthPercentage;

    public BatteryInfo()
    {
    }

    public BatteryInfo(
        int? chargePercentage,
        bool isCharging,
        bool isPluggedIn,
        TimeSpan? estimatedRunTime = null,
        int? healthPercentage = null,
        BatteryStatus status = BatteryStatus.Unknown)
    {
        ChargePercentage = chargePercentage;
        IsCharging = isCharging;
        IsPluggedIn = isPluggedIn;
        EstimatedRunTime = estimatedRunTime;
        HealthPercentage = healthPercentage;
        Status = status;
    }

    /// <summary>
    /// Current battery charge level in percent (0 - 100), or null if unknown or not present.
    /// </summary>
    public int? ChargePercentage
    {
        get => _chargePercentage;
        init => _chargePercentage = value is < 0 or > 100
            ? throw new ArgumentOutOfRangeException(nameof(value), "Charge percentage must be between 0 and 100.")
            : value;
    }

    /// <summary>
    /// Indicates whether the battery is currently charging.
    /// </summary>
    public bool IsCharging { get; init; }

    /// <summary>
    /// Indicates whether the system is connected to external AC power.
    /// </summary>
    public bool IsPluggedIn { get; init; }

    /// <summary>
    /// Estimated remaining operational battery time.
    /// </summary>
    public TimeSpan? EstimatedRunTime { get; init; }

    /// <summary>
    /// Overall battery health/capacity percentage relative to design capacity (0 - 100).
    /// </summary>
    public int? HealthPercentage
    {
        get => _healthPercentage;
        init => _healthPercentage = value is < 0 or > 100
            ? throw new ArgumentOutOfRangeException(nameof(value), "Health percentage must be between 0 and 100.")
            : value;
    }

    /// <summary>
    /// Operational status of the battery.
    /// </summary>
    public BatteryStatus Status { get; init; } = BatteryStatus.Unknown;
}

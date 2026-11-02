namespace IWSEndpointCompanion.Models;

/// <summary>
/// Represents the endpoint device profile, including hardware attributes, OS information,
/// restart telemetry, disk and battery metrics, and key security subsystem statuses.
/// </summary>
public class DeviceProfile
{
    private string _deviceId = string.Empty;
    private string _deviceName = string.Empty;
    private string _operatingSystem = string.Empty;

    public DeviceProfile()
    {
        Antivirus = new AntivirusStatus();
        BitLocker = new BitLockerStatus();
        Tpm = new TpmStatus();
        Firewall = new FirewallStatus();
        WindowsUpdate = new WindowsUpdateStatus();
        Disks = Array.Empty<DiskUsageInfo>();
    }

    public DeviceProfile(
        string deviceId,
        string deviceName,
        string operatingSystem,
        string? model = null,
        string? serialNumber = null,
        DateTimeOffset? lastRestartTime = null,
        IReadOnlyList<DiskUsageInfo>? disks = null,
        BatteryInfo? battery = null,
        AntivirusStatus? antivirus = null,
        BitLockerStatus? bitLocker = null,
        TpmStatus? tpm = null,
        FirewallStatus? firewall = null,
        WindowsUpdateStatus? windowsUpdate = null)
    {
        DeviceId = deviceId;
        DeviceName = deviceName;
        OperatingSystem = operatingSystem;
        Model = model;
        SerialNumber = serialNumber;
        LastRestartTime = lastRestartTime;
        Disks = disks ?? Array.Empty<DiskUsageInfo>();
        Battery = battery;
        Antivirus = antivirus ?? new AntivirusStatus();
        BitLocker = bitLocker ?? new BitLockerStatus();
        Tpm = tpm ?? new TpmStatus();
        Firewall = firewall ?? new FirewallStatus();
        WindowsUpdate = windowsUpdate ?? new WindowsUpdateStatus();
    }

    /// <summary>
    /// Unique identifier for the endpoint device (e.g., Azure AD Device ID, Intune ID, or UUID).
    /// </summary>
    public string DeviceId
    {
        get => _deviceId;
        set => _deviceId = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Device ID cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Computer network name / hostname.
    /// </summary>
    public string DeviceName
    {
        get => _deviceName;
        set => _deviceName = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Device name cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Hardware model name (e.g. "ThinkPad T14 Gen 3", "Surface Pro 9").
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Operating system name, edition, and build (e.g. "Windows 11 Enterprise 23H2").
    /// </summary>
    public string OperatingSystem
    {
        get => _operatingSystem;
        set => _operatingSystem = !string.IsNullOrWhiteSpace(value)
            ? value.Trim()
            : throw new ArgumentException("Operating system cannot be null or whitespace.", nameof(value));
    }

    /// <summary>
    /// Hardware serial number / service tag.
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Timestamp of the last system boot / restart.
    /// </summary>
    public DateTimeOffset? LastRestartTime { get; set; }

    /// <summary>
    /// Storage metrics across all local disks and partitions.
    /// </summary>
    public IReadOnlyList<DiskUsageInfo> Disks { get; set; }

    /// <summary>
    /// Disk usage information for the primary/system volume (or first volume).
    /// </summary>
    public DiskUsageInfo? DiskUsage =>
        Disks.FirstOrDefault(d => d.IsSystemDrive) ?? Disks.FirstOrDefault();

    /// <summary>
    /// Convenience alias for the primary/system volume.
    /// </summary>
    public DiskUsageInfo? PrimaryDisk => DiskUsage;

    /// <summary>
    /// Battery and power telemetry. Null if the device is a desktop or battery is not present.
    /// </summary>
    public BatteryInfo? Battery { get; set; }

    /// <summary>
    /// Antivirus protection and definition status.
    /// </summary>
    public AntivirusStatus Antivirus { get; set; }

    /// <summary>
    /// BitLocker drive encryption status.
    /// </summary>
    public BitLockerStatus BitLocker { get; set; }

    /// <summary>
    /// TPM hardware security processor status.
    /// </summary>
    public TpmStatus Tpm { get; set; }

    /// <summary>
    /// Windows Defender Firewall protection status.
    /// </summary>
    public FirewallStatus Firewall { get; set; }

    /// <summary>
    /// Windows Update compliance and pending patch status.
    /// </summary>
    public WindowsUpdateStatus WindowsUpdate { get; set; }
}

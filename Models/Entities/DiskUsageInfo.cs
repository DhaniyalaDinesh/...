namespace IWSEndpointCompanion.Models;

/// <summary>
/// Represents disk storage and capacity metrics for a specific logical drive.
/// </summary>
public class DiskUsageInfo
{
    private long _totalSizeBytes;
    private long _freeSizeBytes;

    public DiskUsageInfo()
    {
        DriveLetter = "C:";
    }

    public DiskUsageInfo(
        string driveLetter,
        long totalSizeBytes,
        long freeSizeBytes,
        string? volumeLabel = null,
        string? fileSystem = null,
        bool isSystemDrive = false)
    {
        if (string.IsNullOrWhiteSpace(driveLetter))
        {
            throw new ArgumentException("Drive letter cannot be null or empty.", nameof(driveLetter));
        }

        if (totalSizeBytes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalSizeBytes), "Total size cannot be negative.");
        }

        if (freeSizeBytes < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(freeSizeBytes), "Free size cannot be negative.");
        }

        if (freeSizeBytes > totalSizeBytes)
        {
            throw new ArgumentException("Free size cannot exceed total size.", nameof(freeSizeBytes));
        }

        DriveLetter = driveLetter.Trim();
        TotalSizeBytes = totalSizeBytes;
        FreeSizeBytes = freeSizeBytes;
        VolumeLabel = volumeLabel;
        FileSystem = fileSystem;
        IsSystemDrive = isSystemDrive;
    }

    /// <summary>
    /// Logical drive identifier (e.g., "C:", "D:").
    /// </summary>
    public string DriveLetter { get; init; } = "C:";

    /// <summary>
    /// Optional volume label (e.g., "Windows", "Data").
    /// </summary>
    public string? VolumeLabel { get; init; }

    /// <summary>
    /// File system format (e.g., "NTFS", "ReFS").
    /// </summary>
    public string? FileSystem { get; init; }

    /// <summary>
    /// Total drive capacity in bytes.
    /// </summary>
    public long TotalSizeBytes
    {
        get => _totalSizeBytes;
        init => _totalSizeBytes = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Total size cannot be negative.");
    }

    /// <summary>
    /// Remaining free storage in bytes.
    /// </summary>
    public long FreeSizeBytes
    {
        get => _freeSizeBytes;
        init => _freeSizeBytes = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "Free size cannot be negative.");
    }

    /// <summary>
    /// Calculated used space in bytes.
    /// </summary>
    public long UsedSizeBytes => Math.Max(0, TotalSizeBytes - FreeSizeBytes);

    /// <summary>
    /// Used disk space expressed as a percentage (0.0 to 100.0).
    /// </summary>
    public double UsedPercentage => TotalSizeBytes > 0
        ? Math.Round((double)UsedSizeBytes / TotalSizeBytes * 100.0, 2)
        : 0.0;

    /// <summary>
    /// Free disk space expressed as a percentage (0.0 to 100.0).
    /// </summary>
    public double FreePercentage => TotalSizeBytes > 0
        ? Math.Round((double)FreeSizeBytes / TotalSizeBytes * 100.0, 2)
        : 0.0;

    /// <summary>
    /// Indicates whether this drive is the primary operating system boot volume.
    /// </summary>
    public bool IsSystemDrive { get; init; }
}

namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// Strongly typed Windows Update status and patch compliance.
/// </summary>
public class WindowsUpdateStatus
{
    private int _pendingUpdatesCount;

    public WindowsUpdateStatus()
    {
    }

    public WindowsUpdateStatus(
        bool isUpToDate,
        int pendingUpdatesCount,
        bool rebootRequired,
        DateTimeOffset? lastCheckTime = null,
        DateTimeOffset? lastInstallationTime = null)
    {
        IsUpToDate = isUpToDate;
        PendingUpdatesCount = pendingUpdatesCount;
        RebootRequired = rebootRequired;
        LastCheckTime = lastCheckTime;
        LastInstallationTime = lastInstallationTime;
    }

    /// <summary>
    /// Indicates whether the device is up to date with required Windows updates.
    /// </summary>
    public bool IsUpToDate { get; init; }

    /// <summary>
    /// Count of pending updates waiting to be downloaded or installed.
    /// </summary>
    public int PendingUpdatesCount
    {
        get => _pendingUpdatesCount;
        init => _pendingUpdatesCount = value >= 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Pending updates count cannot be negative.");
    }

    /// <summary>
    /// Indicates whether a reboot is required to finish installing updates.
    /// </summary>
    public bool RebootRequired { get; init; }

    /// <summary>
    /// Timestamp when Windows Update was last checked.
    /// </summary>
    public DateTimeOffset? LastCheckTime { get; init; }

    /// <summary>
    /// Timestamp when updates were last successfully installed.
    /// </summary>
    public DateTimeOffset? LastInstallationTime { get; init; }
}

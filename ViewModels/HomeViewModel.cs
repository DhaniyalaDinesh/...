using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using IWSEndpointCompanion.Models;
using Microsoft.Extensions.Logging;

namespace WPFUI.ViewModels;

/// <summary>
/// ViewModel for the main Home dashboard, populated with sample domain data from Phase 1 models.
/// </summary>
public class HomeViewModel : ViewModelBase
{
    private readonly ILogger<HomeViewModel>? _logger;
    private string _greeting = "Good morning, Dinesh";
    private string _subtitle = "Here's the current health status and approved self-service actions for your PC.";
    private string _deviceName = "CORP-LAPTOP-01";
    private string _operatingSystem = "Windows 11 Enterprise 23H2";
    private string _lastUpdatedText = "Today at 10:45 AM";
    private int _healthScore = 85;
    private string _healthStatusText = "Good";
    private string _healthSummaryText = "Your endpoint security subsystems are active and operational. 1 non-critical optimization recommended.";
    private string _updateStatusText = "Up to date";
    private string _lastCheckedText = "Today at 09:30 AM";
    private int _pendingUpdatesCount = 0;

    /// <summary>
    /// Parameterless constructor for XAML designer and backward compatibility.
    /// </summary>
    public HomeViewModel() : this(null)
    {
    }

    /// <summary>
    /// Dependency injection constructor.
    /// </summary>
    public HomeViewModel(ILogger<HomeViewModel>? logger)
    {
        _logger = logger;
        _logger?.LogInformation("HomeViewModel instantiated.");

        HealthCards = new ObservableCollection<HealthCardViewModel>();
        SuggestedFixes = new ObservableCollection<SuggestedFixItemViewModel>();
        TopAlerts = new ObservableCollection<AlertItemViewModel>();
        RecentActivities = new ObservableCollection<RecentActivityItemViewModel>();

        // Commands
        RefreshCommand = new RelayCommand(ExecuteRefresh);
        CheckUpdatesCommand = new RelayCommand(ExecuteCheckUpdates);

        InitializeSampleData();
    }

    public string Greeting
    {
        get => _greeting;
        set => SetProperty(ref _greeting, value);
    }

    public string Subtitle
    {
        get => _subtitle;
        set => SetProperty(ref _subtitle, value);
    }

    public string DeviceName
    {
        get => _deviceName;
        set => SetProperty(ref _deviceName, value);
    }

    public string OperatingSystem
    {
        get => _operatingSystem;
        set => SetProperty(ref _operatingSystem, value);
    }

    public string LastUpdatedText
    {
        get => _lastUpdatedText;
        set => SetProperty(ref _lastUpdatedText, value);
    }

    public int HealthScore
    {
        get => _healthScore;
        set => SetProperty(ref _healthScore, value);
    }

    public string HealthStatusText
    {
        get => _healthStatusText;
        set => SetProperty(ref _healthStatusText, value);
    }

    public string HealthSummaryText
    {
        get => _healthSummaryText;
        set => SetProperty(ref _healthSummaryText, value);
    }

    public string UpdateStatusText
    {
        get => _updateStatusText;
        set => SetProperty(ref _updateStatusText, value);
    }

    public string LastCheckedText
    {
        get => _lastCheckedText;
        set => SetProperty(ref _lastCheckedText, value);
    }

    public int PendingUpdatesCount
    {
        get => _pendingUpdatesCount;
        set => SetProperty(ref _pendingUpdatesCount, value);
    }

    public ObservableCollection<HealthCardViewModel> HealthCards { get; }
    public ObservableCollection<SuggestedFixItemViewModel> SuggestedFixes { get; }
    public ObservableCollection<AlertItemViewModel> TopAlerts { get; }
    public ObservableCollection<RecentActivityItemViewModel> RecentActivities { get; }

    public ICommand RefreshCommand { get; }
    public ICommand CheckUpdatesCommand { get; }

    private void InitializeSampleData()
    {
        // 1. Device Health Cards (Disk Space, Antivirus, BitLocker, Battery, Windows Update, Agent Connection)
        HealthCards.Add(new HealthCardViewModel
        {
            Title = "Disk Space",
            IconResourceKey = "Icon_Disk",
            PrimaryValue = "68% Used",
            StatusText = "Optimal",
            Subtitle = "160 GB free of 500 GB",
            StatusLevel = "Good",
            ProgressValue = 68,
            ShowProgressBar = true
        });

        HealthCards.Add(new HealthCardViewModel
        {
            Title = "Antivirus",
            IconResourceKey = "Icon_Antivirus",
            PrimaryValue = "Active",
            StatusText = "Protected",
            Subtitle = "Microsoft Defender • Up to date",
            StatusLevel = "Good",
            ShowProgressBar = false
        });

        HealthCards.Add(new HealthCardViewModel
        {
            Title = "BitLocker",
            IconResourceKey = "Icon_BitLocker",
            PrimaryValue = "Encrypted",
            StatusText = "Encrypted",
            Subtitle = "Drive C: 100% Protected",
            StatusLevel = "Good",
            ProgressValue = 100,
            ShowProgressBar = true
        });

        HealthCards.Add(new HealthCardViewModel
        {
            Title = "Battery",
            IconResourceKey = "Icon_Battery",
            PrimaryValue = "88% Plugged In",
            StatusText = "Healthy",
            Subtitle = "AC Connected • ~4 hrs remaining",
            StatusLevel = "Good",
            ProgressValue = 88,
            ShowProgressBar = true
        });

        HealthCards.Add(new HealthCardViewModel
        {
            Title = "Windows Update",
            IconResourceKey = "Icon_WindowsUpdate",
            PrimaryValue = "Up to Date",
            StatusText = "Up to date",
            Subtitle = "0 updates pending • No restart",
            StatusLevel = "Good",
            ShowProgressBar = false
        });

        HealthCards.Add(new HealthCardViewModel
        {
            Title = "Agent Connection",
            IconResourceKey = "Icon_Agent",
            PrimaryValue = "Connected",
            StatusText = "Connected",
            Subtitle = "Endpoint Service • 12ms latency",
            StatusLevel = "Good",
            ShowProgressBar = false
        });

        // 2. Suggested Fixes (Phase 1 ActionDefinition models)
        var fix1 = new ActionDefinition(
            actionId: "act-clean-temp",
            scriptId: "clean-temp-v1",
            name: "Clear Temporary Files",
            description: "Cleans user and system cache and temporary directories to reclaim storage space.",
            category: "Storage",
            actionType: ActionType.Remediation,
            estimatedDuration: TimeSpan.FromMinutes(2),
            warningMessage: "Temporary files and unpinned caches will be purged.",
            availability: ActionAvailability.Available
        );

        var fix2 = new ActionDefinition(
            actionId: "act-check-winupdate",
            scriptId: "winupdate-scan-v1",
            name: "Check Windows Update",
            description: "Scans corporate patch catalog and validates device compliance status.",
            category: "Maintenance",
            actionType: ActionType.Detection,
            estimatedDuration: TimeSpan.FromMinutes(5),
            availability: ActionAvailability.Available
        );

        var fix3 = new ActionDefinition(
            actionId: "act-net-diag",
            scriptId: "net-diag-v2",
            name: "Run Network Diagnostics",
            description: "Validates Wi-Fi gateway, DNS resolution, and corporate VPN connectivity.",
            category: "Network",
            actionType: ActionType.Detection,
            estimatedDuration: TimeSpan.FromMinutes(1),
            availability: ActionAvailability.Available
        );

        SuggestedFixes.Add(new SuggestedFixItemViewModel(fix1, "Icon_Broom",
            fixCommand: new RelayCommand(() => MessageBox.Show("Sample action triggered: 'Clear Temporary Files'. Real action execution will be connected in a later phase.", "Action Requested", MessageBoxButton.OK, MessageBoxImage.Information)),
            viewCommand: new RelayCommand(() => MessageBox.Show("Details for 'Clear Temporary Files':\nCategory: Storage\nEstimated Duration: ~2 minutes\nType: Remediation", "Action Details", MessageBoxButton.OK, MessageBoxImage.Information))));

        SuggestedFixes.Add(new SuggestedFixItemViewModel(fix2, "Icon_WindowsUpdate",
            fixCommand: new RelayCommand(() => MessageBox.Show("Sample action triggered: 'Check Windows Update'.", "Action Requested", MessageBoxButton.OK, MessageBoxImage.Information)),
            viewCommand: new RelayCommand(() => MessageBox.Show("Details for 'Check Windows Update':\nCategory: Maintenance\nEstimated Duration: ~5 minutes\nType: Detection", "Action Details", MessageBoxButton.OK, MessageBoxImage.Information))));

        SuggestedFixes.Add(new SuggestedFixItemViewModel(fix3, "Icon_Network",
            fixCommand: new RelayCommand(() => MessageBox.Show("Sample action triggered: 'Run Network Diagnostics'.", "Action Requested", MessageBoxButton.OK, MessageBoxImage.Information)),
            viewCommand: new RelayCommand(() => MessageBox.Show("Details for 'Run Network Diagnostics':\nCategory: Network\nEstimated Duration: ~1 minute\nType: Detection", "Action Details", MessageBoxButton.OK, MessageBoxImage.Information))));

        // 3. Top Alerts (Phase 1 Alert models with various severities)
        var alert1 = new Alert(
            alertId: "alt-001",
            title: "Storage Advisory: Free space below 35%",
            description: "Drive C: has 160 GB available. Run the temporary files cleanup fix to reclaim up to 18 GB of cache.",
            severity: AlertSeverity.Warning,
            creationTime: DateTimeOffset.UtcNow.AddMinutes(-18),
            relatedActionId: "act-clean-temp"
        );

        var alert2 = new Alert(
            alertId: "alt-002",
            title: "Routine Update Check Due",
            description: "Last compliance scan was completed 48 hours ago. It is recommended to perform a scan.",
            severity: AlertSeverity.Information,
            creationTime: DateTimeOffset.UtcNow.AddHours(-2),
            relatedActionId: "act-check-winupdate"
        );

        var alert3 = new Alert(
            alertId: "alt-003",
            title: "Smart Battery Charging Active",
            description: "System is plugged in to AC power. Charging threshold is active to preserve battery lifespan.",
            severity: AlertSeverity.Information,
            creationTime: DateTimeOffset.UtcNow.AddDays(-1)
        );

        TopAlerts.Add(new AlertItemViewModel(alert1, "18 mins ago", "Clear Temporary Files",
            dismissCommand: new RelayCommand(p =>
            {
                if (p is AlertItemViewModel item)
                {
                    item.IsDismissed = true;
                    TopAlerts.Remove(item);
                }
            }),
            fixCommand: new RelayCommand(() => MessageBox.Show("Initiating suggested fix: 'Clear Temporary Files' to resolve low disk space advisory.", "Alert Remediation", MessageBoxButton.OK, MessageBoxImage.Information))));

        TopAlerts.Add(new AlertItemViewModel(alert2, "2 hrs ago", "Check Windows Update",
            dismissCommand: new RelayCommand(p =>
            {
                if (p is AlertItemViewModel item)
                {
                    item.IsDismissed = true;
                    TopAlerts.Remove(item);
                }
            }),
            fixCommand: new RelayCommand(() => MessageBox.Show("Initiating suggested fix: 'Check Windows Update'.", "Alert Remediation", MessageBoxButton.OK, MessageBoxImage.Information))));

        TopAlerts.Add(new AlertItemViewModel(alert3, "Yesterday", null,
            dismissCommand: new RelayCommand(p =>
            {
                if (p is AlertItemViewModel item)
                {
                    item.IsDismissed = true;
                    TopAlerts.Remove(item);
                }
            })));

        // 4. Recent Activity (Phase 1 ActionRun models: completed, running, and queued)
        var runRunning = new ActionRun(
            runId: "run-101",
            actionId: "act-net-diag",
            status: "Triggered",
            createdTime: DateTimeOffset.UtcNow.AddMinutes(-2),
            lastChangedTime: DateTimeOffset.UtcNow.AddSeconds(-30),
            actionDefinition: fix3,
            triggeredTime: DateTimeOffset.UtcNow.AddMinutes(-2)
        );

        var runCompleted = new ActionRun(
            runId: "run-100",
            actionId: "act-clean-temp",
            status: "Completed",
            createdTime: DateTimeOffset.UtcNow.AddHours(-1).AddMinutes(-30),
            lastChangedTime: DateTimeOffset.UtcNow.AddHours(-1).AddMinutes(-28),
            actionDefinition: fix1,
            resultMessage: "Successfully cleared 14.2 GB of temporary files and caches.",
            duration: TimeSpan.FromSeconds(82)
        );

        var runQueued = new ActionRun(
            runId: "run-102",
            actionId: "act-spooler-reset",
            status: "NotStarted",
            createdTime: DateTimeOffset.UtcNow.AddMinutes(-1),
            lastChangedTime: DateTimeOffset.UtcNow.AddMinutes(-1),
            actionDefinition: new ActionDefinition("act-spooler-reset", "spooler-reset-v1", "Reset Print Spooler", "Clears corrupted print spool queue", "Peripherals", ActionType.Remediation)
        );

        var runCompleted2 = new ActionRun(
            runId: "run-099",
            actionId: "act-teams-opt",
            status: "Completed",
            createdTime: DateTimeOffset.UtcNow.AddDays(-1),
            lastChangedTime: DateTimeOffset.UtcNow.AddDays(-1),
            actionDefinition: new ActionDefinition("act-teams-opt", "teams-clean-v1", "Teams Cache Optimization", "Optimized Teams client memory cache", "Office", ActionType.Remediation),
            resultMessage: "Cleared old Teams meeting artifacts.",
            duration: TimeSpan.FromSeconds(45)
        );

        RecentActivities.Add(new RecentActivityItemViewModel(runRunning, "Today, 10:43 AM"));
        RecentActivities.Add(new RecentActivityItemViewModel(runQueued, "Today, 10:44 AM"));
        RecentActivities.Add(new RecentActivityItemViewModel(runCompleted, "Today, 09:15 AM"));
        RecentActivities.Add(new RecentActivityItemViewModel(runCompleted2, "Yesterday, 04:30 PM"));
    }

    private void ExecuteRefresh()
    {
        _logger?.LogInformation("Home dashboard telemetry refresh requested.");
        LastUpdatedText = $"Today at {DateTime.Now:hh:mm tt}";
        MessageBox.Show("Home dashboard refreshed with latest endpoint telemetry.", "Dashboard Refreshed", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void ExecuteCheckUpdates()
    {
        _logger?.LogInformation("Windows Update compliance check initiated.");
        LastCheckedText = $"Today at {DateTime.Now:hh:mm tt}";
        MessageBox.Show("Triggered Windows Update compliance check. All policies are up to date.", "Windows Update", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}

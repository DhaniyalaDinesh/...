namespace WPFUI.ViewModels;

/// <summary>
/// Presentation model for each of the 6 device health metric cards on the Home dashboard.
/// </summary>
public class HealthCardViewModel : ViewModelBase
{
    private string _title = string.Empty;
    private string _iconResourceKey = string.Empty;
    private string _primaryValue = string.Empty;
    private string _statusText = string.Empty;
    private string _subtitle = string.Empty;
    private string _statusLevel = "Good"; // Good, Warning, Error, Info
    private double? _progressValue;
    private bool _showProgressBar;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string IconResourceKey
    {
        get => _iconResourceKey;
        set => SetProperty(ref _iconResourceKey, value);
    }

    public string PrimaryValue
    {
        get => _primaryValue;
        set => SetProperty(ref _primaryValue, value);
    }

    public string StatusText
    {
        get => _statusText;
        set => SetProperty(ref _statusText, value);
    }

    public string Subtitle
    {
        get => _subtitle;
        set => SetProperty(ref _subtitle, value);
    }

    /// <summary>
    /// Status semantic level: "Good", "Warning", "Error", or "Info".
    /// </summary>
    public string StatusLevel
    {
        get => _statusLevel;
        set => SetProperty(ref _statusLevel, value);
    }

    public double? ProgressValue
    {
        get => _progressValue;
        set => SetProperty(ref _progressValue, value);
    }

    public bool ShowProgressBar
    {
        get => _showProgressBar;
        set => SetProperty(ref _showProgressBar, value);
    }
}

using System.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace WPFUI.Services.Theme;

/// <summary>
/// Service responsible for managing application themes (Light, Dark, System)
/// and switching resource dictionaries dynamically at runtime.
/// </summary>
public class ThemeService : IThemeService, IDisposable
{
    private const string LightThemeUri = "/WPFUI;component/Resources/Theme.Light.xaml";
    private const string DarkThemeUri = "/WPFUI;component/Resources/Theme.Dark.xaml";

    private readonly ILogger<ThemeService>? _logger;
    private ThemeMode _currentThemeMode = ThemeMode.Light;
    private bool _isDisposed;

    public ThemeService(ILogger<ThemeService>? logger = null)
    {
        _logger = logger;

        try
        {
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
        }
        catch (Exception ex)
        {
            _logger?.LogWarning(ex, "Failed to register SystemEvents.UserPreferenceChanged handler.");
        }
    }

    public ThemeMode CurrentThemeMode => _currentThemeMode;

    public bool IsDarkTheme => _currentThemeMode switch
    {
        ThemeMode.Dark => true,
        ThemeMode.Light => false,
        ThemeMode.System => IsSystemDarkTheme(),
        _ => false
    };

    public event EventHandler<bool>? ThemeChanged;

    public void SetTheme(ThemeMode mode)
    {
        if (_currentThemeMode == mode)
        {
            return;
        }

        _currentThemeMode = mode;
        _logger?.LogInformation("Theme mode set to {ThemeMode}. Active dark appearance: {IsDarkTheme}", _currentThemeMode, IsDarkTheme);
        ApplyTheme();
    }

    public void ToggleTheme()
    {
        var targetMode = IsDarkTheme ? ThemeMode.Light : ThemeMode.Dark;
        SetTheme(targetMode);
    }

    public void ApplyTheme()
    {
        bool isDark = IsDarkTheme;
        string relativePath = isDark ? DarkThemeUri : LightThemeUri;
        var themeUri = new Uri(relativePath, UriKind.RelativeOrAbsolute);
        var newThemeDict = new ResourceDictionary { Source = themeUri };

        if (Application.Current != null)
        {
            var merged = Application.Current.Resources.MergedDictionaries;
            bool replaced = false;

            for (int i = 0; i < merged.Count; i++)
            {
                var source = merged[i].Source?.OriginalString;
                if (source != null && (source.Contains("Theme.Light.xaml") || source.Contains("Theme.Dark.xaml")))
                {
                    merged[i] = newThemeDict;
                    replaced = true;
                    break;
                }
            }

            if (!replaced)
            {
                merged.Add(newThemeDict);
            }
        }

        _logger?.LogInformation("Applied active theme dictionary: {ThemeUri} (Dark: {IsDark})", relativePath, isDark);
        ThemeChanged?.Invoke(this, isDark);
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category is UserPreferenceCategory.General or UserPreferenceCategory.VisualStyle)
        {
            if (_currentThemeMode == ThemeMode.System)
            {
                _logger?.LogInformation("System appearance preference changed. Updating application theme...");
                ApplyTheme();
            }
        }
    }

    /// <summary>
    /// Reads Windows 10/11 personalized theme setting from the registry.
    /// </summary>
    public static bool IsSystemDarkTheme()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var value = key?.GetValue("AppsUseLightTheme");
            if (value is int intVal)
            {
                return intVal == 0; // 0 = Dark, 1 = Light
            }
        }
        catch
        {
            // Fallback to light theme if registry cannot be accessed
        }

        return false;
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            try
            {
                SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
            }
            catch
            {
                // Ignore cleanup errors
            }
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}

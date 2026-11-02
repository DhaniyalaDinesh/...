namespace WPFUI.Services.Theme;

/// <summary>
/// Contract for managing application themes and system preference synchronization.
/// </summary>
public interface IThemeService
{
    /// <summary>
    /// Gets the current theme configuration mode (Light, Dark, or System).
    /// </summary>
    ThemeMode CurrentThemeMode { get; }

    /// <summary>
    /// Gets whether the currently active appearance is dark.
    /// </summary>
    bool IsDarkTheme { get; }

    /// <summary>
    /// Raised whenever the active theme changes. The boolean parameter indicates whether dark theme is active.
    /// </summary>
    event EventHandler<bool>? ThemeChanged;

    /// <summary>
    /// Sets the application theme to the specified mode.
    /// </summary>
    /// <param name="mode">The desired theme mode.</param>
    void SetTheme(ThemeMode mode);

    /// <summary>
    /// Toggles between Light and Dark themes.
    /// </summary>
    void ToggleTheme();

    /// <summary>
    /// Applies the current theme mode to the active WPF application resource dictionaries.
    /// </summary>
    void ApplyTheme();
}

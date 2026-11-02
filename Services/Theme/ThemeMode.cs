namespace WPFUI.Services.Theme;

/// <summary>
/// Specifies the theme mode for the application.
/// </summary>
public enum ThemeMode
{
    /// <summary>
    /// Explicit Light theme.
    /// </summary>
    Light,

    /// <summary>
    /// Explicit Dark theme.
    /// </summary>
    Dark,

    /// <summary>
    /// Automatically follows Windows system theme preference.
    /// </summary>
    System
}

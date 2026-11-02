using System.Text.RegularExpressions;

namespace WPFUI.Services.Logging;

/// <summary>
/// Helper utilities and sanitization extensions to prevent accidental logging of sensitive credentials.
/// </summary>
public static partial class LoggingExtensions
{
    private static readonly string[] SensitiveKeys =
    [
        "password",
        "secret",
        "token",
        "authorization",
        "bearer",
        "api_key",
        "apikey",
        "credential"
    ];

    /// <summary>
    /// Masks sensitive tokens and passwords from log strings.
    /// </summary>
    public static string Sanitize(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        string result = input;
        foreach (var key in SensitiveKeys)
        {
            // Replace key=value or key: value
            result = Regex.Replace(
                result,
                $@"(?i)({Regex.Escape(key)}\s*[:=]\s*)([^\s,;""']+)",
                "$1[REDACTED]");
        }

        return result;
    }
}

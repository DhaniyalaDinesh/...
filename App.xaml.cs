using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WPFUI.Services.DependencyInjection;
using WPFUI.Services.Theme;

namespace WPFUI;

/// <summary>
/// Interaction logic for App.xaml, managing host lifecycle, dependency injection, and logging.
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .ConfigureServices((context, services) =>
            {
                services.ConfigureApplicationServices();
            })
            .Build();
    }

    /// <summary>
    /// Static access to the application dependency injection service provider.
    /// </summary>
    public static IServiceProvider Services => ((App)Current)._host.Services;

    /// <summary>
    /// Gets whether dark theme is currently active.
    /// </summary>
    public static bool IsDarkTheme
    {
        get
        {
            if (Current is App app && app._host != null)
            {
                var themeService = app._host.Services.GetService<IThemeService>();
                return themeService?.IsDarkTheme ?? false;
            }
            return false;
        }
    }

    /// <summary>
    /// Legacy static helper to switch theme dynamically at runtime.
    /// </summary>
    public static void SetTheme(bool isDark)
    {
        if (Current is App app && app._host != null)
        {
            var themeService = app._host.Services.GetService<IThemeService>();
            themeService?.SetTheme(isDark ? ThemeMode.Dark : ThemeMode.Light);
        }
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        await _host.StartAsync();

        var logger = _host.Services.GetRequiredService<ILogger<App>>();
        logger.LogInformation("==================================================================");
        logger.LogInformation("IWS Endpoint Companion (Phase 3) starting up...");
        logger.LogInformation("==================================================================");

        // Apply theme using registered ThemeService
        var themeService = _host.Services.GetRequiredService<IThemeService>();
        themeService.ApplyTheme();

        // Resolve MainWindow through Dependency Injection
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        logger.LogInformation("MainWindow successfully resolved through Dependency Injection.");
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        var logger = _host.Services.GetService<ILogger<App>>();
        logger?.LogInformation("IWS Endpoint Companion shutting down gracefully...");

        using (_host)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
        }

        base.OnExit(e);
    }
}

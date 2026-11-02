using Microsoft.Extensions.DependencyInjection;
using WPFUI.Services.Dispatching;
using WPFUI.Services.Theme;
using WPFUI.ViewModels;

namespace WPFUI.Services.DependencyInjection;

/// <summary>
/// Centralized service registration for Microsoft Dependency Injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all application-level services, ViewModels, and views into the DI container.
    /// </summary>
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        // -------------------------------------------------------------------------
        // Core Shared Infrastructure
        // -------------------------------------------------------------------------
        services.AddSingleton<IDispatcherService, DispatcherService>();
        services.AddSingleton<IThemeService, ThemeService>();

        // -------------------------------------------------------------------------
        // ViewModels
        // -------------------------------------------------------------------------
        services.AddSingleton<MainViewModel>();
        services.AddTransient<HomeViewModel>();

        // -------------------------------------------------------------------------
        // Views
        // -------------------------------------------------------------------------
        services.AddSingleton<MainWindow>();

        // -------------------------------------------------------------------------
        // Future Services Extension Point (Phase 4):
        // When implementing Phase 4, the API client abstraction and mock/real client
        // will be registered here without modifying ViewModels or Views:
        // services.AddSingleton<IEndpointApiClient, MockEndpointApiClient>();
        // -------------------------------------------------------------------------

        return services;
    }
}

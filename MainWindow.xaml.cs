using System.Windows;
using Microsoft.Extensions.Logging;
using WPFUI.ViewModels;

namespace WPFUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ILogger<MainWindow>? _logger;

    /// <summary>
    /// Parameterless constructor for XAML designer support.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Dependency injection constructor.
    /// </summary>
    public MainWindow(MainViewModel viewModel, ILogger<MainWindow>? logger = null)
        : this()
    {
        DataContext = viewModel;
        _logger = logger;
        _logger?.LogInformation("MainWindow initialized with MainViewModel via Dependency Injection.");
    }
}
